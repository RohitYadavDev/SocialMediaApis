using SocialMediaApis.Models;
using static SocialMediaApis.CommonMethod.CommonModel;

namespace SocialMediaApis.Repository
{
    public interface IUserRepository
    {
        JsonModel UserRegistration(Registration registration);
    }
}
