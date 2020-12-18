using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyParking.DL;

namespace MyParking.BL
{
    public class UsedCarsBL
    {
        UsedCarsDL usedCarsDL = new UsedCarsDL();
        public string GetFirstSteetingName(string brandName)
        {
            return usedCarsDL.GetFirstSteetingName(brandName);
        }
    }
}
