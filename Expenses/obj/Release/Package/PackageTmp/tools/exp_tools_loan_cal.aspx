<%@ Page Title="" Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true" CodeBehind="exp_tools_loan_cal.aspx.cs" Inherits="Expenses.tools.exp_tools_loan_cal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript">

    
        function calculate() {

        var principal = $("#principal").val();
        var interest = $("#interest").val() / 100 / 12;
        var payments = $("#years").val() * 12;
        var x = Math.pow(1 + interest, payments);
        var monthly = (principal * x * interest) / (x - 1);

        //alert(monthly);

        if (!isNaN(monthly) &&
           (monthly != Number.POSITIVE_INFINITY) &&
           (monthly != Number.NEGATIVE_INFINITY)) {
              $("#payment").val(round(monthly));
              $("#total").val(round(monthly * payments));
              $("#totalinterest").val(round((monthly * payments) - principal));
        }

        else
        {
            $("#payment").val("");
            $("#total").val("");
            $("#totalinterest").val("");
        }
        }

        function round(x) {

        return Math.round(x * 100) / 100;
        }

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="exp-page-inside-main-content"> 
<div class="inside-round" style="width:400px;">
<table border="0" cellpadding="1" cellspacing="1" align="center" width="500px">
    <tr><th colspan="3" align="center">
       ENTER LOAN INFORMATION
     </th></tr>
    <tr><td>1.</td><td>Amount of the loan (any currency) :</td>
    <td><input type="text" id="principal" class="exp-text-entry" onchange="calculate()"/>
    </td></tr>
    <tr><td>2.</td><td>Annual percentage rate of interest :</td>
    <td><input type="text" id="interest" class="exp-text-entry" onchange="calculate()" />
    </td></tr>
    <tr><td>3.</td><td>Number of years :</td>
    <td><input type="text" id="years" class="exp-text-entry" onchange="calculate()" />
    </td></tr>
    <tr><td colspan="2">
     <p class="exp-submit-button">
    <input type="button" class="gen-button-b" value="compute" onclick="calculate()" />
    </p>
     </td><td>&nbsp;</td></tr>
     <tr> <th colspan="3" align="center">PAYMENT INFORMATION</th></tr>
    <tr>
    <td>4.</td><td>Your monthly payment will be :</td>
    <td><input type="text" id="payment" class="exp-text-entry" />
    </td>
    </tr>
    <tr><td>5.</td><td>Your total payment will be :</td>
    <td><input type="text" id="total" class="exp-text-entry" />
    </td></tr>
    <tr><td>6.</td><td>Your total interest payment will be :</td>
    <td><input type="text" id="totalinterest" class="exp-text-entry" />
    </td></tr>
</table>
</div>
</div>

</asp:Content>
