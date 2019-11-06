using System;
using System.Collections.Generic;
using System.Text;

namespace BettermeantHealth.DataContract
{
   public class DC_InsurancePlan : DataOperationResponse
    {
        public int InsurancePlanId { get; set; }
        public int InsuranceCarrierId { get; set; }
        public string InsurancePlanName { get; set; }
        public string InsuranceName { get; set; }
    }
}
