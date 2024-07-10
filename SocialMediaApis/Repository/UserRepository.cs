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
                var CheckExitsResult = _context.users.Where(x=>x.UserName == ((registration.UserName).ToLower()) || x.EmailId == ((registration.EmailId).ToLower())).Select(x=> new {x.UserName,x.EmailId}).FirstOrDefault();
                if (CheckExitsResult != null)
                {
                    if(CheckExitsResult.UserName == ((registration.UserName).ToLower()))
                    {
                        return new JsonModel
                        {
                            Data = registration,
                            Message = ConstString.UserNameExits,
                            StatusCode = StatusCodes.Status409Conflict,
                        };

                    }
                    if (CheckExitsResult.EmailId == ((registration.EmailId).ToLower()))
                    {
                       return new JsonModel
                       {
                           Data = registration,
                           Message = ConstString.EmailExits,
                           StatusCode = StatusCodes.Status409Conflict,
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

                        //Store the EmailId And UserName casesenstive
                        RegistrationResult.EmailId = (registration.EmailId).ToLower();
                        RegistrationResult.UserName = (registration.UserName).ToLower();
                        RegistrationResult.Password = CommonMethods.Encryptword(registration.Password);
                        RegistrationResult.CreateDate = DateTime.UtcNow;
                        RegistrationResult.UpdateDate = DateTime.UtcNow;
                        RegistrationResult.IsDeleted = false;
                        RegistrationResult.IsAdmin = false;
                        _context.users.Add(RegistrationResult);
                        _context.SaveChanges();
                        return new JsonModel
                        {
                            Data = registration,
                            Message = ConstString.RegistrationSuccessfully,
                            StatusCode = StatusCodes.Status200OK,
                        };

                    }
                    catch (Exception ex)
                    {
                        return new JsonModel
                        {
                            Message = "Registration Save Changes Error Message" + ex.Message,
                            StatusCode = StatusCodes.Status406NotAcceptable,
                        };
                    }
                }  
                return new JsonModel
                {
                    Data = registration,
                    Message = ConstString.UnKnowError,
                    StatusCode = StatusCodes.Status500InternalServerError,
                };

            }
            catch(Exception ex)
            {
                return new JsonModel
                {
                    Message = "Registration Error Message" + ex.Message,
                    StatusCode = StatusCodes.Status406NotAcceptable,
                };
            }
        }
    }
}
