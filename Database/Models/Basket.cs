namespace Read_Excel_File.Database.Models;

public class Basket 
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<BasketProduct>? BasketProducts { get; set; }
}