<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_ucMonthYear.ascx.cs" Inherits="Expenses._ucMonthYear" %>

 <asp:ImageButton runat="server" ID="btnPrevious" AlternateText="Next" ToolTip="Previous Month" ImageAlign="Baseline"
            ImageUrl="~/images/icons/go-previous-icon-16.png" OnClick="btnPrevious_Click" />
  <asp:DropDownList runat="server" ID="ddlMonth" CssClass="exp-click-entry"
        onselectedindexchanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> 
  <asp:DropDownList runat="server" ID="ddlYear" CssClass="exp-click-entry"
        onselectedindexchanged="ddlYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
       <asp:ImageButton runat="server" ID="btnNext" AlternateText="Next" ToolTip="Next Month" ImageAlign="Baseline"
            ImageUrl="~/images/icons/go-next-icon-16.png" OnClick="btnNext_Click" />
&nbsp;
<asp:DropDownList ID="ddlCtg" runat="server" AutoPostBack="true" CssClass="exp-click-entry" 
    onselectedindexchanged="ddlCtg_SelectedIndexChanged">
</asp:DropDownList>
<asp:DropDownList ID="ddlSubCtg" runat="server" AutoPostBack="true" CssClass="exp-click-entry" 
    onselectedindexchanged="ddlSubCtg_SelectedIndexChanged">
</asp:DropDownList>
&nbsp;&nbsp;
<asp:ImageButton runat="server" ID="imgRefresh" AlternateText="Refresh" 
    ToolTip="Refresh Result" ImageAlign="Baseline"
            ImageUrl="~/images/icons/refresh-icon-16.png" 
    onclick="imgRefresh_Click" />
    &nbsp;&nbsp;

