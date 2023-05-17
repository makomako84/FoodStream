namespace Foodstream.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FoodPartnerItemId { get; set; }
    }
}
