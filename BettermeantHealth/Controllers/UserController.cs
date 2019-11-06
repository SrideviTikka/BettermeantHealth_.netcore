using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettermeantHealth.BAL;
using BettermeantHealth.DataContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BettermeantHealth.Controllers
{
    public class UserController : Controller
    {
        BL_Account objBL_Account = null;
        List<DC_UserLogins> lstDC_UserLogins = null;
        DC_UserLogins objDC_UserLogins = null;
        DataOperationResponse response = null;
        BL_User objBL_User = null;
        DC_PatientInsurance objDC_PatientInsurance = null;
        List<DC_PatientInsurance> lstDC_PatientInsurance = null;

        // GET: User
        public IActionResult UserDetails(int UserId)
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                objBL_Account = new BL_Account();
                lstDC_UserLogins = objBL_Account.UserLogins_Get(UserId);
                return View(lstDC_UserLogins);
            }
            return Redirect("~/Account/Login");
        }

        public ActionResult UserLogin_Update(IFormCollection frmcll)
        {
            //DC_StaticConstants.Session_UserLogin = HttpContext.Session.GetString("Session") == null ? default(DC_UserLogins) : JsonConvert.DeserializeObject<DC_UserLogins>(HttpContext.Session.GetString("Session"));
            if (HttpContext.Session.GetString("Session") != null)
            {
                DC_UserLogins login_Userdetails = DC_StaticConstants.Session_UserLogin;
                objBL_User = new BL_User();
                response = new DataOperationResponse();
                objDC_UserLogins = new DC_UserLogins();
                objDC_UserLogins.UserId = string.IsNullOrEmpty(frmcll["hdnUserId"]) ? 0 : Convert.ToInt32(frmcll["hdnUserId"]);
                objDC_UserLogins.FirstName = string.IsNullOrEmpty(frmcll["txtFirstName"]) ? string.Empty : frmcll["txtFirstName"].ToString();
                objDC_UserLogins.MiddleName = string.IsNullOrEmpty(frmcll["txtMiddleName"]) ? string.Empty : frmcll["txtMiddleName"].ToString();
                objDC_UserLogins.LastName = string.IsNullOrEmpty(frmcll["txtLastName"]) ? string.Empty : frmcll["txtLastName"].ToString();
                objDC_UserLogins.PhoneNumber = string.IsNullOrEmpty(frmcll["txtPhoneNumber"]) ? string.Empty : frmcll["txtPhoneNumber"].ToString();
                objDC_UserLogins.EmailAddress = string.IsNullOrEmpty(frmcll["txtEmailAddress"]) ? string.Empty : frmcll["txtEmailAddress"].ToString();
                objDC_UserLogins.Address = string.IsNullOrEmpty(frmcll["txtAddress"]) ? string.Empty : frmcll["txtAddress"].ToString();
                objDC_UserLogins.City = string.IsNullOrEmpty(frmcll["txtCity"]) ? string.Empty : frmcll["txtCity"].ToString();
                objDC_UserLogins.State = string.IsNullOrEmpty(frmcll["txtState"]) ? string.Empty : frmcll["txtState"].ToString();
                objDC_UserLogins.ZipCode = string.IsNullOrEmpty(frmcll["txtZipCode"]) ? string.Empty : frmcll["txtZipCode"].ToString();
                objDC_UserLogins.UpdatedBy = login_Userdetails.UserId;
                objDC_UserLogins.RoleId = login_Userdetails.RoleId;
               if(login_Userdetails.RoleName=="Physcian")
                {
                    objDC_UserLogins.NPI_Id = string.IsNullOrEmpty(frmcll["txtNPIId"]) ? string.Empty : frmcll["txtNPIId"].ToString();
                }
                response = objBL_User.UserLogin_Update(objDC_UserLogins);
                if (login_Userdetails.RoleName == "Physcian")
                {
                    return Redirect("~/Physcian/PhyscianDetails?UserId=" + objDC_UserLogins.UserId);
                }
                else
                {
                    return Redirect("~/User/UserDetails?UserId=" + objDC_UserLogins.UserId);
                }
            }
            return Redirect("~/User/UserDetails");
        }

        public IActionResult PatientInsurance_AddUpdate(IFormCollection frmcll)
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                DC_UserLogins login_Userdetails = DC_StaticConstants.Session_UserLogin;
                objBL_User = new BL_User();
                response = new DataOperationResponse();
                objDC_PatientInsurance = new DC_PatientInsurance();
                objDC_PatientInsurance.PatientInsuranceId = string.IsNullOrEmpty(frmcll["hdnPatientInsuranceId"]) ? 0 : Convert.ToInt32(frmcll["hdnPatientInsuranceId"]);
                objDC_PatientInsurance.UserId = string.IsNullOrEmpty(frmcll["hdnUserId"]) ? 0 : Convert.ToInt32(frmcll["hdnUserId"]);
                objDC_PatientInsurance.InsuranceCarrierId = string.IsNullOrEmpty(frmcll["ddlInsuranceCarrier"]) ? 0 : Convert.ToInt32(frmcll["ddlInsuranceCarrier"]);
                objDC_PatientInsurance.InsurancePlanId = string.IsNullOrEmpty(frmcll["ddlInsurancePlan"]) ? 0 : Convert.ToInt32(frmcll["ddlInsurancePlan"]);
                objDC_PatientInsurance.InsuranceMemberId = string.IsNullOrEmpty(frmcll["txtInsuranceMemberId"]) ? string.Empty : frmcll["txtInsuranceMemberId"].ToString();
                objDC_PatientInsurance.ExpiryDate = Convert.ToDateTime(frmcll["txtExpiryDate"]);
                objDC_PatientInsurance.CreatedBy = login_Userdetails.UserId;
                response = objBL_User.PatientInsurance_AddUpdate(objDC_PatientInsurance);
                return Redirect("~/User/UserDetails?UserId=" + objDC_PatientInsurance.UserId);
            }
            return Redirect("~/User/UserDetails");
        }

        public JsonResult PatientInsurance_Get(int UserId, int PatientInsuranceId)
        {
            objBL_User = new BL_User();
            lstDC_PatientInsurance = objBL_User.PatientInsurance_Get(UserId, PatientInsuranceId);
            var result = Json(lstDC_PatientInsurance);           
            return result;
        }

        public DataOperationResponse PatientInsurance_Delete(int PatientInsuranceId)
        {
            objBL_User = new BL_User();
            response = new DataOperationResponse();
            response = objBL_User.PatientInsurance_Delete(PatientInsuranceId);
            return response;
        }
    }
}