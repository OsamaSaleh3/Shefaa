public abstract class BaseEntity
{
    public int Id { get; protected set; }     
    public DateTime CreatedAt { get; private set; } = DateTime.Now; 
    public DateTime? UpdatedAt { get; protected set; }
    public bool IsDeleted { get; protected set; } = false; 

    public void MarkAsUpdated() => UpdatedAt = DateTime.Now;
    public void SoftDelete() => IsDeleted = true;
}