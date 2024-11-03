using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C971_Grant_Putnam.Models
{
    public static class DateEvaluator
    {
        public static bool AreDatesValid(DateTime start, DateTime end)
        {
            return start < end;
        }
    }
}
