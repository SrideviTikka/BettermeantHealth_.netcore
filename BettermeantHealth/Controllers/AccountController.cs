using BettermeantHealth.BAL;
using BettermeantHealth.DataContract;
using BettermeantHealth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace BettermeantHealth.Controllers
{
    public class AccountController : BaseController
    {
        BL_Account objBL_Account = null;
        DataOperationResponse response = null;
        DC_UserLogins objDC_UserLogins = null;
        List<DC_UserLogins> lst_DC_UserLogins = null;
        DC_Roles dC_Roles = null;
        List<DC_Roles> lstdc_Roles = null;
        string strMessage = string.Empty;

        private readonly IConfiguration configuration;

        public AccountController(IConfiguration configuration) : base(configuration)
        {
            this.configuration = configuration;
            DC_StaticConstants.StrConnectionString = configuration.GetConnectionString("BettermeantHealthConStr");
        }

        // GET: Account
        public IActionResult Signup(IFormCollection frmcoll)
        {
            response = new DataOperationResponse();
            objBL_Account = new BL_Account();
            if (!string.IsNullOrEmpty(frmcoll["btnSave"]) && (string.Compare(frmcoll["btnSave"], "Save") == 0))
            {
                objDC_UserLogins = new DC_UserLogins();
                objDC_UserLogins.UserId = string.IsNullOrEmpty(frmcoll["hdnUserId"]) ? 0 : Convert.ToInt32(frmcoll["hdnUserId"]);
                objDC_UserLogins.FirstName = frmcoll["txtFirstName"];
                objDC_UserLogins.LastName = frmcoll["txtLastName"];
                objDC_UserLogins.EmailAddress = frmcoll["txtMail"];
                objDC_UserLogins.RoleId = Convert.ToInt32(frmcoll["ddlRoles"]);
                lst_DC_UserLogins = objBL_Account.PatientSignupAdd(objDC_UserLogins.UserId, objDC_UserLogins.FirstName, objDC_UserLogins.LastName, objDC_UserLogins.EmailAddress, objDC_UserLogins.RoleId);
                if (lst_DC_UserLogins.Count > 0)
                {
                    EmailAttributesModel objEmailAttributes = new EmailAttributesModel();
                    objEmailAttributes.Subject = "Patient Details";
                    objEmailAttributes.MessageBody = "Hello " + lst_DC_UserLogins[0].FirstName + "," + " <br/><br/>" + "Please find your New Password for Bettermeant Health. <br/>" + "Your new password is: " + lst_DC_UserLogins[0].Password + "" + "<br/><br/>Please access system at  " + AppConfig.HostName + "  <br/><br/> Thank You!";
                    objEmailAttributes.From = AppConfig.SMTPEMAILFROM;
                    objEmailAttributes.To = objDC_UserLogins.EmailAddress;
                    strMessage = SendMail(objEmailAttributes);
                    TempData["SuccessMessage"] = "Email has been sent to your account";
                    return Redirect("~/Account/Login");
                }
                else
                {
                    TempData["errorMessage"] = response.Message;
                }
            }
            return View();
        }

        #region Login
        /// <summary>
        /// Description: This returns login page
        /// </summary>
        /// <param name="frmUserLogOn"></param>
        /// <returns></returns>       
        public IActionResult Login(IFormCollection frmColl_Login)
        {           
            if (!string.IsNullOrEmpty(frmColl_Login["btnLogin"]) && (string.Compare(frmColl_Login["btnLogin"], "Login") == 0))
            {
                objBL_Account = new BL_Account();
                DC_UserLogins obj = new DC_UserLogins();
                obj.RoleId = string.IsNullOrEmpty(frmColl_Login["ddlRoles"]) ? 0 : Convert.ToInt32(frmColl_Login["ddlRoles"]);
                objDC_UserLogins = objBL_Account.UserLogon(frmColl_Login["txtUserName"], frmColl_Login["txtPassword"], obj.RoleId);
                HttpContext.Session.SetString("Session", JsonConvert.SerializeObject(objDC_UserLogins));
                DC_StaticConstants.Session_UserLogin = HttpContext.Session.GetString("Session") == null ? null : JsonConvert.DeserializeObject<DC_UserLogins>(HttpContext.Session.GetString("Session"));
                if (objDC_UserLogins.Code > 0)
                {
                   // Session["UserLogon"] = objDC_UserLogins;
                    if (string.IsNullOrEmpty(objDC_UserLogins.LastLoginTime.ToString()))
                    {
                        return Redirect("~/Account/ChangePassword");
                    }
                    else
                    {
                        if (objDC_UserLogins.RoleName == "Admin")
                        {
                            return Redirect("~/InsuranceCarrier/InsuranceCarrier");
                        }
                        else if (objDC_UserLogins.RoleName == "Patient")
                        {
                            return Redirect("~/User/UserDetails?UserId=" + objDC_UserLogins.UserId);
                        }
                        else
                        {
                            return Redirect("~/Physcian/PhyscianDetails?UserId=" + objDC_UserLogins.UserId);
                        }
                    }
                }
                else
                {
                    TempData["errorMessage"] = "The UserID/Password is Incorrect.";
                }
            }
            return View();
        }
        #endregion

        #region Change Password
        /// <summary>
        /// This method is used for Change Password
        /// </summary>
        /// <returns></returns>
        ///        
        public IActionResult ChangePassword(IFormCollection frmColl)
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                objDC_UserLogins = DC_StaticConstants.Session_UserLogin;
                objBL_Account = new BL_Account();
                response = new DataOperationResponse();
                if (!string.IsNullOrEmpty(frmColl["btnSubmit"]) && (string.Compare(frmColl["btnSubmit"], "Submit") == 0))
                {
                    response = objBL_Account.ChangePassword(objDC_UserLogins.UserId, frmColl["txtOldPassword"], frmColl["txtNewPassword"]);
                   // if (response.Code > 0)
                    {
                        EmailAttributesModel objEmailAttributes = new EmailAttributesModel();
                        objEmailAttributes.Subject = "Change Password";
                        objEmailAttributes.MessageBody = "Hello " + objDC_UserLogins.FirstName + "," + " <br/><br/>" + "Please find your New Password for Bettermeant Health. <br/>" + "Your new password is: " + frmColl["txtNewPassword"] + "<br/><br/>Please access system at  " + AppConfig.HostName + "  <br/><br/> Thank You!<br/>";
                        objEmailAttributes.From = AppConfig.SMTPEMAILFROM;
                        objEmailAttributes.To = objDC_UserLogins.EmailAddress;
                        strMessage = SendMail(objEmailAttributes);
                        TempData["SuccessMessage"] = response.Message;

                        if (objDC_UserLogins.RoleName == "Admin")
                        {
                            return Redirect("~/InsuranceCarrier/InsuranceCarrier");
                        }
                        else if (objDC_UserLogins.RoleName == "Patient")
                        {
                           // if (string.IsNullOrEmpty(objDC_UserLogins.LastLoginTime.ToString()))
                            {
                                return Redirect("~/User/UserDetails?UserId=" + objDC_UserLogins.UserId);
                            }                            
                        }
                        else
                        {
                            return Redirect("~/Physcian/PhyscianDetails?UserId=" + objDC_UserLogins.UserId);
                        }
                    }
                   // else
                    {
                        TempData["errorMessage"] = response.Message;
                    }
                }
            }
            return View();
        }
        #endregion

        #region Logout
        /// <summary>
        /// This method is used for Logout
        /// </summary>
        /// <param name="frmColl"></param>
        /// <returns></returns>

        public ActionResult Logout(FormCollection frmColl)
        {
            HttpContext.Session.Remove("Session");
            HttpContext.Session.Clear();           
            return RedirectToAction("Login");
        }
        #endregion

        public JsonResult UserLogins_Get(int UserId)
        {
            objBL_Account = new BL_Account();
            lst_DC_UserLogins = objBL_Account.UserLogins_Get(UserId);
            var result = Json(lst_DC_UserLogins);
            return result;
        }

        public JsonResult RoleName_Get()
        {
            objBL_Account = new BL_Account();
            lstdc_Roles = objBL_Account.RoleName_Get();
            var result = Json(lstdc_Roles);
            return result;
        }

        public ActionResult AdminLogin()
        {
            return View();
        }
    }
}