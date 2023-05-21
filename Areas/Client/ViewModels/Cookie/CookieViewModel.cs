namespace Read_Excel_File.Areas.Client.ViewModels.Cookie;

public class CookieViewModel
{
    public CookieViewModel(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
    public CookieViewModel()
    {

    }

    public string Name { get; set; }
    public int Quantity { get; set; }
}
