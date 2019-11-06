using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using BettermeantHealth.DAL;
using BettermeantHealth.DataContract;

namespace BettermeantHealth.BAL
{
    public class BL_Physcian:BL_BaseClass
    {
        DC_PhyscianInsurance objDC_PhyscianInsurance = null;
        List<DC_PhyscianInsurance> lstDC_PhyscianInsurance = null;
        DataOperationResponse response = null;
        DatabaseHelper objDatabaseHelper = null;

        public DataOperationResponse PhyscianInsurance_AddUpdate(DC_PhyscianInsurance objPhyscianInsurance)
        {
            response = new DataOperationResponse();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDatabaseHelper.AddParameter("pPhyscianInsuranceId", objPhyscianInsurance.PhyscianInsuranceId == 0 ? DBNull.Value : (object)objPhyscianInsurance.PhyscianInsuranceId);
                objDatabaseHelper.AddParameter("pUserId", objPhyscianInsurance.UserId == 0 ? DBNull.Value : (object)objPhyscianInsurance.UserId);
                objDatabaseHelper.AddParameter("pInsuranceCarrierId", objPhyscianInsurance.InsuranceCarrierId == 0 ? DBNull.Value : (object)objPhyscianInsurance.InsuranceCarrierId);               
                objDatabaseHelper.AddParameter("pCreatedBy", objPhyscianInsurance.CreatedBy == 0 ? DBNull.Value : (object)objPhyscianInsurance.CreatedBy);
                int result = objDatabaseHelper.ExecuteNonQuery(BL_DBRoutiens.SP_PHYSCIANINSURANCE_ADDUPDATE, CommandType.StoredProcedure);
                if (result > 0)
                {
                    response.Code = GetSuccessCode;
                }
                else
                {
                    response.Code = GetErrorCode;
                }
            }
            catch (Exception excp)
            {
                response.Code = GetErrorCode;
                response.Message = excp.Message;
                return response;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
            return response;

        }

        public DataOperationResponse PhyscianInsurance_Delete(int PhyscianInsuranceId)
        {
            response = new DataOperationResponse();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDatabaseHelper.AddParameter("pphyscianinsuranceid", PhyscianInsuranceId == 0 ? DBNull.Value : (object)PhyscianInsuranceId);
                int result = objDatabaseHelper.ExecuteNonQuery(BL_DBRoutiens.SP_PHYSCIAN_INSURANCE_DELETE, CommandType.StoredProcedure);
                if (result > 0)
                {
                    response.Code = GetSuccessCode;
                    response.Message = "Physcian insurance deleted successfully";
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
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
            return response;
        }

        public List<DC_PhyscianInsurance> Physcian_Insurance_Get(int UserId, int PhyscianInsuranceId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDC_PhyscianInsurance = new List<DC_PhyscianInsurance>();
            try
            {
                objDatabaseHelper.AddParameter("puserid", UserId == 0 ? DBNull.Value : (object)UserId);
                objDatabaseHelper.AddParameter("pphyscianinsuranceid", PhyscianInsuranceId == 0 ? (object)DBNull.Value : (object)PhyscianInsuranceId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BL_DBRoutiens.SP_PHYSCIAN_INSURANCE_GET, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDC_PhyscianInsurance = new DC_PhyscianInsurance();
                        objDC_PhyscianInsurance.PhyscianInsuranceId = reader.IsDBNull(reader.GetOrdinal("PhyscianInsuranceId")) ? 0 : reader.GetInt32(reader.GetOrdinal("PhyscianInsuranceId"));
                        objDC_PhyscianInsurance.PhyscianId = reader.IsDBNull(reader.GetOrdinal("PhyscianId")) ? 0 : reader.GetInt32(reader.GetOrdinal("PhyscianId"));                        
                        objDC_PhyscianInsurance.InsuranceCarrierId = reader.IsDBNull(reader.GetOrdinal("InsuranceCarrierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsuranceCarrierId"));                        
                        objDC_PhyscianInsurance.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        lstDC_PhyscianInsurance.Add(objDC_PhyscianInsurance);
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
            return lstDC_PhyscianInsurance;
        }

        public List<DC_PhyscianInsurance> InsuranceCarrier_Physcian_Insurance_Get(int UserId, int PhyscianInsuranceId,int InsuranceCarrierId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDC_PhyscianInsurance = new List<DC_PhyscianInsurance>();
            try
            {
                objDatabaseHelper.AddParameter("puserid", UserId == 0 ? DBNull.Value : (object)UserId);
                objDatabaseHelper.AddParameter("pphyscianinsuranceid", PhyscianInsuranceId == 0 ? DBNull.Value : (object)PhyscianInsuranceId);
                objDatabaseHelper.AddParameter("pinsurancecarrierid", InsuranceCarrierId == 0 ? DBNull.Value : (object)InsuranceCarrierId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BL_DBRoutiens.SP_INSURANCECARRIER_GET_PHYSCIAN_INSURANCE, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDC_PhyscianInsurance = new DC_PhyscianInsurance();
                        objDC_PhyscianInsurance.PhyscianInsuranceId = reader.IsDBNull(reader.GetOrdinal("PhyscianInsuranceId")) ? 0 : reader.GetInt32(reader.GetOrdinal("PhyscianInsuranceId"));
                        objDC_PhyscianInsurance.PhyscianId = reader.IsDBNull(reader.GetOrdinal("PhyscianId")) ? 0 : reader.GetInt32(reader.GetOrdinal("PhyscianId"));
                        objDC_PhyscianInsurance.InsuranceCarrierId = reader.IsDBNull(reader.GetOrdinal("InsuranceCarrierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsuranceCarrierId"));
                      //  objDC_PhyscianInsurance.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        objDC_PhyscianInsurance.InsuranceName = reader.IsDBNull(reader.GetOrdinal("InsuranceName")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsuranceName"));
                        lstDC_PhyscianInsurance.Add(objDC_PhyscianInsurance);
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
            return lstDC_PhyscianInsurance;
        }
    }
}
