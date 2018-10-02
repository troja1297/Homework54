namespace MyFirstMVC.Models
{
    public class Order : Entity
    {
        public string User { get; set; }
        public string Addres { get; set; }
        public string ContactPhone { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        
    }
}