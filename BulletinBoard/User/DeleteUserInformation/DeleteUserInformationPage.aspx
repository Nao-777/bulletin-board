<%@ Page Title="" Language="C#" MasterPageFile="~/BulletinBoard/User/User.Master" AutoEventWireup="true" CodeBehind="DeleteUserInformationPage.aspx.cs" Inherits="Board.BulletinBoard.User.DeleteUserInformation.DeleteUserInformationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p>本当に退会しますか</p>
        <div>
            <asp:LinkButton ID="lbDeleteButton" runat="server" OnClick="lbDeleteButton_Click" Text="退会する" />
       </div>
        <div>
            <a href="../MyPage.aspx">キャンセル</a>
        </div>
   </div>
</asp:Content>
