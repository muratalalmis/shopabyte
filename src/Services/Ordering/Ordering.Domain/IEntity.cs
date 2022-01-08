namespace Ordering.Domain
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
