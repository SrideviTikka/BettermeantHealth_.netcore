using System;

namespace BettermeantHealth.BAL
{
    public class BL_BaseClass
    {
        public int GetSuccessCode
        {
            get { return 1; }
        }

        public int GetErrorCode
        {
            get { return -1; }
        }
        public string GetErrorMessage
        {
            get { return "Something went wrong!, try after sometime"; }
        }
    }
}
