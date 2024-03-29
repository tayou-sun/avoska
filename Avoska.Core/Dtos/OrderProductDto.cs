public class OrderProductDto
{
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal NewPrice { get; set; }
    public string Info { get; set; }
    public decimal TagId { get; set; }
    public StoreDto Store {get;set;}
    public decimal AdditionalSum { get; set; }
}