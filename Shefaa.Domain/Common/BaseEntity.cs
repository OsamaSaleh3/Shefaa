public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; protected set; }
    public bool IsDeleted { get; protected set; } = false;
    public DateTime? DeletedAt { get; protected set; }

    public void MarkAsUpdated() => UpdatedAt = DateTime.Now;

    public virtual void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
    }
}