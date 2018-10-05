namespace MyFirstMVC.Services
{
    public class ErrorMessage
    {
        public bool IsValid { get; set; }
        public string FieldName { get; set; }
        public string Message { get; set; }

        public static ErrorMessage Correct =>
            new ErrorMessage()
            {
                IsValid = true
            };

    }
}