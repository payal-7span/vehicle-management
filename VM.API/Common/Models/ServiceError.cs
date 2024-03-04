namespace VM.API.Common.Models
{
    [Serializable]
    public class ServiceError
    {
        public ServiceError(string code, params string[] arguments)
        {
            Code = code;
            Arguments = arguments;
        }

        public string[] Arguments { get; }

        public string Code { get; }
    }
}
