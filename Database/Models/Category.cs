namespace Read_Excel_File.Database.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Model> Models { get; set; }
    }
}
