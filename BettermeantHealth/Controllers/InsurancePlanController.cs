using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettermeantHealth.BAL;
using BettermeantHealth.DataContract;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BettermeantHealth.Controllers
{
    public class InsurancePlanController : Controller
    {
        DataOperationResponse response = null;
        BL_InsurancePlan objBL_InsurancePlan = null;
        DC_InsurancePlan objDC_InsurancePlan = null;
        List<DC_InsurancePlan> lstDC_InsurancePlan = null;
        List<DC_UserLogins> lstDC_UserLogins = null;
        DC_UserLogins objDC_UserLogins = null;
        DC_PatientInsurance objDC_PatientInsurance = null;
        List<DC_PatientInsurance> lstDC_PatientInsurance = null;
        public IActionResult InsurancePlan()
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                return View();
            }
            return Redirect("~/Account/Login");
        }

        public IActionResult InsurancePlan_AddUpdate(IFormCollection fromColl)
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                DC_UserLogins login_Userdetails = DC_StaticConstants.Session_UserLogin;

                objBL_InsurancePlan = new BL_InsurancePlan();
                response = new DataOperationResponse();
                objDC_InsurancePlan = new DC_InsurancePlan();

                if (!string.IsNullOrEmpty(fromColl["btnSave"]) && (string.Compare(fromColl["btnSave"], "Save") == 0))
                {

                    objDC_InsurancePlan.InsurancePlanId = string.IsNullOrEmpty(fromColl["hdnInsurancePlanID"]) ? 0 : Convert.ToInt32(fromColl["hdnInsurancePlanID"]);
                    objDC_InsurancePlan.InsuranceCarrierId = string.IsNullOrEmpty(fromColl["ddlInsuranceCarrier"]) ? 0 : Convert.ToInt32(fromColl["ddlInsuranceCarrier"]);
                    objDC_InsurancePlan.InsurancePlanName = fromColl["txtPlanName"];

                    if (objDC_InsurancePlan.InsurancePlanId == 0)
                    {
                        objDC_InsurancePlan.CreatedBy = login_Userdetails.UserId;
                    }
                    else
                    {
                        objDC_InsurancePlan.UpdatedBy = login_Userdetails.UserId ;
                    }
                    response = objBL_InsurancePlan.InsurancePlan_AddUpdate(objDC_InsurancePlan);

                    if (response.Code > 0)
                    {
                        TempData["successMessage"] = response.Message;
                        return Redirect("/InsurancePlan/InsurancePlan");
                    }
                    else
                    {
                        TempData["errorMessage"] = response.Message;
                        return Redirect("/InsurancePlan/InsurancePlan");
                    }
                }
            }
            return View();
        }

        public JsonResult InsurancePlan_Get(int InsurancePlanId)
        {
            objBL_InsurancePlan = new BL_InsurancePlan();
            lstDC_InsurancePlan = objBL_InsurancePlan.InsurancePlan_Get(InsurancePlanId);
            var result = Json(lstDC_InsurancePlan);
            return result;
        }

        public DataOperationResponse InsurancePlan_Delete(int InsurancePlanId)
        {
            objBL_InsurancePlan = new BL_InsurancePlan();
            response = new DataOperationResponse();
            response = objBL_InsurancePlan.InsurancePlan_Delete(InsurancePlanId);
            return response;
        }

        public JsonResult InsurancePlan_GetByInsuranceCarrierId(int InsuranceCarrierId)
        {
            objBL_InsurancePlan = new BL_InsurancePlan();
            lstDC_InsurancePlan = objBL_InsurancePlan.InsurancePlan_GetByInsuranceCarrierId(InsuranceCarrierId);
            var result = Json(lstDC_InsurancePlan);
            return result;
        }


    }
}