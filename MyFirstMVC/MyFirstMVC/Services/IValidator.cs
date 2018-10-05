using System.Collections.Generic;
using MyFirstMVC.Models;

namespace MyFirstMVC.Services
{
    public interface IValidator<in T> where T : Entity
    {
        List<ErrorMessage> Validate(T entity);
    }
}