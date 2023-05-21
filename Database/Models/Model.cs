namespace Read_Excel_File.Database.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
