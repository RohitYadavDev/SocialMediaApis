using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SocialMediaApis.CommonMethod;
using SocialMediaApis.DBContext;
using SocialMediaApis.Models;
using System;
using System.Linq;
using static SocialMediaApis.CommonMethod.CommonModel;

namespace SocialMediaApis.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public JsonModel UserRegistration(Registration registration)
        {
            try
            {
                //If registration Model Null
                if(registration == null)
                {
                    return new JsonModel
                    {
                        Data = registration,
                        Message = ConstString.RegistrationDataNull,
                        StatusCode = StatusCodes.Status204NoContent,
                    };
                }

                //Check Username And EmailID Exits
                var CheckExitsResult = _context.users.Where(x=>x.UserName == registration.UserName || x.EmailId == registration.EmailId).Select(x=> new {x.UserName,x.EmailId}).FirstOrDefault();
                if (CheckExitsResult != null)
                {
                    if(CheckExitsResult.UserName == registration.UserName)
                    {
                        return new JsonModel
                        {
                            Data = registration,
                            Message = ConstString.UserNameExits,
                            StatusCode = StatusCodes.Status204NoContent
                        };
                    }
                    else
                    {
                        return new JsonModel
                        {
                            Data = registration,
                            Message = ConstString.EmailExits,
                            StatusCode = StatusCodes.Status204NoContent
                        };
                    }
                }
                else
                {
                    try
                    {
                        var RegistrationResult = new User();              
                        RegistrationResult.FirstName = registration.FirstName;
                        RegistrationResult.LastName = registration.LastName;
                        RegistrationResult.EmailId = registration.EmailId;
                        RegistrationResult.UserName = registration.UserName;
                        RegistrationResult.Password = CommonMethods.Encryptword(registration.Password);
                        _context.users.Add(RegistrationResult);
                        _context.SaveChangesAsync();
                        return new JsonModel
                        {
                            Data = registration,
                            Message = ConstString.RegistrationSuccessfully,
                            StatusCode = StatusCodes.Status200OK,
                        };

                    }catch (Exception ex)
                    {
                        return new JsonModel
                        {
                            Message = "Registration Save Changes Error Message" + ex.Message,
                            StatusCode = StatusCodes.Status406NotAcceptable,
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
