using System;
using System.Collections.Generic;
using System.Text;

namespace BettermeantHealth.DataContract
{
  public  class DC_InsuranceCarrier:DataOperationResponse
    {
        public int InsuranceCarrierId { get; set; }
        public string InsuranceName { get; set; }
    }
}
