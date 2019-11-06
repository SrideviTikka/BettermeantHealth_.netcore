using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettermeantHealth.BAL;
using BettermeantHealth.DataContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BettermeantHealth.Controllers
{
    public class PhyscianController : Controller
    {
        BL_Account objBL_Account = null;
        List<DC_UserLogins> lstDC_UserLogins = null;
        DC_UserLogins objDC_UserLogins = null;
        DataOperationResponse response = null;
        DC_PhyscianInsurance objDC_PhyscianInsurance = null;
        BL_Physcian objBL_Physcian = null;
        List<DC_PhyscianInsurance> lstDC_PhyscianInsurance = null;
        public IActionResult PhyscianDetails(int UserId)
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                objBL_Account = new BL_Account();
                lstDC_UserLogins = objBL_Account.UserLogins_Get(UserId);
                return View(lstDC_UserLogins);
            }
            return Redirect("~/Account/Login");
        }

        public IActionResult PhyscianInsurance_AddUpdate(DC_PhyscianInsurance dC_PhyscianInsurance)
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                objBL_Physcian = new BL_Physcian();
                objDC_PhyscianInsurance = new DC_PhyscianInsurance();
                objDC_PhyscianInsurance.PhyscianInsuranceId = dC_PhyscianInsurance.PhyscianInsuranceId;
                objDC_PhyscianInsurance.InsuranceCarrierId = dC_PhyscianInsurance.InsuranceCarrierId;
                objDC_PhyscianInsurance.UserId = DC_StaticConstants.Session_UserLogin.UserId;
                objDC_PhyscianInsurance.CreatedBy = DC_StaticConstants.Session_UserLogin.UserId;
                response = objBL_Physcian.PhyscianInsurance_AddUpdate(objDC_PhyscianInsurance);
                return Redirect("~/Physcian/PhyscianDetails?UserId=" + objDC_PhyscianInsurance.UserId);
            }
            return Redirect("~/Physcian/Login");
        }

        public DataOperationResponse PhyscianInsurance_Delete(int PhyscianInsuranceId)
        {
            objBL_Physcian = new BL_Physcian();
            response = new DataOperationResponse();
            response = objBL_Physcian.PhyscianInsurance_Delete(PhyscianInsuranceId);
            return response;
        }

        public JsonResult Physcian_Insurance_Get(int UserId, int PhyscianInsuranceId)
        {
            objBL_Physcian = new BL_Physcian();
            lstDC_PhyscianInsurance = objBL_Physcian.Physcian_Insurance_Get(UserId, PhyscianInsuranceId);
            var result = Json(lstDC_PhyscianInsurance);
            return result;
        }

        public JsonResult InsuranceCarrier_Physcian_Insurance_Get(int UserId, int PhyscianInsuranceId, int InsuranceCarrierId)
        {
            objBL_Physcian = new BL_Physcian();
            lstDC_PhyscianInsurance = objBL_Physcian.InsuranceCarrier_Physcian_Insurance_Get(UserId, PhyscianInsuranceId, InsuranceCarrierId);
            var result = Json(lstDC_PhyscianInsurance);
            return result;
        }
    }
}