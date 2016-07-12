<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activity_day_form.aspx.cs" Inherits="Expenses.ActivityDayForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <link href="js/css/smoothness/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/blocker/jquery.blockUI.js" type="text/javascript"></script>
    <link href="ccs/expenses.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

//    window.onload = function () {
//        $.blockUI({
//            message: '<h1><img src="images/busy.gif" /> Processing</h1>',
//            theme: true
//        });

//        $(document).ready(function () { $.unblockUI() });
//    }

    function startRequest() {
        $.blockUI({
            message: '<h1><img src="images/busy.gif" /> Processing</h1>',
            theme: true
        });
    }

    function endRequest() {
        $.unblockUI();
    }


    $(document).ready(function () {

        $('body').css('overflow-x', 'hidden');
        $("#<%=hdnUrlPath.ClientID %>").val("activity_day_form.aspx?selectedDay=" + $("#<%=hdnYmdAdd.ClientID %>").val());
        //alert($("#<%=hdnUrlPath.ClientID %>").val());

        $("#lblMsg").hide();

        $("#imgRefresh").click(function () {
            startRequest();
            var url = $("#<%=hdnUrlPath.ClientID %>").val();
            window.location.href = url;
            endRequest();
        });

        $("#dpDate").datepicker({
            numberOfMonths: 3,
            showButtonPanel: true,
            showAnim: "slide",
            defaultDate: "-1m"
        });

        $("#ddlCtg").change(function () {
            //startRequest();
            $.ajax({
                type: "POST",
                url: "activity_day_form.aspx/getJsonSubCtgString",
                data: '{ ctgID: "' + $("#ddlCtg").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonString) {
                    var obj = $.parseJSON(jsonString.d);
                    $('#ddlSubCtg option').each(function (index, option) {
                        $(option).remove();
                    });
                    $.each(obj, function (key, value) {
                        $('#ddlSubCtg').append($("<option/>", {
                            value: key,
                            text: value
                        }));
                    });

                    // Sort by name
                    $("#ddlSubCtg").html($("#ddlSubCtg option").sort(function (a, b) {
                        return a.text == b.text ? 0 : a.text < b.text ? -1 : 1;
                    }));

                    // Reselect the first option
                    $('#ddlSubCtg').val('1');
                }
            });
            //endRequest();
            return false;
        });

        $(function () {
            $("#slrMonthNbr").slider({
                range: "max",
                value: 0,
                min: 0,
                max: 12,
                slide: function (event, ui) {
                    $("#txtMonthNbr").val(ui.value);
                }
            });
            $("#txtMonthNbr").val($("#slrMonthNbr").slider("value"));

        });

        $(function () {
            $(".aSelect, .aDelete, .aAdd, .aUpdate").button();
        });

        $("#btnAdd, #btnUpdate").click(function () {
            startRequest();
            var dpDate = $("#dpDate").val();
            var amountNbr = $("#<%=txtAmount.ClientID %>").val();
            var monthNbr = $("#<%=txtMonthNbr.ClientID %>").val();
            var ctgID = $('#ddlCtg').val();
            var subctgID = $('#ddlSubCtg').val();
            var recordDesc = $("#<%=txtDesc.ClientID %>").val();
            var recordID = $("#<%=hdnRecordId.ClientID %>").val();
            var plusMinus = $("#<%= rbPlusMinus.ClientID %> input:checked").val();
            //alert(amountNbr);
            if (recordID == "") {
                recordID = "0";
            }
            //validation here
            if (dpDate == "" || Date.parse(dpDate) == NaN) {
                $("#dpDate").css({ 'background-color': '#F2F5A9' });
                alert("ERROR - Invalid date!");
                return false;
            };

            if (isNaN(amountNbr) || amountNbr == "") {
                $("#<%=txtAmount.ClientID %>").css({ 'background-color': '#F2F5A9' });
                alert("ERROR - Invalid amount!");
                return false;
            }

            $.ajax({
                type: "POST",
                url: "activity_day_form.aspx/recordAction",
                data: '{ recordID: "' + recordID + '", dtDate: "' + dpDate + '", plusMinus: "' + plusMinus + '", amountNbr: "' + amountNbr + '", monthNbr: "' + monthNbr + '", ctgID: "' + ctgID + '", subctgID: "' + subctgID + '", recordDesc: "' + recordDesc + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonString) {
                    //alert($("#<%=hdnUrlPath.ClientID %>").val());
                    window.location.href = $("#<%=hdnUrlPath.ClientID %>").val();
                    //$("#lblMsg").text(jsonString.d);
                }
            });
            endRequest();
            return false;
        });
    });

    function recordSelect(recordID) {
       // alert("I am here");
        //$("#<%=hdnRecordId.ClientID %>").val(recordID);
        var url = $("#<%=hdnUrlPath.ClientID %>").val() + "&selectedRecordID=" + recordID;
        //alert(url);
        window.location.href = url;
        return false;
    }

    function recordDelete(recordID) {
        if (confirm("Are you sure about it?")) {
            startRequest();
            $.ajax({
                type: "POST",
                url: "activity_day_form.aspx/recordDelete",
                data: '{ recordID: "' + recordID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonString) {
                    // alert(jsonString.d);
                    window.location.href = $("#<%=hdnUrlPath.ClientID %>").val(); // reload();
                    // $("#lblMsg").text(jsonString.d);
                }
            });
            endRequest();
          };

        return false;
    }

    function fnDialogChangeDate(i){
        var d = new Date($("#dpDate").val());
        d.setDate(d.getDate() + i);
        var mm = (d.getMonth()+1).toString();
        var dd = d.getDate().toString();
        var yyyy = d.getFullYear().toString();
        if (mm.length<2){mm='0'+mm}
        if (dd.length<2){dd='0'+dd}
        var d4 = yyyy+mm+dd;
        var newurl = 'activity_day_form.aspx?selectedDay=' + d4;
        //alert(d4);
        window.location.href = newurl;
    }


</script>

</head>

<body>
    <form id="form1" runat="server">

      <asp:HiddenField runat="server" ID="hdnRecordId" />
      <asp:HiddenField runat="server" ID="hdnUrlPath" />
      <asp:HiddenField runat="server" ID="hdnYmdAdd" />

 <div id="divDayActivity" class="exp-div-transparent">
 <div>
  <table align="center" cellpadding="2" cellspacing="0" class="exp-form-table" width="675px">
     <tr>
		<th colspan="3">Date *</th><th>Amount *</th><th>Duration</th><th>Category</th><th>Sub-Category</th>
		<th>Description</th>
        <th style="width:70px;">
            <div style="float:left;">Action</div>
              <div style="float:right;"><img id="imgRefresh" alt="Refresh" title="Refresh" 
                      src="images/icons/refresh-icon-16.png" style="vertical-align:middle;"  /></div>
           
        </th>
     </tr> 

   <tr>
     <td style="width:5px;"><img src="images/icons/go-previous-icon-16.png" alt="Previouse Day" onclick="fnDialogChangeDate(-1);" /></td>
     <td style="width:80px;">
        <input runat="server" id="dpDate" type="text" class="exp-click-entry" style="width:75px;" />
    </td>
    <td style="width:5px;"><img src="images/icons/go-next-icon-16.png" alt="Next Day" onclick="fnDialogChangeDate(1);"/></td>
    <td valign="top" style="width:160px;">
    <div style="display:block; border:0px;">
      <table class="exp-form-table" border="0px" cellpadding="0" cellspacing="0">
       <tr>
       <td align="left">
       <asp:RadioButtonList runat="server" ID="rbPlusMinus" AutoPostBack="false" RepeatDirection="Horizontal" TextAlign="Left">
           <asp:ListItem Text="+$" Value="1"></asp:ListItem>
           <asp:ListItem Text="-$" Value="0" Selected="True"></asp:ListItem>
       </asp:RadioButtonList>
       </td>
      <td align="left"><asp:TextBox ID="txtAmount" runat="server" Width="65px" CssClass="exp-text-entry">
      </asp:TextBox></td>
      </tr></table>
      </div>
    </td>
     <td>
      <div style="margin-top:-3px;">
       <table><tr><td align="right">
               <input runat="server" type="text" id="txtMonthNbr" 
                             class="exp-text-entry" style="border:0; width:15px; text-align:center;" />
       </td>
       <td align="left"> <div id="slrMonthNbr" style="width:50px; float:right; margin: 4px 2px 0px 4px;"></div></td>
       </tr></table>
       </div>
     </td>
    <td>
        <select runat="server" id="ddlCtg" class="exp-click-entry">
        </select>
    </td>
     <td>
        <select runat="server" id="ddlSubCtg" class="exp-click-entry" style="width:150px;"></select>
    </td>
    <td>
        <asp:TextBox ID="txtDesc" runat="server" Width="120px" MaxLength="100" CssClass="exp-text-entry"></asp:TextBox>
    </td>
      <td>
      <input runat="server" type="button" id="btnAdd" value="Add" class="aAdd" style="width:60px;" />
      <input runat="server" type="button" id="btnUpdate" value="Update" class="aUpdate" style="width:60px;" />
    </td>
   </tr>
   <tr><td colspan="7" style="font-size:8px;">   <b>*</b> - required field</td></tr>
   </table>
 </div>
  <div class="exp-div-text" style="height:62px; margin-top:3px; margin-bottom:3px;">
   <p style="padding: 5px 10px 10px 10px;">
    <b>ADD</b> - fill out all information and click on "Add" button.<br />
   <b>UPDATE</b> - click on "Select" button first, then change information and click on "Update" button.<br />
   <b>DELETE</b> - click on "Delete" button and confirm.
   </p>
  </div>

<div>
   <asp:GridView runat="server" ID="grvUpdate" AutoGenerateColumns="false" CssClass="exp-grid-view"
                 CellPadding="2" CellSpacing="2" BorderWidth="0" GridLines="None" AllowPaging="false"
                 AllowSorting="true" ShowHeaderWhenEmpty="true" OnSorting="grvUpdate_Sorting">
     <HeaderStyle Width="675px" CssClass="exp-grid-view-header" />
     <EmptyDataTemplate>
        <asp:Label ID="lblEmptySearch" runat="server">No Results Found</asp:Label>
     </EmptyDataTemplate>

     <Columns>
      <asp:BoundField DataField="ExpId" HeaderText="ID" />
      <asp:BoundField DataField="YmdAdd" HeaderText="Date" DataFormatString="{0:MM/dd/yy}" />
      <asp:BoundField DataField="AmtExp" HeaderText="Amount" DataFormatString="{0:+$#,##0.00;-$#,##0.00;$0.00}" SortExpression="AmtExp" />
      <asp:BoundField DataField="CtgName" HeaderText="Category" SortExpression="CtgName"  />
      <asp:BoundField DataField="SubCtgName" HeaderText="Sub-Category" SortExpression="SubCtgName"/>
      <asp:BoundField DataField="ExpDesc" HeaderText="Description"  />
      <asp:TemplateField HeaderText="Action" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="140px">
         <ItemTemplate>
           <input type="button" id="btnSelect" value="Select" class="aSelect" onclick="recordSelect('<%#Eval("ExpId") %>');" style="width:60px;" />
           <input type="button" id="btnDelete" value="Delete" class="aDelete" onclick="recordDelete('<%#Eval("ExpId") %>');" style="width:60px;" />
         </ItemTemplate>
      </asp:TemplateField>
     </Columns>
     <RowStyle CssClass="gridrow" />
     <EmptyDataRowStyle BackColor="ButtonShadow" />

   </asp:GridView>
</div>
<br />
<div>
 <label id="lblMsg" class="ui-state-highlight"></label>
</div>
</div>
   </form>
</body>
</html>