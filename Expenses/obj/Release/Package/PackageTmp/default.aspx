<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="Expenses._default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Total Expenses</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
    
    .div1 
    {
    height: auto;
    width:auto;
    -moz-border-radius: 25px 10px / 10px 25px;
    border-radius: 25px 10px / 10px 25px;
    border:1px solid #4b6c9e;
    padding:8px 8px 8px 8px;
    font-size:small;
    text-align:inherit;
    margin:7px 7px 7px 7px;
    border-bottom:outset 5px #99CCFF;
    border-right:outset 5px #99CCFF;
    background:#ECEEEC; /*EDEDED;*/
    }
    p {text-indent:50px;}
    </style>

    <div class="exp-page-inside-main-content"> 
     <div class="inside-round">
     <table><tr>
     <td style="width:28%;"><div class="div1">
             <a href="account/Register.aspx" title="Click to Sign Up">
            <img src="images/icons/internet-icon-48.png" alt="Internet" style="float:left; margin-right: 5px;" /></a>
            <b>Easy access </b><br /><br />
            Total Expenses is the simple, smarter online tool. There's no software to download and no
             worrying about system compatibility.
             All you need is an Internet connection. WE will help you create a personalized spending plan, 
             and in just a few minutes you are well on your way to financial freedom.
        </div>
        <div class="div1">
         <a href="account/Register.aspx" title="Click to Sign Up">
            <img src="images/icons/stock-icon-48.png" alt="Monitor" style="float:left; margin-right: 5px;" /></a>
            <b>Monitor and Plan 24/7 </b><br /><br />
            Monitor and control your spending.
            Your 12 month plan is completely customizable so you can fine-tune those months where you are likely to spend more. 
            Helps you set up a budget and keeps it up-to-date.
            Track whether you live within your means month-to-month.
            See your top spending trends over time.
        </div>
      </td>
     <td align="center" valign="middle" style="width:41%;">
      <div class="div1" style="text-align:left;">
          <a href="account/Register.aspx" title="Click to Sign Up">
          <img src="images/icons/invoice-icon-64.png" alt="Budget" style="float:left; margin-right: 5px;" /></a>
          <b>Control your budget and spending</b><br /><br />
            &nbsp;&nbsp;&nbsp;To define a budget means to outline a plan for expected revenues and expenses in a way that 
            allows wise spending and saving. Budgeting is an important concept for individuals and families
             to provide a safe level of forecasting of income and expenses and construct models based on their 
            personal goals for saving.Without a budget, most households are just scraping by, and that's just not 
            enough in this day and age.<br />

            &nbsp;&nbsp;&nbsp;Total Expenses is an important forecasting tool for measuring overall growth and planning for the future. 
            Knowing how to design a budget is not, however, a commonly understood skill. 

<%--          <img src="images/icons/chart-pie-icon-48.png" alt="Chart" style="float:right; margin-left: 5px;" />--%>
                      Total Expenses is an interactive online budgeting tool designed to simplify the way you manage your money by showing 
            you how to quickly and easily define a budget. Once you know where your money is going now, you can more easily design a 
            plan to save more and achieve your financial goals.
        </div></td>
     <td valign="top" style="width:31%;">
     <div class="div1"style="text-align:left;">
     <a href="account/Register.aspx" title="Click to Sign Up">
           <img src="images/icons/sign-up-icon-48.png" alt="Sing Up"  style="float:right; margin-right: 5px;" /> </a>
           <b> Quick sign-up</b><br /><br />
            Signing up takes only a few quick clicks, 
            just provide your e-mail address <%--(optional)--%>, 
            create your login, password, and verify your agreement to the Terms of Use. 
           <%-- A confirmation message is sent to your e-mail address. 
            Open that message, click the URL link, and in less than 2 minutes ... you're in!--%>
            Start your finance planning right away.
           
        </div>
         <div class="div1" style="text-align:left;">
        
            <b>Why Total Expenses?</b><br />
            <table>
            <tr><td>1.</td><td style="width:100%;">It is real fun :).</td></tr>
            <tr><td>2.</td><td>No personal info needed.</td></tr>
            <tr><td>3.</td><td>Start finance planning right away.</td></tr>
            <tr><td>4.</td><td>Make comparison between years, months, groups, categories, etc...</td></tr>
            <tr><td>5.</td><td>The best way to create a realistic 12 month spending plan in minutes.</td></tr>
            <tr><td>6.</td><td>Overall Easy 1-2-3 personal budgeting.</td></tr>
            <tr><td colspan="2"> <a href="account/Register.aspx" title="Click to Sign Up">
            <img src="images/icons/smile-icon-48.png" alt="Monitor" style="float:right; margin-right: 5px;" /></a></td></tr>
            </table>
            
        </div>
     </td>
     </tr></table>
        
        
     </div>
    </div>

   
</asp:Content>
