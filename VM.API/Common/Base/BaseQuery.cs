namespace VM.API.Common.Base
{
    public class BaseQuery
    {

    }
    public class BaseModifiableQuery : BaseQuery
    {
        public int Id { get; private set; }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
