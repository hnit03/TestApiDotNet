using Task3.Models;

namespace Task3.IServices
{
    public interface IAuthenticationService
    {
        public string GenerateJSONWebToken(Member member);
    }
}
