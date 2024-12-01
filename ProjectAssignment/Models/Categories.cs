namespace ProjectAssignment.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
