namespace Webapi.models
{
    public class plan
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public ICollection<subscription>? subscriptions { get; set; }
    }
}
