using BettermeantHealth.DAL;
using BettermeantHealth.DataContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace BettermeantHealth.BAL
{
    public class BL_User : BL_BaseClass
    {
        DataOperationResponse response = null;
        DatabaseHelper objDatabaseHelper = null;
        List<DC_UserLogins> lst_DC_UserLogins = null;
        DC_UserLogins objDC_UserLogins = null;
        DC_PatientInsurance objDC_PatientInsurance = null;
        List<DC_PatientInsurance> lstDC_PatientInsurance = null;


        #region UserLogin_Update
        /// <summary>
        /// This region is used for User details update
        /// </summary>
        /// <returns></returns>
        public DataOperationResponse UserLogin_Update(DC_UserLogins dC_UserLogins)
        {
            response = new DataOperationResponse();
            objDatabaseHelper = new DatabaseHelper();

            try
            {
                objDatabaseHelper.AddParameter("puserid", dC_UserLogins.UserId == 0 ? DBNull.Value : (object)dC_UserLogins.UserId);
                objDatabaseHelper.AddParameter("pfirstname", string.IsNullOrEmpty(dC_UserLogins.FirstName) ? DBNull.Value : (object)dC_UserLogins.FirstName);
                objDatabaseHelper.AddParameter("plastname", string.IsNullOrEmpty(dC_UserLogins.LastName) ? DBNull.Value : (object)dC_UserLogins.LastName);
                objDatabaseHelper.AddParameter("pemailaddress", string.IsNullOrEmpty(dC_UserLogins.EmailAddress) ? DBNull.Value : (object)dC_UserLogins.EmailAddress);
                objDatabaseHelper.AddParameter("pmiddlename", string.IsNullOrEmpty(dC_UserLogins.MiddleName) ? DBNull.Value : (object)dC_UserLogins.MiddleName);
                objDatabaseHelper.AddParameter("pphonenumber", string.IsNullOrEmpty(dC_UserLogins.PhoneNumber) ? DBNull.Value : (object)dC_UserLogins.PhoneNumber);
                objDatabaseHelper.AddParameter("paddress", string.IsNullOrEmpty(dC_UserLogins.Address) ? DBNull.Value : (object)dC_UserLogins.Address);
                objDatabaseHelper.AddParameter("pcity", string.IsNullOrEmpty(dC_UserLogins.City) ? DBNull.Value : (object)dC_UserLogins.City);
                objDatabaseHelper.AddParameter("pstate", string.IsNullOrEmpty(dC_UserLogins.State) ? DBNull.Value : (object)dC_UserLogins.State);
                objDatabaseHelper.AddParameter("pzipcode", string.IsNullOrEmpty(dC_UserLogins.ZipCode) ? DBNull.Value : (object)dC_UserLogins.ZipCode);
                objDatabaseHelper.AddParameter("pupdatedby", dC_UserLogins.UpdatedBy == 0 ? DBNull.Value : (object)dC_UserLogins.UpdatedBy);
                objDatabaseHelper.AddParameter("proleid", dC_UserLogins.RoleId == 0 ? DBNull.Value : (object)dC_UserLogins.RoleId);
                objDatabaseHelper.AddParameter("pnpi_id", string.IsNullOrEmpty(dC_UserLogins.NPI_Id) ? DBNull.Value : (object)dC_UserLogins.NPI_Id);
                object found = objDatabaseHelper.ExecuteScalar(BL_DBRoutiens.SP_USERLOGIN_UPDATE, CommandType.StoredProcedure);
                int result = Convert.ToInt32(found);
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
        #endregion

        #region Patient Insurance Addupdate
        /// <summary>
        /// This region is used for Patient insurance details update
        /// </summary>
        /// <returns></returns>
        public DataOperationResponse PatientInsurance_AddUpdate(DC_PatientInsurance dC_PatientInsurance)
        {
            response = new DataOperationResponse();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDatabaseHelper.AddParameter("pPatientInsuranceId", dC_PatientInsurance.PatientInsuranceId == 0 ? DBNull.Value : (object)dC_PatientInsurance.PatientInsuranceId);
                objDatabaseHelper.AddParameter("pUserId", dC_PatientInsurance.UserId == 0 ? DBNull.Value : (object)dC_PatientInsurance.UserId);
                objDatabaseHelper.AddParameter("pInsuranceCarrierId", dC_PatientInsurance.InsuranceCarrierId == 0 ? DBNull.Value : (object)dC_PatientInsurance.InsuranceCarrierId);
                objDatabaseHelper.AddParameter("pInsurancePlanId", dC_PatientInsurance.InsurancePlanId == 0 ? DBNull.Value : (object)dC_PatientInsurance.InsurancePlanId);
                objDatabaseHelper.AddParameter("pInsuranceMemberId", string.IsNullOrEmpty(dC_PatientInsurance.InsuranceMemberId) ? DBNull.Value : (object)dC_PatientInsurance.InsuranceMemberId);
                objDatabaseHelper.AddParameter("pExpiryDate", dC_PatientInsurance.ExpiryDate == null ? DBNull.Value : (object)dC_PatientInsurance.ExpiryDate);
                objDatabaseHelper.AddParameter("pCreatedBy", dC_PatientInsurance.CreatedBy == 0 ? DBNull.Value : (object)dC_PatientInsurance.CreatedBy);
                int result = objDatabaseHelper.ExecuteNonQuery(BL_DBRoutiens.SP_PATIENTINSURANCE_ADDUPDATE, CommandType.StoredProcedure);
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
        #endregion

        public List<DC_PatientInsurance> PatientInsurance_Get(int UserId, int PatientInsuranceId)
        {
            objDatabaseHelper = new DatabaseHelper();
            lstDC_PatientInsurance = new List<DC_PatientInsurance>();
            try
            {
                objDatabaseHelper.AddParameter("puserid", UserId == 0 ? DBNull.Value : (object)UserId);
                objDatabaseHelper.AddParameter("ppatientinsuranceid", PatientInsuranceId == 0 ? DBNull.Value : (object)PatientInsuranceId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BL_DBRoutiens.SP_PATIENTINSURANCE_GET, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDC_PatientInsurance = new DC_PatientInsurance();
                        objDC_PatientInsurance.PatientInsuranceId = reader.IsDBNull(reader.GetOrdinal("PatientInsuranceId")) ? 0 : reader.GetInt32(reader.GetOrdinal("PatientInsuranceId"));
                        objDC_PatientInsurance.PatientId = reader.IsDBNull(reader.GetOrdinal("PatientId")) ? 0 : reader.GetInt32(reader.GetOrdinal("PatientId"));
                        objDC_PatientInsurance.InsurancePlanId = reader.IsDBNull(reader.GetOrdinal("InsurancePlanId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsurancePlanId"));
                        objDC_PatientInsurance.InsuranceCarrierId = reader.IsDBNull(reader.GetOrdinal("InsuranceCarrierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("InsuranceCarrierId"));
                        objDC_PatientInsurance.InsurancePlanName = reader.IsDBNull(reader.GetOrdinal("InsurancePlanName")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsurancePlanName"));
                        objDC_PatientInsurance.InsuranceName = reader.IsDBNull(reader.GetOrdinal("InsuranceName")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsuranceName"));
                        objDC_PatientInsurance.InsuranceMemberId = reader.IsDBNull(reader.GetOrdinal("InsuranceMemberId")) ? string.Empty : reader.GetString(reader.GetOrdinal("InsuranceMemberId"));
                        objDC_PatientInsurance.ExpiryDate = reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("ExpiryDate"));
                        lstDC_PatientInsurance.Add(objDC_PatientInsurance);
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
            return lstDC_PatientInsurance;
        }

        public DataOperationResponse PatientInsurance_Delete(int PatientInsuranceId)
        {
            response = new DataOperationResponse();
            objDatabaseHelper = new DatabaseHelper();
            try
            {
                objDatabaseHelper.AddParameter("pPatientInsuranceId", PatientInsuranceId == 0 ? DBNull.Value : (object)PatientInsuranceId);
                int result = objDatabaseHelper.ExecuteNonQuery(BL_DBRoutiens.SP_PATIENTINSURANCE_DELETE, CommandType.StoredProcedure);
                if (result > 0)
                {
                    response.Code = GetSuccessCode;
                    response.Message = "Patient insurance deleted successfully";
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
    }
}
