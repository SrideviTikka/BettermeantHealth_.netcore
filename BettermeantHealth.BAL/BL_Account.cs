using BettermeantHealth.DAL;
using BettermeantHealth.DataContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace BettermeantHealth.BAL
{
    public class BL_Account : BL_BaseClass
    {
        DatabaseHelper objDatabaseHelper = null;
        DC_UserLogins objDC_UserLogins = null;
        List<DC_UserLogins> lst_DC_UserLogins = null;
        List<DC_Roles> objDC_Roles = null;
        DC_Roles dc_Roles = null;

        #region Change Password
        /// <summary>
        /// This region is used for user Password changing.
        /// </summary>
        /// <param name="intUserId"></param>
        /// <param name="strOldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public DataOperationResponse ChangePassword(int intUserId, string strOldPassword, string strNewPassword)
        {
            DataOperationResponse dtResponse = new DataOperationResponse();
            DatabaseHelper dataHelper = new DatabaseHelper();
            try
            {
                dataHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
                dataHelper.AddParameter("pOldPassword", string.IsNullOrEmpty(strOldPassword) ? DBNull.Value : (object)strOldPassword);
                dataHelper.AddParameter("pNewPassword", string.IsNullOrEmpty(strNewPassword) ? DBNull.Value : (object)strNewPassword);
                dataHelper.AddParameter("pMessage", string.Empty, ParameterDirection.Output, DbType.String, 500);
                int intResult = dataHelper.ExecuteNonQuery(BL_DBRoutiens.SP_ChangePassword, CommandType.StoredProcedure);
                dtResponse.Message = Convert.ToString(dataHelper.Command.Parameters["pMessage"].Value);
                if (intResult > 0)
                    dtResponse.Code = GetSuccessCode;
                else
                    dtResponse.Code = GetErrorCode;
                return dtResponse;
            }
            catch (Exception excp)
            {
                dtResponse.Code = GetErrorCode;
                dtResponse.Message = excp.Message;
                return dtResponse;
            }
            finally
            {
                if (dataHelper != null)
                    dataHelper.Dispose();
            }
        }
        #endregion

        #region UserLogon
        /// <summary>
        /// This region is used for User Login
        /// </summary>
        /// <param name="strEmailAddress"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public DC_UserLogins UserLogon(string strEmailAddress, string strPassword,int RoleId)
        {
            try
            {
                objDatabaseHelper = new DatabaseHelper();
                objDC_UserLogins = new DC_UserLogins();
                objDatabaseHelper.AddParameter("pemailaddress", strEmailAddress);
                objDatabaseHelper.AddParameter("ppassword", strPassword);
                objDatabaseHelper.AddParameter("pRoleId", RoleId);
                DbDataReader reader = objDatabaseHelper.ExecuteReader(BL_DBRoutiens.SP_USER_LOGIN, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDC_UserLogins.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                        objDC_UserLogins.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                        objDC_UserLogins.RoleName = reader.IsDBNull(reader.GetOrdinal("RoleName")) ? string.Empty : reader.GetString(reader.GetOrdinal("RoleName"));
                        objDC_UserLogins.LastLoginTime = reader.IsDBNull(reader.GetOrdinal("LastLoginTime")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("LastLoginTime"));
                        objDC_UserLogins.EmailAddress = reader.IsDBNull(reader.GetOrdinal("EmailAddress")) ? string.Empty : reader.GetString(reader.GetOrdinal("EmailAddress"));
                        objDC_UserLogins.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                        objDC_UserLogins.RoleId = reader.IsDBNull(reader.GetOrdinal("RoleId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RoleId"));
                    }
                    objDC_UserLogins.Code = GetSuccessCode;
                }
                else
                {
                    objDC_UserLogins.Code = GetErrorCode;
                }

                return objDC_UserLogins;
            }
            catch (Exception excp)
            {
                objDC_UserLogins.Code = GetErrorCode;
                objDC_UserLogins.Message = excp.Message;
                return objDC_UserLogins;
            }
            finally
            {
                if (objDatabaseHelper != null)
                    objDatabaseHelper.Dispose();
            }
        }
        #endregion

        #region PatientSignupAdd
        /// <summary>
        /// This region is used for User Login
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        ///  /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public List<DC_UserLogins> PatientSignupAdd(int intUserId, string FirstName, string LastName, string EmailAddress,int RoleId)
        {
            DataOperationResponse dtResponse = new DataOperationResponse();
            DatabaseHelper dataHelper = new DatabaseHelper();
            lst_DC_UserLogins = new List<DC_UserLogins>();
            try
            {
                dataHelper.AddParameter("pUserId", intUserId == 0 ? DBNull.Value : (object)intUserId);
                dataHelper.AddParameter("pFirstName", string.IsNullOrEmpty(FirstName) ? DBNull.Value : (object)FirstName);
                dataHelper.AddParameter("pLastName", string.IsNullOrEmpty(LastName) ? DBNull.Value : (object)LastName);
                dataHelper.AddParameter("pEmailAddress", string.IsNullOrEmpty(EmailAddress) ? DBNull.Value : (object)EmailAddress);
                dataHelper.AddParameter("pRoleId", RoleId==0 ? DBNull.Value : (object)RoleId);
                DbDataReader reader = dataHelper.ExecuteReader(BL_DBRoutiens.SP_USERLOGIN_ADDUPDATE, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    {
                        while (reader.Read())
                        {
                            objDC_UserLogins = new DC_UserLogins();
                            objDC_UserLogins.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                            objDC_UserLogins.EmailAddress = reader.IsDBNull(reader.GetOrdinal("EmailAddress")) ? string.Empty : reader.GetString(reader.GetOrdinal("EmailAddress"));
                            objDC_UserLogins.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                            objDC_UserLogins.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                            objDC_UserLogins.Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? string.Empty : reader.GetString(reader.GetOrdinal("Password"));
                            lst_DC_UserLogins.Add(objDC_UserLogins);
                        }
                    }

                }
            }
            catch (Exception excp)
            {
                dtResponse.Code = GetErrorCode;
                dtResponse.Message = excp.Message;
                return lst_DC_UserLogins;
            }
            finally
            {
                if (dataHelper != null)
                    dataHelper.Dispose();
            }
            return lst_DC_UserLogins;
        }
        #endregion

        #region UserLogins_Get
        /// <summary>
        /// This region is used for User Login Get
        /// </summary>
        /// <param name="intUserId"></param>        
        /// <returns></returns>
        public List<DC_UserLogins> UserLogins_Get(int intUserId)
        {
            DataOperationResponse dtResponse = new DataOperationResponse();
            DatabaseHelper dataHelper = new DatabaseHelper();
            lst_DC_UserLogins = new List<DC_UserLogins>();
            try
            {
                dataHelper.AddParameter("puserid", intUserId == 0 ? DBNull.Value : (object)intUserId);
                DbDataReader reader = dataHelper.ExecuteReader(BL_DBRoutiens.SP_USERLOGINS_GET, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    {
                        while (reader.Read())
                        {
                            objDC_UserLogins = new DC_UserLogins();
                            objDC_UserLogins.UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId"));
                            objDC_UserLogins.EmailAddress = reader.IsDBNull(reader.GetOrdinal("EmailAddress")) ? string.Empty : reader.GetString(reader.GetOrdinal("EmailAddress"));
                            objDC_UserLogins.FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FirstName"));
                            objDC_UserLogins.LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("LastName"));
                            objDC_UserLogins.Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? string.Empty : reader.GetString(reader.GetOrdinal("Password"));
                            objDC_UserLogins.MiddleName = reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? string.Empty : reader.GetString(reader.GetOrdinal("MiddleName"));
                            objDC_UserLogins.Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? string.Empty : reader.GetString(reader.GetOrdinal("Address"));
                            objDC_UserLogins.PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("PhoneNumber"));
                            objDC_UserLogins.City = reader.IsDBNull(reader.GetOrdinal("City")) ? string.Empty : reader.GetString(reader.GetOrdinal("City"));
                            objDC_UserLogins.State = reader.IsDBNull(reader.GetOrdinal("State")) ? string.Empty : reader.GetString(reader.GetOrdinal("State"));
                            objDC_UserLogins.ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("ZipCode"));
                            objDC_UserLogins.NPI_Id= reader.IsDBNull(reader.GetOrdinal("NPI_Id")) ? string.Empty : reader.GetString(reader.GetOrdinal("NPI_Id"));
                            lst_DC_UserLogins.Add(objDC_UserLogins);
                        }
                    }

                }
            }
            catch (Exception excp)
            {
                dtResponse.Code = GetErrorCode;
                dtResponse.Message = excp.Message;
                return lst_DC_UserLogins;
            }
            finally
            {
                if (dataHelper != null)
                    dataHelper.Dispose();
            }
            return lst_DC_UserLogins;
        }
        #endregion

        public List<DC_Roles> RoleName_Get()
        {
            DataOperationResponse dtResponse = new DataOperationResponse();
            DatabaseHelper dataHelper = new DatabaseHelper();
            objDC_Roles = new List<DC_Roles>();
            try
            {
                DbDataReader reader = dataHelper.ExecuteReader(BL_DBRoutiens.SP_ROLES_GET, CommandType.StoredProcedure);
                if (reader.HasRows)
                {
                    {
                        while (reader.Read())
                        {
                            dc_Roles = new DC_Roles();
                            dc_Roles.RoleId = reader.IsDBNull(reader.GetOrdinal("RoleId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RoleId"));
                            dc_Roles.RoleName = reader.IsDBNull(reader.GetOrdinal("RoleName")) ? string.Empty : reader.GetString(reader.GetOrdinal("RoleName"));
                            objDC_Roles.Add(dc_Roles);
                        }
                    }

                }
            }
            catch (Exception excp)
            {
                dtResponse.Code = GetErrorCode;
                dtResponse.Message = excp.Message;

            }
            finally
            {
                if (dataHelper != null)
                    dataHelper.Dispose();
            }
            return objDC_Roles;
        }
    }
}
