using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expenses
{
    public class LibStruc
    {
        public class strucExpensesViewData
        {
           public DateTime YmdAdd { get; set; }
           public Int32 ExpId { get; set; }
           public decimal AmtExp  { get; set; }
           public string CtgName { get; set; }
           public string SubCtgName { get; set; }
           public string ExpDesc { get; set; }
        }

        public class strucExpensesCalendarSummary
        {
            public string amtReceived { get; set; }
            public string amtSpended { get; set; }
            public string amtTotal { get; set; }
            public string amtYTD { get; set; }
            public string amtYearEnd { get; set; }
        }

        public class strucExpensesReportData
        {
            //  nYear      nMonth  
            public Int16 SortOrder {get; set;}
            public Int16 GroupOrder {get; set;}
            public Int32 CtgId {get; set;}
            public string CtgName  {get; set;}                                              
            public Int32 SubCtgID {get; set;}
            public string SubCtgName {get; set;}
            public decimal SumAmtExp { get; set; }
        }
    }

  
}