namespace VM.API.Common.Base
{
    public class BaseCommand
    {
    }

    public class BaseModifiableCommand : BaseCommand
    {
        public int Id { get; private set; }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
