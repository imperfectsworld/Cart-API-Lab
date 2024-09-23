namespace Cart_API_Lab.Models
{
    public class CartItem
    {
        public long id { get; set; }
        public string product { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }
}
