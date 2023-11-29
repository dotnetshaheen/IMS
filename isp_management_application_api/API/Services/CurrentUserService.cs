using Application.Abstractions.Services;
using System.Security.Claims;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _context;
        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext;
        }
        
        private List<Claim> _claim => _context.User.Claims.ToList();

        public int UserId { 
            get
            {
                if (_claim.Any(x => x.Type == "uid"))
                {
                    int id = Convert.ToInt32(_claim.First(x => x.Type == "uid").Value);
                    return id > 0 ? id : 0;
                }
                return 0;
            }
        }
        //public int RoleId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
