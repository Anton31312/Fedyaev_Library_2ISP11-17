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
        public static decimal Debt(double bookCost, DateTime startDate, DateTime endDate)
        {

            double sum = 0;
            DateTime date;
            if (endDate == null)
            {
                return 0;   
            }
            if (Convert.ToInt32(endDate.Day - startDate.Day) < 30)
            {
                return 0;
            }
            if (Convert.ToInt32(DateTime.Now.Day - startDate.Day) > 30)
            {
                int i = 1;
                while (i != Convert.ToInt32(DateTime.Now.Date - startDate.Date) - 30)
                {
                    sum = bookCost * 0.01;
                }
            }

            return Convert.ToDecimal(sum);
        }
    }
}
