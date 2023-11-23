namespace OrderWebApp.Infrastructure.Entities
{
    public class Store : IDbEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
