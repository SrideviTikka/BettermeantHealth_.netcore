using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BettermeantHealth.BAL;
using BettermeantHealth.DataContract;
using Microsoft.AspNetCore.Http;
namespace BettermeantHealth.Controllers
{
    public class InsuranceCarrierController : Controller
    {
        BL_InsuranceCarrier objBL_InsuranceCarrier = null;
        DC_InsuranceCarrier objDC_InsuranceCarrier = null;
        List<DC_InsuranceCarrier> lstDC_InsuranceCarrier = null;
        DataOperationResponse objDataOperationResponse = null;
        DC_UserLogins logindetails = null;
        [HttpGet]
        public IActionResult InsuranceCarrier()
        {

            if (HttpContext.Session.GetString("Session") != null)
            {
                return View();
            }
            return Redirect("~/Account/Login");
        }
        public JsonResult Get_InsuranceCarrier(int InsuranceCarrierId)
        {
            lstDC_InsuranceCarrier = new List<DC_InsuranceCarrier>();
            BL_InsuranceCarrier objBL_InsuranceCarrier = new BL_InsuranceCarrier();
            lstDC_InsuranceCarrier = objBL_InsuranceCarrier.Get_InsuranceCarrier(InsuranceCarrierId);
            var result = Json(lstDC_InsuranceCarrier);
            return result;
        }

        public IActionResult AddupdateInsuranceCarrier(IFormCollection frmcollection)
        {
            if (HttpContext.Session.GetString("Session") != null)
            {
                logindetails = new DC_UserLogins();
                logindetails = DC_StaticConstants.Session_UserLogin;
                objBL_InsuranceCarrier = new BL_InsuranceCarrier();
                objDC_InsuranceCarrier = new DC_InsuranceCarrier();
                if (!string.IsNullOrEmpty(frmcollection["btnSave"]) && (string.Compare(frmcollection["btnSave"], "Save") == 0))
                {
                    objDC_InsuranceCarrier.InsuranceCarrierId = string.IsNullOrEmpty(frmcollection["hdnInsuranceCarrierId"]) ? 0 : Convert.ToInt32(frmcollection["hdnInsuranceCarrierId"]);
                    objDC_InsuranceCarrier.InsuranceName = frmcollection["txtInsuranceName"];
                    if (objDC_InsuranceCarrier.InsuranceCarrierId == 0)
                    {
                        objDC_InsuranceCarrier.CreatedBy = logindetails.UserId;
                    }
                  else
                    {
                        objDC_InsuranceCarrier.UpdatedBy = logindetails.UserId;
                    }
                    objDataOperationResponse = objBL_InsuranceCarrier.Addupdate(objDC_InsuranceCarrier);
                    if (objDataOperationResponse.Code > 0)
                    {
                        TempData["successMessage"] = objDataOperationResponse.Message;
                        return Redirect("/InsuranceCarrier/InsuranceCarrier");

                    }
                    else
                    {
                        TempData["errorMessage"] = objDataOperationResponse.Message;
                        return Redirect("/InsuranceCarrier/InsuranceCarrier");
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult DeleteInsuranceCarrier(int InsuranceCarrierId)
        {
            objDataOperationResponse = new DataOperationResponse();
            objBL_InsuranceCarrier = new BL_InsuranceCarrier();
            objDataOperationResponse = objBL_InsuranceCarrier.DeleteInsuranceCarrier(Convert.ToInt32(InsuranceCarrierId));
            var result = Json(objDataOperationResponse);
            return result;
        }
    }
}