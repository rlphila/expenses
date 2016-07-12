<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report_sum_by_dates_range.aspx.cs" Inherits="Expenses.report_sum_by_dates_range" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc" TagName="ucDatesRange" Src="~/controls/_ucDatesRange.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monthly Report</title>
    <script src="../js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <link href="../js/css/smoothness/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <link href="../ccs/expenses.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
  
    $(document).ready(function () {

        $('#<%=ucDatesRange1.FindControl("dpDateFrom").ClientID %>').datepicker({
            numberOfMonths: 1,
            showButtonPanel: true,
            showAnim: "slide",
            defaultDate: "1m"
        });

        $('#<%=ucDatesRange1.FindControl("dpDateTo").ClientID %>').datepicker({
            numberOfMonths: 1,
            showButtonPanel: true,
            showAnim: "slide",
            defaultDate: "1m"
        });

    });

    function fnShowCalendarDetail(a) {
        $(".exp-rpt-detail-text, .exp-rpt-detail-amount").each(function (index) {
            if (a == "show") {
                $("#htmlReportTable").attr('cellspacing', '1');
                $(this).slideDown(500);
            }
            else {
                $("#htmlReportTable").attr('cellspacing', '0');
                $(this).slideUp(500);
            }
        });
    }

</script>
</head>
<body class="exp-rpt-body">
    <form id="form1" runat="server">
    <uc:ucDatesRange runat="server" ID="ucDatesRange1" />

    <div style="margin: 3px auto 5px auto; float:right; width: 50px;">
       <img src="../images/icons/actions-go-down-icon-16.png" alt="Go Down" title="Show Daily Detail" 
            class="exp-click-entry" onclick="fnShowCalendarDetail('show')" />
       <img src="../images/icons/actions-go-up-icon-16.png" alt="Go Up" title="Hide Daily Detail" 
           class="exp-click-entry" onclick="fnShowCalendarDetail('hide')" />
   </div>
    <br />
   <table id="htmlReportTable" runat="server" align="center" cellpadding="0" cellspacing="1" class="exp-rpt-table">
   <tr>
     <th>Category</th><th>Sub-category</th><th>Amount</th>
   </tr>
 </table>
 </form>
</body>
</html>

