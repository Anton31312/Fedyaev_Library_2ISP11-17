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

            double sum = 0;
            if (startDate.Add(DateTime.Today + startDate.Date) > startDate.AddDays(30))
            {
                sum = bookCost * 0.1;
            }

            return sum;
        }
    }
}
