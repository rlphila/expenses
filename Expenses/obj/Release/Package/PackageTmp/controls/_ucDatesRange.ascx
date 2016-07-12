<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_ucDatesRange.ascx.cs" Inherits="Expenses._ucDatesRange" %>

<div style="margin: 3px auto 5px auto; float:left; width:300px;">
<input runat="server" id="dpDateFrom" type="text"  style="width:75px;" />
<input runat="server" id="dpDateTo" type="text" style="width:75px;" />

    <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="exp-button"
        onclick="btnSubmit_Click" />

</div>

<script type="text/javascript">
    $(document).ready(function () {
        $(function () {
            $(".exp-button").button();
        });
    });

</script>