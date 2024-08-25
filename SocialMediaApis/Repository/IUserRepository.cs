using SocialMediaApis.Models;
using static SocialMediaApis.CommonMethod.CommonModel;
using static SocialMediaApis.Models.UserModel;

namespace SocialMediaApis.Repository
{
    public interface IUserRepository
    {
        JsonModel UserRegistration(Registration registration);
        JsonModel Login(Login login);
    }
}
