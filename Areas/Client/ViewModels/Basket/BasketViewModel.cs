namespace Read_Excel_File.Areas.Client.ViewModels.Basket
{
    public class BasketViewModel
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }


        public BasketViewModel()
        {

        }

        public BasketViewModel( string? name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
