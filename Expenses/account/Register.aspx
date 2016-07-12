<%@ Page Title="Register" Language="C#" MasterPageFile="~/master/Main.Master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Expenses.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 
<div class="exp-page-inside-main-content"> 
  <div class="inside-round" style="width:50%;">
        <h2>
            Create a New Account
        </h2>
        <p>
            Use the form below to create a new account.
        </p>
        <p>
            Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
        </p>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
        </span>
        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                ValidationGroup="RegisterUserValidationGroup"/>
        <div class="exp-account-info">
            <fieldset class="exp-account-action">
                <legend>Account Information</legend>
                <p>
                    <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName">Username:</asp:Label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="exp-text-entry" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName" 
                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                            ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtUserEmail">E-mail:</asp:Label>
                    <asp:TextBox ID="txtUserEmail" runat="server" CssClass="exp-text-entry" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtUserEmail" 
                            CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                            ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="lblUserPsw" runat="server" AssociatedControlID="txtUserPsw">Password:</asp:Label>
                    <asp:TextBox ID="txtUserPsw" runat="server" CssClass="exp-text-entry" Width="200px" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtUserPsw" 
                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                            ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmUserPsw">Confirm Password:</asp:Label>
                    <asp:TextBox ID="txtConfirmUserPsw" runat="server" CssClass="exp-text-entry" Width="200px" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtConfirmUserPsw" CssClass="failureNotification" Display="Dynamic" 
                            ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                            ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtUserPsw" ControlToValidate="txtConfirmUserPsw" 
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                            ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                </p>
            </fieldset>
            <p class="exp-submit-button">
                <asp:Button ID="btnRegisterUser" runat="server" Text="Create User" CssClass="exp-button"
                        ValidationGroup="RegisterUserValidationGroup" OnClick="btnRegisterUser_OnClick"/>
            </p>
        </div>

  </div>
</div>

</asp:Content>
