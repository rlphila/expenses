<%@ Page Title="Log In" Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Expenses.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
<script type="text/javascript">

    var minHeight = 400;

    $(document).ready(function () {

        $(".exp-master-main-content, .exp-page-inside-main-content").css("min-height", function () {

            if ($(window).height() <= minHeight) {
                $(this).css("min-height", minHeight);
            }
            else {
                $(this).css("min-height", $(window).height() - 135);
            }
            return false;
        });

        $(window).resize(function () {
            $(".exp-master-main-content, .exp-page-inside-main-content").css("min-height", function () {

                if ($(window).height() <= minHeight) {
                    $(this).css("min-height", minHeight);
                }
                else {
                    $(this).css("min-height", $(window).height() - 135);
                }
                return false;
            });
        });

    });

</script>

<%--<script type="text/javascript">
    $(document).ready(function () {

        $("#btnForgotPsw").click(function () {

            var userNameOrEmail = $("#<%=UserName.ClientID %>").val();

            $.ajax({
                type: "POST",
                url: "login.aspx/passwordRecovery",
                data: '{ userNameOrEmail: "' + userNameOrEmail + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonString) {
                    alert($("#<%=hdnUrlPath.ClientID %>").val());
                    window.location.href = $("#<%=hdnUrlPath.ClientID %>").val();
                    $("#lblMsg").text(jsonString.d);
                }
            });
            return false;
        });
    });

</script>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="exp-page-inside-main-content"> 
 
   <div style="width:100%; margin-left:20px;">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.
    </p>
    </div>
  <div class="inside-round" style="float:left; width:45%; vertical-align:top;">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>
           <div class="exp-account-info">
                <fieldset class="exp-account-action">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="exp-text-entry" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">* required</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="exp-text-entry" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">* required</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>
                </fieldset>
                <p class="exp-submit-button">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" onclick="LoginButton_Click" CssClass="gen-button-b"/>
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
    
</div>
<div class="inside-round" style="float:right; width:45%; vertical-align:top;">
   <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" SuccessTextStyle-CssClass="successNotification" GeneralFailureText="Error" >
<SuccessTextStyle CssClass="successNotification"></SuccessTextStyle>
    <UserNameTemplate>
     <fieldset class="exp-account-action">
       <legend>Forgot Your Password?</legend>
         <p style="white-space:nowrap;"> Enter your username or email to receive your password.</p>
         <p><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username or Email:</asp:Label>
         <asp:TextBox ID="UserName" runat="server" CssClass="exp-text-entry" Width="200px"></asp:TextBox>
         <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
              CssClass="failureNotification"
                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">* required</asp:RequiredFieldValidator>
         </p>
      </fieldset>

    <p class="exp-submit-button">
    <asp:Button ID="btnForgotPsw" runat="server" CommandName="Submit" Text="Submit" 
    ValidationGroup="PasswordRecovery1" CssClass="gen-button-b" 
            onclick="btnForgotPsw_Click" />
    </p>
    </UserNameTemplate>
   </asp:PasswordRecovery>
</div>

</div>
</asp:Content>
