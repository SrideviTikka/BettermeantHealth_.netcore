using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using BettermeantHealth.DAL;
using BettermeantHealth.DataContract;
namespace BettermeantHealth.BAL
{
   public class BL_InsuranceCarrier: BL_BaseClass
    {
        DC_InsuranceCarrier objDC_InsuranceCarrier = null;
        DatabaseHelper objDatabaseHelper = null;
        DataOperationResponse response = null;
        List<DC_InsuranceCarrier> lstDC_InsuranceCarrier = null;
        public List<DC_InsuranceCarrier> Get_InsuranceCarrier(int InsuranceCarrierId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDC_InsuranceCarrier = new List<DC_InsuranceCarrier>();
            try
            {
                objDatabaseHelper.AddParameter("pInsuranceCarrierId", InsuranceCarrierId == 0 ? DBNull.Value : (object)InsuranceCarrierId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BL_DBRoutiens.SP_INSURANCECARRIER_GET, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDC_InsuranceCarrier = new DC_InsuranceCarrier();
                        objDC_InsuranceCarrier.InsuranceCarrierId = reader.IsDBNull(reader.GetOrdinal("InsuranceCarrierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsuranceCarrierId"));
                        objDC_InsuranceCarrier.InsuranceName = reader.IsDBNull(reader.GetOrdinal("InsuranceName")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsuranceName"));
                        objDC_InsuranceCarrier.CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? 0 : reader.GetInt32(reader.GetOrdinal("CreatedBy"));
                        if (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) == false)
                        {
                            objDC_InsuranceCarrier.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        }
                        objDC_InsuranceCarrier.UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? 0 : reader.GetInt32(reader.GetOrdinal("UpdatedBy"));
                        if (reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) == false)
                        {
                            objDC_InsuranceCarrier.UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"));
                        }
                        lstDC_InsuranceCarrier.Add(objDC_InsuranceCarrier);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
            return lstDC_InsuranceCarrier;
        }
        public DataOperationResponse Addupdate(DC_InsuranceCarrier objDC_InsuranceCarrier)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                response = new DataOperationResponse();
                objDatabaseHelper.AddParameter("pInsuranceCarrierId", objDC_InsuranceCarrier.InsuranceCarrierId == 0 ? DBNull.Value : (object)objDC_InsuranceCarrier.InsuranceCarrierId);
                objDatabaseHelper.AddParameter("pInsuranceName", string.IsNullOrEmpty(objDC_InsuranceCarrier.InsuranceName) ? DBNull.Value : (object)objDC_InsuranceCarrier.InsuranceName);
                objDatabaseHelper.AddParameter("pCreatedBy", objDC_InsuranceCarrier.CreatedBy == 0 ? DBNull.Value : (object)objDC_InsuranceCarrier.CreatedBy);
                objDatabaseHelper.AddParameter("pUpdatedBy", objDC_InsuranceCarrier.UpdatedBy == 0 ? DBNull.Value : (object)objDC_InsuranceCarrier.UpdatedBy);
                object found= objDatabaseHelper.ExecuteScalar(BL_DBRoutiens.SP_INSURANCECARRIER_ADDUPDATE, CommandType.StoredProcedure);
                int result = Convert.ToInt32(found);
                if (result >0)
                {
                    response.Code = GetSuccessCode;

                    response.Message = objDC_InsuranceCarrier.InsuranceCarrierId == 0 ? "InsuranceCarrier  Added Successfully" : "InsuranceCarrier  Updated Successfully";
                }
                else
                {
                    response.Code = GetErrorCode;
                    response.Message = "This InsuranceCarrier ID is already exists, Please try with some other InsuranceCarrier ID ";
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return response;
        }
        public DataOperationResponse DeleteInsuranceCarrier(int intInsuranceCarrierId)
        {
            try
            {
                response = new DataOperationResponse();
                objDatabaseHelper = new DatabaseHelper();
                objDatabaseHelper.AddParameter("pInsuranceCarrierId", intInsuranceCarrierId == 0 ? DBNull.Value : (object)intInsuranceCarrierId);
                object found = objDatabaseHelper.ExecuteScalar(BL_DBRoutiens.SP_INSURANCECARRIER_DELETE, CommandType.StoredProcedure);
                int intResult = Convert.ToInt32(found);
                if (intResult > 0)
                {
                    response.Code = GetSuccessCode;
                    response.Message = "InsuranceCarrier  Deleted Successfully";
                }
                else
                {
                    response.Code = GetErrorCode;
                    response.Message = "Unable to Delete InsuranceCarrier, Please try again";
                }

                // return response;
            }
            catch (Exception exec)
            {
                throw exec;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
            return response;
        }
    }
}



    

