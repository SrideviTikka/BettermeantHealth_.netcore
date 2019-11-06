using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettermeantHealth.Models
{
    public class EmailAttributesModel
    {
        public string MessageBody { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string CC { get; set; }
    }
}
