using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.EntityClient;
using System.Web.Services;
using System.Web.Security;

namespace Expenses
{
    public class LibDB
    {
        public static List<Expenses.data.exp_ctg> getCtgList(Int32 userID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                db.Connection.Open();

                var q1 = (from c in db.exp_ctg orderby c.CtgName where c.UserId == userID select c).ToList();

                //check if user already created custom list
                if (q1 != null && q1.Count > 0)
                {
                    return q1;
                }
                else
                {
                    return db.exp_ctg.Where(x => x.UserId == null).OrderBy(y => y.CtgName).ToList();
                }
            }
        }

        public static List<Expenses.data.exp_subctg> getSubCtgList(Int32 ctgID, bool excludeSelected)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                db.Connection.Open();

                var q1 = (from s in getSubCtgList(ctgID) select s.SubCtgId).ToList();

                List<Int32> lstIDs = new List<Int32>();

                foreach (Int32 item in q1.ToList())
                {
                    lstIDs.Add(item);
                }

                var q2 = (from s in db.exp_subctg
                          orderby s.SubCtgName
                          where !lstIDs.Contains(s.SubCtgId)
                          select s);

                return q2.ToList();

                // return q.Cast<Expenses.data.exp_subctg>().ToList();
            }
        }

        public static List<Expenses.data.exp_subctg> getSubCtgList(Int32 ctgID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                db.Connection.Open();

                var q = (from l in db.exp_ctglink
                         join s in db.exp_subctg
                             on l.SubCtgId equals s.SubCtgId
                         orderby s.SubCtgName
                         where l.CtgId == ctgID
                         select s);

                return q.ToList();

               // return q.Cast<Expenses.data.exp_subctg>().ToList();
            }
        }

        public static List<Expenses.LibStruc.strucExpensesViewData> getExpensesMonthlyData(DateTime calendarDate, Int32 ctgID,
                                                                                           Int32 subCtgID, Int32 userID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    DateTime startDate, endDate;
                    startDate = calendarDate.AddDays(-10);
                    endDate = calendarDate.AddDays(40);

                    if (ctgID > 0 && subCtgID == 0)
                    {
                        return (from r in db.exp_record
                                join c in db.exp_ctg on r.CtgId equals c.CtgId
                                join s in db.exp_subctg on r.SubCtgId equals s.SubCtgId
                                orderby c.CtgName, s.SubCtgName, r.AmtExp descending
                                where (r.UserId == userID && r.CtgId == ctgID && r.YmdAdd >= startDate && r.YmdAdd <= endDate)
                                select new LibStruc.strucExpensesViewData
                                {
                                    YmdAdd = r.YmdAdd,
                                    ExpId = r.ExpId,
                                    AmtExp = r.AmtExp,
                                    CtgName = c.CtgName,
                                    SubCtgName = s.SubCtgName,
                                    ExpDesc = r.ExpDesc
                                }).ToList();

                    }
                    else if (ctgID > 0 && subCtgID > 0)
                    {
                        return (from r in db.exp_record
                                join c in db.exp_ctg on r.CtgId equals c.CtgId
                                join s in db.exp_subctg on r.SubCtgId equals s.SubCtgId
                                orderby c.CtgName, s.SubCtgName, r.AmtExp descending
                                where (r.UserId == userID && r.CtgId == ctgID
                                       && r.SubCtgId == subCtgID && r.YmdAdd >= startDate && r.YmdAdd <= endDate)
                                select new LibStruc.strucExpensesViewData
                                {
                                    YmdAdd = r.YmdAdd,
                                    ExpId = r.ExpId,
                                    AmtExp = r.AmtExp,
                                    CtgName = c.CtgName,
                                    SubCtgName = s.SubCtgName,
                                    ExpDesc = r.ExpDesc
                                }).ToList();
                    }
                    else
                    {
                        return (from r in db.exp_record
                                join c in db.exp_ctg on r.CtgId equals c.CtgId
                                join s in db.exp_subctg on r.SubCtgId equals s.SubCtgId
                                orderby c.CtgName, s.SubCtgName, r.AmtExp descending
                                where (r.UserId == userID && r.YmdAdd >= startDate && r.YmdAdd <= endDate)
                                select new LibStruc.strucExpensesViewData
                                {
                                    YmdAdd = r.YmdAdd,
                                    ExpId = r.ExpId,
                                    AmtExp = r.AmtExp,
                                    CtgName = c.CtgName,
                                    SubCtgName = s.SubCtgName,
                                    ExpDesc = r.ExpDesc
                                }).ToList();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                    //Console.WriteLine("{0} Exception caught.", e);
                }
            }
        }

        public static List<Expenses.LibStruc.strucExpensesReportData> getExpensesReportByDatesRange(
                                                                        DateTime dtFrom, DateTime dtTo, Int32 userID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                db.Connection.Open();

                string sFrom = string.Format("{0:yyyyMMdd}", dtFrom);
                string sTo = string.Format("{0:yyyyMMdd}", dtTo);

                var q = (from x in db.proc_exp_rpt_sum_dates_range(userID, sFrom, sTo)
                         select new LibStruc.strucExpensesReportData 
                         { GroupOrder = Convert.ToInt16(x.GroupOrder), SortOrder = Convert.ToInt16(x.SortOrder), 
                             CtgId = Convert.ToInt32(x.CtgId), CtgName = x.CtgName, 
                              SubCtgID = Convert.ToInt32(x.SubCtgID),
                              SubCtgName = x.SubCtgName, SumAmtExp = Convert.ToDecimal(x.SumAmtExp)
                           }).ToList();
                return q;
            }
        }

        //delete expenses category --> sub-category link in ctglink table
        public static void deleteExpensesCtgLink(Int32 ctgID, Int32 subCtgID, Int32 userID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    var l = db.exp_ctglink.FirstOrDefault(x => x.CtgId == ctgID && x.SubCtgId == subCtgID && x.UserId == userID);

                    if (l != null && l.SubCtgId != 1 && l.UserId != null)
                    {
                        db.DeleteObject(l);
                        db.SaveChanges();
                    }
                    
                }
                catch (Exception e)
                {
                    throw e;
                    //Console.WriteLine("{0} Exception caught.", e);
                }
            }
        }
        //add new expenses category --> sub-category link in ctglink table
        public static void addnewExpensesCtgLink(Int32 ctgID, Int32 subCtgID, Int32 userID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    var newCtgLink = new Expenses.data.exp_ctglink();
                    newCtgLink.SubCtgId = subCtgID;
                    newCtgLink.CtgId= ctgID;
                    newCtgLink.UserId = userID;

                    db.exp_ctglink.AddObject(newCtgLink);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                    //Console.WriteLine("{0} Exception caught.", e);
                }
            }
        }
        //delete expenses record by record id
        public static bool deleteExpensesRecordByID(Int32 recordID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    var rec = db.exp_record.FirstOrDefault(x => x.ExpId == recordID);

                    db.DeleteObject(rec);
                    db.SaveChanges();

                    var recs = (from x in db.exp_record where x.ExpSchemeId == recordID select x).ToList();

                    if (recs != null && recs.Count > 0)
                    {
                        foreach (data.exp_record r in recs)
                        {
                            db.DeleteObject(r);
                        }

                        db.SaveChanges();
                    }

                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                    //Console.WriteLine("{0} Exception caught.", e);
                }
            }
        }
        //update existing expenses record
        public static bool updateExpensesRecord(Int32 expID, Int32 userID, Decimal amtEXP, DateTime ymdADD, 
                                                Int32 ctgID, Int32 subctgID, string expDESC,Int16 monthNBR)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    var oldRec = db.exp_record.SingleOrDefault(p => p.ExpId == expID);
                    oldRec.UserId = userID;
                    oldRec.AmtExp = amtEXP;
                    oldRec.YmdAdd = ymdADD;
                    oldRec.CtgId = ctgID;
                    oldRec.SubCtgId = subctgID;
                    oldRec.ExpDesc = expDESC.Trim();

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    saveError(ex, userID);
                    return false;
                }
            }
        }
        //add new expenses record 
        public static bool addnewExpensesRecord(Int32 userID, Decimal amtEXP, DateTime ymdADD, 
                                                Int32 ctgID, Int32 subctgID, string expDESC,Int16 monthNBR)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    var newRec = new Expenses.data.exp_record();
                    newRec.UserId = userID;
                    newRec.AmtExp = amtEXP;
                    newRec.YmdAdd = ymdADD;
                    newRec.CtgId = ctgID;
                    newRec.SubCtgId = subctgID;

                    if (monthNBR > 1)
                    {
                        newRec.ExpDesc = expDESC.Trim() + " (added monthly through - " + ymdADD.AddMonths(monthNBR).ToString("MM/dd/yyyy") + ")";
                    }
                    else
                    {
                        newRec.ExpDesc = expDESC.Trim();
                    }

                    db.exp_record.AddObject(newRec);
                    db.SaveChanges();

                    //add related record to next number of month selected by user
                    if (monthNBR > 1)
                        for (int i = 1; i <= monthNBR; i++)
                        {
                            var newRec2 = new Expenses.data.exp_record();
                            newRec2.UserId = userID;
                            newRec2.AmtExp = amtEXP;
                            newRec2.YmdAdd = ymdADD.AddMonths(i);
                            newRec2.CtgId = ctgID;
                            newRec2.SubCtgId = subctgID;
                            newRec2.ExpDesc = expDESC.Trim();
                            newRec2.ExpSchemeId = newRec.ExpId;
                            db.exp_record.AddObject(newRec2);
                        }

                    db.SaveChanges();
                   

                    return true;
                }
                catch (Exception ex)
                {
                    saveError(ex, userID);
                    return false;
                }
            }
        }
        //select an existing record by ID
        public static Expenses.data.exp_record selectExpensesRecordByID(Int32 expID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    var objRecord = db.exp_record.FirstOrDefault(x => x.ExpId == expID);

                    if (objRecord != null)
                    {
                        return objRecord;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    saveError(ex, LibCode.getExpensesUserID());
                    return null;
                }
            }
        }
        //select expenses within selected day
        public static List<LibStruc.strucExpensesViewData> selectExpensesRecordByDate(DateTime ymdADD, Int32 userID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    var q = (from r in db.exp_record
                                  orderby r.ExpId descending
                                  where r.YmdAdd == ymdADD && r.UserId == userID 
                                  select new LibStruc.strucExpensesViewData
                                    { ExpId = r.ExpId, YmdAdd = r.YmdAdd, AmtExp = r.AmtExp, CtgName = r.exp_ctg.CtgName, 
                                       SubCtgName = r.exp_subctg.SubCtgName, ExpDesc = r.ExpDesc }).ToList();

                    if (q != null && q.Count > 0)
                    {
                        return q;
                    }
                    else
                    {
                        return null;
                    }
                
                }
                catch (Exception ex)
                {
                    saveError(ex, userID);
                    return null;
                }
            }
        }
       
        //select expenses summary by month year for month / y-t-d / end of the year
        public static LibStruc.strucExpensesCalendarSummary selectExpensesSummary(int m, int y, Int32 ctgID, Int32 subCtgID, Int32 userID)
        {
           using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                     db.Connection.Open();

                     LibStruc.strucExpensesCalendarSummary strucSummary = new LibStruc.strucExpensesCalendarSummary();

                     //assign all values
                     decimal _amtReceived = 0, _amtSpended = 0, _amtTotal = 0, _amtYTD = 0, _amtYearEnd = 0;

                     if (ctgID > 0 && subCtgID == 0)
                     { // return summary for selected ctg and all sub-ctg within selected ctg group
                        var q1a = (from r in db.exp_record
                               where r.UserId == userID && r.CtgId == ctgID 
                               && r.AmtExp >= 0 && r.YmdAdd.Month == m && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        var q1b = (from r in db.exp_record
                               where r.UserId == userID && r.CtgId == ctgID 
                               && r.AmtExp < 0 && r.YmdAdd.Month == m && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        var q1c = (from r in db.exp_record
                               where r.UserId == userID && r.CtgId == ctgID 
                               && r.YmdAdd.Month <= m && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        var q1d = (from r in db.exp_record
                               where r.UserId == userID && r.CtgId == ctgID 
                               && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        if (q1a.Count > 0) { _amtReceived = q1a.Sum(x => x.amount); }
                        if (q1b.Count > 0) { _amtSpended = q1b.Sum(x => x.amount); }
                        if (q1c.Count > 0) { _amtYTD = q1c.Sum(x => x.amount); }
                        if (q1d.Count > 0) { _amtYearEnd = q1d.Sum(x => x.amount); }

                     }
                     else if (ctgID > 0 && subCtgID > 0)
                     {  // return summary for selected ctg and selected sub-ctg
                         var q2a = (from r in db.exp_record
                                    where r.UserId == userID && r.CtgId == ctgID && r.SubCtgId == subCtgID
                                    && r.AmtExp >= 0 && r.YmdAdd.Month == m && r.YmdAdd.Year == y
                                    group r by r.AmtExp into t
                                    select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                         var q2b = (from r in db.exp_record
                                    where r.UserId == userID && r.CtgId == ctgID && r.SubCtgId == subCtgID
                                    && r.AmtExp < 0 && r.YmdAdd.Month == m && r.YmdAdd.Year == y
                                    group r by r.AmtExp into t
                                    select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                         var q2c = (from r in db.exp_record
                                    where r.UserId == userID && r.CtgId == ctgID && r.SubCtgId == subCtgID
                                    && r.YmdAdd.Month <= m && r.YmdAdd.Year == y
                                    group r by r.AmtExp into t
                                    select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                         var q2d = (from r in db.exp_record
                                    where r.UserId == userID && r.CtgId == ctgID && r.SubCtgId == subCtgID
                                    && r.YmdAdd.Year == y
                                    group r by r.AmtExp into t
                                    select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                         if (q2a.Count > 0) { _amtReceived = q2a.Sum(x => x.amount); }
                         if (q2b.Count > 0) { _amtSpended = q2b.Sum(x => x.amount); }
                         if (q2c.Count > 0) { _amtYTD = q2c.Sum(x => x.amount); }
                         if (q2d.Count > 0) { _amtYearEnd = q2d.Sum(x => x.amount); }
                     }
                     else
                     {  //return summary for all ctg and sub-ctg
                        var q3a = (from r in db.exp_record
                               where r.UserId == userID && r.AmtExp >= 0 && r.YmdAdd.Month == m && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        var q3b = (from r in db.exp_record
                               where r.UserId == userID && r.AmtExp < 0 && r.YmdAdd.Month == m && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        var q3c = (from r in db.exp_record
                               where r.UserId == userID && r.YmdAdd.Month <= m && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        var q3d = (from r in db.exp_record
                               where r.UserId == userID && r.YmdAdd.Year == y
                               group r by r.AmtExp into t
                               select new { amount = t.Sum(s => s.AmtExp) }).ToList();

                        if (q3a.Count > 0) { _amtReceived = q3a.Sum(x => x.amount); }
                        if (q3b.Count > 0) { _amtSpended = q3b.Sum(x => x.amount); }
                        if (q3c.Count > 0) { _amtYTD = q3c.Sum(x => x.amount); }
                        if (q3d.Count > 0) { _amtYearEnd = q3d.Sum(x => x.amount); }
                     }

                     _amtTotal = _amtReceived + _amtSpended;

                     strucSummary.amtReceived = String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", _amtReceived);
                     strucSummary.amtSpended = String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", _amtSpended);
                     strucSummary.amtTotal = String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", _amtTotal);
                     strucSummary.amtYTD = String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", _amtYTD);
                     strucSummary.amtYearEnd = String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", _amtYearEnd);

                     return strucSummary;

                }
                catch (Exception ex)
                {
                    saveError(ex, userID);
                    return null;
                }
            }
        }
        //save an error as event into event table
        public static void saveError(Exception ex, Int32 userID)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                db.Connection.Open();

                Expenses.data.exp_event oErr = new Expenses.data.exp_event();

                oErr.EventDesc = Convert.ToString(ex);
                oErr.EventTypeId = Convert.ToInt32(LibEnum.eEventTypes.ErrorEvent);
                oErr.UserId = userID;

                db.exp_event.AddObject(oErr);
                db.SaveChanges();
            }

        }

        public class NetFourMembershipProvider : SqlMembershipProvider
        {
            public string GetClearTextPassword(string encryptedPwd)
            {
                byte[] encodedPassword = Convert.FromBase64String(encryptedPwd);
                byte[] bytes = this.DecryptPassword(encodedPassword);
                if (bytes == null)
                {
                    return null;
                }
                return System.Text.Encoding.Unicode.GetString(bytes, 0x10, bytes.Length - 0x10);
            }
        }




    }
}