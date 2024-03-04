namespace VM.API.Common.Models
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public ServiceError? Error { get; set; }

        public static ApiResult SuccessResult()
        {
            return new ApiResult { Success = true };
        }
        public static ApiResult ErrorResult(ServiceError error)
        {
            return new ApiResult { Success = false, Error = error };
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public required T Data { get; set; }

        public ApiResult GetOnlyResult()
        {
            return new ApiResult()
            {
                Success = Success,
                Error = Error
            };
        }

        public static ApiResult<T> SuccessResult(T data)
        {
            return new ApiResult<T> { Data = data, Success = true };
        }
        public static ApiResult<T> ErrorResult(T data, ServiceError error)
        {
            return new ApiResult<T> { Data = data, Success = false, Error = error };
        }
    }
}
