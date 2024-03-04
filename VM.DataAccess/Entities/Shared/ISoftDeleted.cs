namespace VM.DataAccess.Entities.Shared
{
    public interface ISoftDeleted
    {
        bool IsDeleted { get; set; }
    }
}
