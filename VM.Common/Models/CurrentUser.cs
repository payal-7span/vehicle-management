using Microsoft.AspNetCore.Http;
using VM.Common.Constants;

namespace VM.Common.Models
{
    public interface ICurrentUser
    {
        int Id { get; }
        bool IsLoggedIn
        {
            get
            {
                return Id > 0;
            }
        }
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly HttpContext _httpContext;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            Initialize();
        }
        private List<Dictionary<string, string>> values { get; set; }
        public int Id
        {
            get
            {
                var userIdDictionary = values.Where(t => t.ContainsKey(ClaimNames.UserId)).FirstOrDefault();
                if (userIdDictionary == null || userIdDictionary[ClaimNames.UserId] == null)
                    return 0;
                else if (int.TryParse(userIdDictionary[ClaimNames.UserId], out int result))
                    return result;

                return 0;
            }
        }
        private void Initialize()
        {
            values = Get();
        }

        private List<Dictionary<string, string>> Get()
        {
            return _httpContext.User.Claims.Select(u => new Dictionary<string, string> { { u.Type, u.Value } }).ToList();
        }
    }
}
