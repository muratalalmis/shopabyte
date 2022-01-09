namespace Ordering.Domain.Entities
{
    /// <summary>
    /// The base entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Key column
        /// </summary>
        int Id { get; set; }
    }
}
