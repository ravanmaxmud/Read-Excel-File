namespace Read_Excel_File.Areas.Client.ViewModels.Home
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }

}
