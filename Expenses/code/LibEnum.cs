using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expenses
{
    public class LibEnum
    {
        public enum eEventTypes
        {
            ErrorEvent = 1,
        }

        public enum eFormTypes
        {
            CalendarForm = 1,
            ReportForm = 2,
        }

        public enum eReportTypes
        {
            SumByMonth = 2,
            SumByYTD = 3,
            SumByYear = 4,
        }

    }
}