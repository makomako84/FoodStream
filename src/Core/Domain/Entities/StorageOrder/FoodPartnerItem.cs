namespace Foodstream.Domain
{
    public class FoodPartnerItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoodGlobalItemId { get; set; }
        public float Cost { get; set; }
        public int SellerId { get; set; }
        public int Count { get; set; }
    }
}
