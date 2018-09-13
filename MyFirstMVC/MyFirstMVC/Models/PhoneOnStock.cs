namespace MyFirstMVC.Models
{
    public class PhoneOnStock
    {
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int Quantity { get; set; }
    }
}