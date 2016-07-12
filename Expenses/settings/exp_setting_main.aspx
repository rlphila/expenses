<%@ Page Title="" Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="exp_setting_main.aspx.cs" Inherits="Expenses.settings.setting_main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">


    $(document).ready(function () {

        $("#divDialogSettings").dialog({
            modal: true,
            autoOpen: false
        });

        return false;
    });


    function fnOpenDialogSettings(t) {
        //alert("I am here");
    // 1 - Add/Remove Sub-categories to/from a Main-category

        if (t == "1") {

            $("#divDialogSettings").dialog({
                modal: true,
                autoOpen: false,
                width: $(window).width() - 100,
                height: $(window).height() - 100,
                title: "Add or Remove Sub-categories"
            });
            $('body').css('overflow-x', 'hidden');
            var newurl = 'exp_settings_assign_subctg_to_ctg.aspx';
            $('#iFrameSettings').attr('src', newurl);
            $('#divDialogSettings').css({ backgroundImage: 'none' });
        }


//        if (t == "2" || t == "3" || t == "4") {

//            $("#divDialogCalendarForm").dialog({
//                modal: true,
//                autoOpen: false,
//                height: 400,
//                width: 500,
//                overflow: 'hidden',
//                title: "Summary Report"
//            });
//            $('body').css('overflow', 'hidden');


//            var newurl = 'report/report_sum_by_dates_range.aspx?nType=' + t + '&nMonth=' + m + '&nYear=' + y;
//            $('#iFrameDayForm').attr('src', newurl);
//            $('#divDialogCalendarForm').css({ backgroundImage: 'none' });
//        }

        $("#divDialogSettings").dialog('open');
        //window.frames["iFrameDayForm"].location.reload();
        $('#divDialogSettings').css({ backgroundImage: 'none' });

        return false;
    }

</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="divDialogSettings" style="background:url('/IMAGES/ajax-loader.gif') center center no-repeat; overflow:hidden;">
<iframe id="iFrameSettings" width="99%" height="99%" frameborder="0"></iframe>
</div>

  <div class="exp-page-inside-main-content"> 
   <div class="inside-round" style="width:auto;">

     <fieldset style="width:350px;">
      <legend>Category/Sub-Category</legend>
       <input type="button" class="gen-button-b" value="Assign Sub-Category to Main-Category" onclick="fnOpenDialogSettings(1)" />
       <br /><br />
       <input type="button" class="gen-button-b" value="Unassign Sub-Category from Main-Category" onclick="fnOpenDialogSettings(1)" />
    </fieldset>

    </div>
  </div>

</asp:Content>
