using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BettermeantHealth.DAL;
using BettermeantHealth.DataContract;
using System.Data.Common;
using System.Data;

namespace BettermeantHealth.BAL
{
   public class BL_InsurancePlan : BL_BaseClass
    {

        DataOperationResponse response = null;
        DatabaseHelper objDatabaseHelper = null;
        DC_InsurancePlan objDC_InsurancePlan = null;
        List<DC_InsurancePlan> lstDC_InsurancePlan = null;

        public DataOperationResponse InsurancePlan_AddUpdate(DC_InsurancePlan objDC_InsurancePlan)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                response = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pInsurancePlanId", objDC_InsurancePlan.InsurancePlanId == 0 ? DBNull.Value : (object)objDC_InsurancePlan.InsurancePlanId);
                objDatabaseHelper.AddParameter("pInsuranceCarrierId", objDC_InsurancePlan.InsuranceCarrierId == 0 ? DBNull.Value : (object)objDC_InsurancePlan.InsuranceCarrierId);
                objDatabaseHelper.AddParameter("pInsurancePlanName", string.IsNullOrEmpty(objDC_InsurancePlan.InsurancePlanName) ? DBNull.Value : (object)objDC_InsurancePlan.InsurancePlanName);
                objDatabaseHelper.AddParameter("pCreatedBy", objDC_InsurancePlan.CreatedBy == 0 ? DBNull.Value : (object)objDC_InsurancePlan.CreatedBy);
                objDatabaseHelper.AddParameter("pCreatedDate", DateTime.Now);
                objDatabaseHelper.AddParameter("pUpdatedBy", objDC_InsurancePlan.UpdatedBy == 0 ? DBNull.Value : (object)objDC_InsurancePlan.UpdatedBy);
                objDatabaseHelper.AddParameter("pUpdatedDate", DateTime.Now);
                object found  = objDatabaseHelper.ExecuteScalar(BL_DBRoutiens.SP_INSURANCEPLAN_ADDUPDATE, CommandType.StoredProcedure);
                int result = Convert.ToInt32(found);


                if (result >0)
                {
                    response.Code = GetSuccessCode;
                    response.Message = (objDC_InsurancePlan.InsurancePlanId == 0) ? " Insurance Details added Successfully" : "Insurance Details updated Successfully";

                }
                else
                {
                    response.Code = GetErrorCode;
                    response.Message = " Insurance  Number already exists";
                }
               
            }
            catch (Exception exce)
            {

                response.Code = GetErrorCode;
                response.Message = GetErrorMessage;
                throw exce;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }

            return response;
        }

        public List<DC_InsurancePlan> InsurancePlan_Get(int InsurancePlanId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDC_InsurancePlan = new List<DC_InsurancePlan>();
            try
            {
                objDatabaseHelper.AddParameter("pInsurancePlanId", InsurancePlanId == 0 ? DBNull.Value : (object)InsurancePlanId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BL_DBRoutiens.SP_INSURANCEPLAN_GET, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDC_InsurancePlan = new DC_InsurancePlan();
                        objDC_InsurancePlan.InsurancePlanId = reader.IsDBNull(reader.GetOrdinal("InsurancePlanId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsurancePlanId"));
                        objDC_InsurancePlan.InsuranceCarrierId = reader.IsDBNull(reader.GetOrdinal("InsuranceCarrierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsuranceCarrierId"));
                        objDC_InsurancePlan.InsurancePlanName = reader.IsDBNull(reader.GetOrdinal("InsurancePlanName")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsurancePlanName"));
                        objDC_InsurancePlan.InsuranceName = reader.IsDBNull(reader.GetOrdinal("InsuranceName")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsuranceName"));
                        objDC_InsurancePlan.CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? 0 : reader.GetInt32(reader.GetOrdinal("CreatedBy"));
                        if (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) == false)
                            objDC_InsurancePlan.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        objDC_InsurancePlan.UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? 0 : reader.GetInt32(reader.GetOrdinal("UpdatedBy"));
                        if (reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) == false)
                            objDC_InsurancePlan.UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"));
                        lstDC_InsurancePlan.Add(objDC_InsurancePlan);
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
            return lstDC_InsurancePlan;
        }

        public DataOperationResponse InsurancePlan_Delete(int InsurancePlanId)
        {
            response = new DataOperationResponse();
            objDatabaseHelper = new DatabaseHelper();
            lstDC_InsurancePlan = new List<DC_InsurancePlan>();
            try
            {
                objDatabaseHelper.AddParameter("pInsurancePlanId", InsurancePlanId == 0 ? DBNull.Value : (object)InsurancePlanId);
                int result = objDatabaseHelper.ExecuteNonQuery(BL_DBRoutiens.SP_INSURANCEPLAN_DELETE, CommandType.StoredProcedure);
                if (result >= -1)
                {
                    response.Code = GetSuccessCode;
                    response.Message = "Insurance plan deleted successfully";
                }
                else
                {
                    response.Code = GetErrorCode;
                    response.Message = GetErrorMessage;
                }

            }
            catch (Exception ex)
            {
                response.Code = GetErrorCode;
                response.Message = GetErrorMessage;
                throw ex;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
            return response;
        }

        public List<DC_InsurancePlan> InsurancePlan_GetByInsuranceCarrierId(int InsuranceCarrierId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDC_InsurancePlan = new List<DC_InsurancePlan>();
            try
            {
                objDatabaseHelper.AddParameter("pinsurancecarrierid", InsuranceCarrierId == 0 ? DBNull.Value : (object)InsuranceCarrierId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BL_DBRoutiens.SP_INSURANCEPLAN_GETBYINSURANCECARRIERID, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDC_InsurancePlan = new DC_InsurancePlan();
                        objDC_InsurancePlan.InsurancePlanId = reader.IsDBNull(reader.GetOrdinal("InsurancePlanId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsurancePlanId"));
                        objDC_InsurancePlan.InsuranceCarrierId = reader.IsDBNull(reader.GetOrdinal("InsuranceCarrierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsuranceCarrierId"));
                        objDC_InsurancePlan.InsurancePlanName = reader.IsDBNull(reader.GetOrdinal("InsurancePlanName")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsurancePlanName"));
                        lstDC_InsurancePlan.Add(objDC_InsurancePlan);
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
            return lstDC_InsurancePlan;
        }

    }
}
