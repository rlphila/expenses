<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exp_settings_assign_subctg_to_ctg.aspx.cs" Inherits="Expenses.settings.exp_settings_assign_subctg_to_ctg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <link href="../js/css/smoothness/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <link href="../ccs/expenses.css" rel="stylesheet" type="text/css" />
 
 <script type="text/javascript">
     $(document).ready(function () {
         $(".exp-tool-page-inside-main-content").css('height', $(window).height() - 6);
     });
 </script>
</head>
<body>
 
    <form id="form1" runat="server">
     <div class="exp-tool-page-inside-main-content"> 
            <div style="width:99%; padding: 15px 10px 15px 5px;">
                <label class="exp-no-entry">Category:</label>
                <asp:DropDownList ID="ddlCtg" runat="server" Width="300px" AutoPostBack="true" 
                       CssClass="exp-click-entry" onselectedindexchanged="ddlCtg_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div style="border: 1px solid; width: auto; float:left; vertical-align:top; margin: 5px 5px 5px 5px;">
                <label class="exp-no-entry">Available Sub-categories:</label>
                <asp:CheckBoxList ID="lstSubCtg1" runat="server" AutoPostBack="false"
                     CssClass="exp-click-entry"
                     RepeatColumns="4" RepeatDirection="Vertical" RepeatLayout="Table">
                </asp:CheckBoxList>
            </div>
            <div style="width: 30px; float:left; margin: 0px 10px 0px 10px;">
                <div style="margin-top:20px;">
                  <asp:ImageButton ID="btnAdd" ImageUrl="~/images/icons/go-next-icon-24.png" runat="server" AlternateText="Add Sub-Categoty" ToolTip="Add Sub-Categoty" onclick="btnAdd_Click" />
                </div>
                <div style="margin-top:40px;">
                  <asp:ImageButton ID="btnRemove" ImageUrl="~/images/icons/go-previous-icon-24.png" runat="server" AlternateText="Remove Sub-Categoty" ToolTip="Remove Sub-Categoty" onclick="btnRemove_Click" />
                </div>
             </div>
            <div style="border: 1px solid; width: auto; float:left; vertical-align:top; margin: 5px 5px 5px 5px;">
                 <label class="exp-no-entry">Selected Sub-categories:</label>
                 <asp:CheckBoxList ID="lstSubCtg2" runat="server" AutoPostBack="false" CssClass="exp-click-entry"
                                   RepeatColumns="2" RepeatDirection="Vertical" RepeatLayout="Table">
                </asp:CheckBoxList>
           </div>
       </div>
    </form>
</body>
</html>
