<%@ Page Title="Your Monthly Activity" Language="C#" MasterPageFile="~/master/Main.Master"
         AutoEventWireup="true" CodeBehind="activity_month_calendar.aspx.cs" Inherits="Expenses.Activity" %>

<%@ Register TagPrefix="uc" TagName="ucMonthYear" Src="~/controls/_ucMonthYear.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<script type="text/javascript">

    var minHeight = 20;
    var minWindowHeight = 600;

    $(document).ready(function () {

        $("#divDialogCalendarForm").dialog({
            modal: true,
            autoOpen: false
        });


        $(".exp-amount-total").each(function (index, item) {
            // alert($(this).text().substring(0, 1));
            if ($(this).text().substring(0, 1) == "-") {
                $(this).css({ 'color': '#FF3300' });
            }
            else {
                $(this).css({ 'color': '#009900' });
            }
        });

//        $(".exp-cal-table td").css("min-height", function () {
//            if ($(window).height() <= minWindowHeight) {
//                $(this).css("min-height", minHeight);
//            }
//            else {
//                $(this).css("min-height", ($(window).height() - 220) / 5);
//            }
//            return false;
//        });
//        $(window).resize(function () {
//            $(".exp-cal-table td").css("min-height", function () {

//                if ($(window).height() <= minWindowHeight) {
//                    $(this).css("min-height", minHeight);
//                }
//                else {
//                    $(this).css("min-height", ($(window).height() - 220) / 5);
//                }
//                return false;
//            });
//        });



        return false;


    });



    function fnOpenCalDialog(d, t) {

            // t = 1 - daily form to add new or update expenses record
            if (t == "1") {

                $("#divDialogCalendarForm").dialog({
                    modal: true,
                    autoOpen: false,
                    width: $(window).width() - $(window).width()/4,
                    height: $(window).height() - $(window).height()/4,
                    title: "Daily Activity"
                });
                $('body').css('overflow-x', 'hidden');
                var newurl = 'activity_day_form.aspx?selectedDay=' + d;
                $('#iFrameDayForm').attr('src', newurl);
            }

            // t = 2,3,4 - reports by month , y-t-d, and whole year summary
            if (t == "2" || t == "3" || t == "4") {
                
                $("#divDialogCalendarForm").dialog({
                    modal: true,
                    autoOpen: false,
                    width: $(window).width() - $(window).width() / 2,
                    height: $(window).height() - 100,
                    overflow: 'hidden',
                    title: "Summary Report"
                });
                $('body').css('overflow', 'hidden');

                var m = $('#<%=ucMonthYear1.FindControl("ddlMonth").ClientID %>').val();
                var y = $('#<%=ucMonthYear1.FindControl("ddlYear").ClientID %>').val();

                var newurl = 'report/report_sum_by_dates_range.aspx?nType='+t+'&nMonth='+m+'&nYear='+y;
                $('#iFrameDayForm').attr('src', newurl);
                $('#divDialogCalendarForm').css({ backgroundImage: 'none' });
            }


            $("#divDialogCalendarForm").dialog('open');
            //window.frames["iFrameDayForm"].location.reload();
            $('#divDialogCalendarForm').css({ backgroundImage: 'none' });

           
            return false;
        }

    function fnShowCalendarDetail(a) {
            $(".exp-cal-div-day-detail").each(function(index) {
            if (a == "show") {
                $(this).slideDown(800);
            }
            else {
            $(this).slideUp(1000);
            }
        });
    }

</script>

</asp:Content>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div id="divDialogCalendarForm" style="background:url('/IMAGES/ajax-loader.gif') center center no-repeat; overflow:hidden;">
<iframe id="iFrameDayForm" width="99%" height="99%" frameborder="0"></iframe>
</div>
<div class="exp-div-transparent">
     <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
     <div style="margin: 3px auto 5px auto; float:left; width:auto;">
       <uc:ucMonthYear ID="ucMonthYear1" runat="server" />
    </div>

   <div style="margin: 3px auto 5px auto; float:right; width: 50px;">
       <img src="images/icons/actions-go-down-icon-16.png" alt="Go Down" title="Show Daily Detail" 
            class="exp-click-entry" onclick="fnShowCalendarDetail('show')" />
       <img src="images/icons/actions-go-up-icon-16.png" alt="Go Up" title="Hide Daily Detail" 
           class="exp-click-entry" onclick="fnShowCalendarDetail('hide')" />
   </div>
<div style="float:left; width:auto;">
    <div class="exp-div-round-small" runat="server" style="float:left; width:auto; white-space:nowrap; margin-left:5px;">
        <label class="exp-label-ampunt-total">Month:</label>
        <a href="#" class="exp-rpt-link" onclick="fnOpenCalDialog(0,2)">
        <asp:Label runat="server" ID="lblSumMonth" CssClass="exp-amount-total"></asp:Label></a> 
        </div>
    <div class="exp-div-round-small" runat="server" style="float:left; width:auto; white-space:nowrap; margin-left:5px;">
           <label class="exp-label-ampunt-total">YTD:</label>
            <a href="#" class="exp-rpt-link" onclick="fnOpenCalDialog(0,3)">
            <asp:Label runat="server" ID="lblSumYTD" CssClass="exp-amount-total"></asp:Label></a>&nbsp;
        </div>
    <div class="exp-div-round-small" runat="server" style="float:left; width:auto; white-space:nowrap; margin-left:5px;">
        <label class="exp-label-ampunt-total">Year:</label>
        <a href="#" class="exp-rpt-link" onclick="fnOpenCalDialog(0,4)">
        <asp:Label runat="server" ID="lblSumYear" CssClass="exp-amount-total"></asp:Label></a>&nbsp;
        </div>
 </div>

 </div>

   <div style="float:left; width:100%;">
    <table id="htmlCalendarTable" runat="server" align="center" cellpadding="1" cellspacing="1" class="exp-cal-table">
   <tr>
     <th>Sun</th><th>Mon</th><th>Tue</th><th>Wed</th><th>Thu</th><th>Fri</th><th>Sat</th>
   </tr>
 </table>
 </div>

</asp:Content>

