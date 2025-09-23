using AutoMapper;
using AutoMapperComplexMappingDemo.Data;
using AutoMapperComplexMappingDemo.DTOs;
using AutoMapperComplexMappingDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperComplexMappingDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ECommerceDBContext _context;
    private readonly IMapper _mapper;
    public OrderController ( ECommerceDBContext context, IMapper mapper )
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDTO>> CreateOrder ( [FromBody] OrderCreateDTO orderCreateDTO )
    {
        // Validate the incoming order data
        if (orderCreateDTO == null)
        {
            return BadRequest("Order data is null.");
        }
        try
        {
            // Check if the customer exists in the database
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == orderCreateDTO.CustomerId);
            if (!customerExists)
            {
                return NotFound($"Customer with ID {orderCreateDTO.CustomerId} not found.");
            }
            // Validate the products in the order
            // Extract product IDs from the order items
            var productIds = orderCreateDTO.Items.Select(i => i.ProductId).ToList();
            // Retrieve products from the database
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            // Validate that all products exist
            if (products.Count != productIds.Count)
            {
                return BadRequest("One or more products in the order are invalid.");
            }

            // Map OrderCreateDTO to Order entity
            var order = _mapper.Map<Order>(orderCreateDTO);
            // Calculate the total amount of the order based on product prices and quantities
            decimal totalAmount = 0;
            foreach (var item in order.OrderItems)
            {
                var product = products.First(p => p.Id == item.ProductId);

                // Calculate total price for each order item and accumulate the total order amount
                totalAmount += product.Price * item.Quantity;
            }
            order.Amount = totalAmount; // Set the calculated amount
                                        // Add the new order to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Save changes asynchronously
                                               // Fetch the created order along with related data to return in the response
            var createdOrder = await _context.Orders
                .Include(o => o.Customer)
                .ThenInclude(c => c.Address)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == order.Id);
            if (createdOrder == null)
                return StatusCode(500, "An error occurred while creating the order.");
            // Map the created Order entity to OrderDTO
            var orderDTO = _mapper.Map<OrderDTO>(createdOrder);
            // Return the created order data in the response with a status code 201 (Created)
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderDTO);
        }
        catch (Exception ex)
        {
            // Return a 500 Internal Server Error with the exception message
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrderById ( int id )
    {
        try
        {
            // Fetch the order by its ID, including related data such as Customer, Address, and OrderItems
            var order = await _context.Orders
                .Include(o => o.Customer)
                .ThenInclude(c => c.Address)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return NotFound($"Order with ID {id} not found.");
            // Map the Order entity to OrderDTO
            var orderDTO = _mapper.Map<OrderDTO>(order);
            // Return the order data in the response
            return Ok(orderDTO);
        }
        catch (Exception ex)
        {
            // Return a 500 Internal Server Error with the exception message
            return StatusCode(500, $"An error occurred while fetching the order: {ex.Message}");
        }
    }


    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByCustomerId ( int customerId )
    {
        try
        {
            // Fetch all orders associated with a specific customer ID, including related data
            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Customer)
                .ThenInclude(c => c.Address)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
            if (orders == null || orders.Count == 0)
                return NotFound($"No orders found for customer with ID {customerId}.");
            // Map the list of Order entities to a list of OrderDTO
            var ordersDTO = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            // Return the list of orders in the response
            return Ok(ordersDTO);
        }
        catch (Exception ex)
        {
            // Return a 500 Internal Server Error with the exception message
            return StatusCode(500, $"An error occurred while fetching orders for customer ID {customerId}: {ex.Message}");
        }
    }


}
