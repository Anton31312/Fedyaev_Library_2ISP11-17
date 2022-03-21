using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY1.EF
{
    public partial class BookRental
    {
        public string GetDebt { get => $"Процент остатка: {DebtRatio}"; }

        public string GetColor

        {
            get
            {
                if (DebtRatio > 0)
                {
                    return "#ff0000";
                }
                else
                {
                    return "#ffffff";
                }
            }
        }

    }
}
