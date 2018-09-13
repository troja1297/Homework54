namespace MyFirstMVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Addres { get; set; }
        public string ContactPhone { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        
    }
}