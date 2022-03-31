using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY1.ClassHelper
{
    public class DebtRationClass
    {
        public static double Debt(double bookCost, DateTime startDate)
        {

            int dayCount = (DateTime.Now - startDate).Days;
            double sum = (dayCount - 30) * 0.01 * bookCost;
            if (bookCost <= 0)
            {
                return 0;
            }

            if (sum > 0)
            {
                return sum;
            }
            else
            {
                return 0;
            }

        }
    }
}
