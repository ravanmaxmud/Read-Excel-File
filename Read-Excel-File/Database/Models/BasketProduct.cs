using System.Drawing;

namespace Read_Excel_File.Database.Models;

public class BasketProduct 
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public Basket? Basket { get; set; }
    public string ModelName { get; set; }
    public int ModelQuantity { get; set; }
}