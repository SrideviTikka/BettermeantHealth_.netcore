using System;
using System.Collections.Generic;
using System.Text;

namespace BettermeantHealth.DataContract
{
    public class DC_PhyscianInsurance:DataOperationResponse
    {
        public int PhyscianInsuranceId { get; set; }
        public int PhyscianId { get; set; }
        public int InsuranceCarrierId { get; set; }
        public string InsuranceName { get; set; }
    }
}
