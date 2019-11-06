using System;
using System.Collections.Generic;
using System.Text;

namespace BettermeantHealth.BAL
{
    public class BL_DBRoutiens
    {
        public const string USERLOGINS_GET = "userlogins_get";
        //public const string SP_USERLOGIN_ADDUPDATE = "\"SP_UserLogin_AddUpdate\"";
        //public const string SP_USER_LOGIN= "\"SP_User_Login\"";
        //"\"SP_UserLogins_Get\"";

        //Example : public const string PROCEDURE_NAME = "procedure_name";
        public const string SP_INSURANCECARRIER_GET = "\"SP_Get_InsuranceCarrier\"";
        public const string SP_INSURANCECARRIER_ADDUPDATE = "\"SP_InsuranceCarrier_AddUpdate\"";
        public const string SP_INSURANCECARRIER_DELETE = "\"SP_InsuranceCarrier_Delete\"";

        //Account
        public const string SP_ChangePassword = "\"SP_ChangePassword\"";
        public const string SP_USERLOGIN_ADDUPDATE = "\"SP_UserLogin_AddUpdate\"";
        public const string SP_USER_LOGIN = "\"SP_User_Login\"";
        public const string SP_USERLOGINS_GET = "\"SP_UserLogins_Get\"";
        public const string SP_USERLOGIN_UPDATE = "\"SP_UserLogin_Update\"";

        //Insurance Plan
        public const string SP_INSURANCEPLAN_ADDUPDATE = "\"SP_InsurancePlan_AddUpdate\"";
        public const string SP_INSURANCEPLAN_GET = "\"SP_Get_InsurancePlan\"";
        public const string SP_INSURANCEPLAN_GETBYINSURANCECARRIERID = "\"SP_InsurancePlan_GetByInsuranceCarrierId\"";
        public const string SP_INSURANCEPLAN_DELETE = "\"SP_InsurancePlan_Delete\"";

        //Patient Insurance
        public const string SP_PATIENTINSURANCE_ADDUPDATE = "\"SP_PatientInsurance_AddUpdate\"";
        public const string SP_PATIENTINSURANCE_GET = "\"SP_PatientInsurance_Get\"";
        public const string SP_PATIENTINSURANCE_DELETE = "\"SP_PatientInsurance_DELETE\"";

        //Roles
        public const string SP_ROLES_GET = "\"SP_Roles_Get\"";
        //Physcian Insurance
        public const string SP_PHYSCIANINSURANCE_ADDUPDATE = "\"SP_PhyscianInsurance_AddUpdate\"";
        public const string SP_PHYSCIAN_INSURANCE_GET = "\"SP_Physcian_Insurance_Get\"";
        public const string SP_PHYSCIAN_INSURANCE_DELETE = "\"SP_Physcian_Insurance_Delete\"";
        public const string SP_INSURANCECARRIER_GET_PHYSCIAN_INSURANCE = "sp_insurancecarrier_get_physcian_insurance";

    }
}
