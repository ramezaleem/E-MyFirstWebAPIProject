﻿using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Repositories;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll ();
    Employee? GetById ( int id );
    void Add ( Employee employee );
    void Update ( Employee employee );
    void Delete ( int id );
    bool Exists ( int id );
}
