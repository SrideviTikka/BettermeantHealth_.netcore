using System;
using System.Collections.Generic;
using System.Text;

namespace BettermeantHealth.DataContract
{
    public class DC_PatientInsurance : DataOperationResponse
    {
        public int PatientInsuranceId { get; set; }
        public int PatientId { get; set; }
        public int InsuranceCarrierId { get; set; }
        public int InsurancePlanId { get; set; }
        public string InsuranceMemberId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string InsurancePlanName { get; set; }
        public string InsuranceName { get; set; }
    }
}
