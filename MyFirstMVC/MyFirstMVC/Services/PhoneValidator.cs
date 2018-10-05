using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstMVC.Models;

namespace MyFirstMVC.Services
{
    public class PhoneValidator : IValidator<Phone>
    {
        public List<ErrorMessage> Validate(Phone phone)
        {
            return new List<ErrorMessage>()
            {
                CheckDate(phone.AssemblyDate),
                CheckPrice(phone.Price)
            };
        }

        private ErrorMessage CheckDate(DateTime assemblyDate)
        {
            if (assemblyDate > DateTime.Now)
            {
                return new ErrorMessage()
                {
                    IsValid = false,
                    Message = "Дата не должа быть из будущего",
                    FieldName = nameof(Phone.AssemblyDate)
                };
            }

            return ErrorMessage.Correct;
        }

        private ErrorMessage CheckPrice(double price)
        {
            if (price < 0 || price > 500000)
            {
                return new ErrorMessage()
                {
                    IsValid = false,
                    Message = "Цена должна быть от 0 до 500000",
                    FieldName = nameof(Phone.Price)
                };
            }
            return ErrorMessage.Correct;
        }
    }
}
