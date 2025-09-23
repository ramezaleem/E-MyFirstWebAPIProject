using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyFirstWebAPIProject.Models;

public class CustomObjectModelBinder : IModelBinder
{
    public Task BindModelAsync ( ModelBindingContext bindingContext )
    {
        throw new NotImplementedException();
    }
}
