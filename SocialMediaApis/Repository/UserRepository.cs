using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SocialMediaApis.CommonMethod;
using SocialMediaApis.DBContext;
using SocialMediaApis.Models;
using System;
using System.Data;
using System.Linq;
using static SocialMediaApis.CommonMethod.CommonModel;
using static SocialMediaApis.Models.UserModel;

namespace SocialMediaApis.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly UserDbContext _context;
        public readonly CommonMethods _commonMethods;
        public UserRepository(UserDbContext context, CommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
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
                            StatusCode = 210,
                        };

                    }
                    if (CheckExitsResult.EmailId == ((registration.EmailId).ToLower()))
                    {
                       return new JsonModel
                       {
                           Data = registration,
                           Message = ConstString.EmailExits,
                           StatusCode = 210,
                       };
                    }
                }
                else
                {
                    try
                    {
                        var RegistrationResult = new Users();              
                        RegistrationResult.FirstName = registration.FirstName;
                        RegistrationResult.LastName = registration.LastName;

                        //Store the EmailId And UserName casesenstive
                        RegistrationResult.EmailId = (registration.EmailId).ToLower();
                        RegistrationResult.UserName = (registration.UserName).ToLower();
                        RegistrationResult.Password = _commonMethods.Encryptword(registration.Password);
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
                            StatusCode = 210,
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
                    StatusCode = 210,
                };
            }
        }
        public JsonModel Login(Login login)
        {
            try
            {
                login.Password = _commonMethods.Encryptword(login.Password);
                var userExits = _context.users.Where(x => x.UserName == (login.UserName).ToLower() && x.Password == login.Password).FirstOrDefault();
                if (userExits != null)
                {
                    return new JsonModel
                    {
                        Data = userExits,
                        Message = CommonMethod.ConstString.LoginSuccessfully,
                        StatusCode = StatusCodes.Status200OK,
                        AccessToken = _commonMethods.GenerateToken(login.UserName),
                    };
                }
                else
                {

                    var userNameExits = _context.users.Where(x => x.UserName == (login.UserName).ToLower()).FirstOrDefault();
                    var passwordExits = _context.users.Where(x => x.Password == (_commonMethods.Encryptword(login.Password))).FirstOrDefault();
                    if (userNameExits == null && passwordExits == null)
                    {
                        return new JsonModel
                        {
                            Message = CommonMethod.ConstString.invalid,
                            StatusCode = 201,
                        };
                    }
                    else if (passwordExits == null)
                    {
                        return new JsonModel
                        {
                            Message = CommonMethod.ConstString.InvalidPassword,
                            StatusCode = 201,
                        };
                    }
                    else
                    {
                        return new JsonModel
                        {
                            Message = CommonMethod.ConstString.InvalidUserName,
                            StatusCode = 201,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonModel
                {
                    Message = CommonMethod.ConstString.UnKnowError,
                    StatusCode = 201,
                };
            }
        }
    }
}
