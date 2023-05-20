namespace Read_Excel_File.Areas.Client.ViewModels.Models
{
    public class ModelListItemViewModel
    {
        public ModelListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
