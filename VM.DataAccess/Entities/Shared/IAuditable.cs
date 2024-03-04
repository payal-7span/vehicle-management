namespace VM.DataAccess.Entities.Shared
{
    public interface IAuditable : ICreatable
    {
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public interface ICreatable
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
