namespace ProjectAssignment.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public decimal Rating { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }

    }
}
