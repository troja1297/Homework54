using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstMVC.Models;

namespace MyFirstMVC.Services
{
    public class FeedbackValidator : IValidator<Feedback>
    {
        public List<ErrorMessage> Validate(Feedback feedBack)
        {
            return new List<ErrorMessage>()
            {
                CheckAuthor(feedBack.Author),
                CheckMessage(feedBack.Message)
            };
        }

        private ErrorMessage CheckMessage(string feedBackMessage)
        {
            if (string.IsNullOrWhiteSpace(feedBackMessage))
            {
                return new ErrorMessage()
                {
                    IsValid = false,
                    FieldName = nameof(Feedback.Message),
                    Message = "Поле с отзывом не должно быть пустым"
                };
            }

            return ErrorMessage.Correct;
        }

        private ErrorMessage CheckAuthor(string feedBackAuthor)
        {
            if (string.IsNullOrWhiteSpace(feedBackAuthor))
            {
                return new ErrorMessage()
                {
                    IsValid = false,
                    FieldName = nameof(Feedback.Author),
                    Message = "Поле с автором не должно быть пустым"
                };
            }

            return ErrorMessage.Correct;
        }
    }
}
