using System;

namespace BettermeantHealth.DataContract
{
    public class DataOperationResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }
}
