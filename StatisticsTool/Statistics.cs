using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsTool
{
    public abstract class Statistics
    {

        protected decimal getAverage(int sum, int occurs)
        {
            if (occurs == 0)
            {
                return 0;
            }
            return sum / occurs;
        }
        protected decimal getAverage(double sum, double occurs)
        {
            if (occurs == 0)
            {
                return 0;
            }
            return (decimal)(sum / occurs);
        }
        protected decimal getAverage(long sum, long occurs)
        {
            if (occurs == 0)
            {
                return 0;
            }
            return sum / occurs;
        }
        protected decimal getAverage(float sum, float occurs)
        {
            if (occurs == 0)
            {
                return 0;
            }
            return (decimal)(sum / occurs);
        }
        protected decimal getAverage(decimal sum, decimal occurs)
        {
            if (occurs == 0)
            {
                return 0;
            }
            return sum / occurs;
        }

    }
}
