<%@ Page Title="" Language="C#" MasterPageFile="~/BulletinBoard/User/User.Master" AutoEventWireup="true" CodeBehind="MyPage.aspx.cs" Inherits="Board.BulletinBoard.User.MyPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
            ログインID
            <p id="pLoginID" runat="server"></p>
            <br />
            名前
            <p id="pUserName" runat="server"></p>
    </div>
    <br />
    <div>
        <asp:LinkButton ID="lbModifyButton" runat="server" OnClick="lbModifyButton_Click" Text="会員情報の変更" />
        <br />
        <asp:LinkButton ID="lbDeleteUserInfomationButton" runat="server" OnClick="lbDeleteUserInfomationButton_Click" Text="退会する"/>
        <br />
        <a href="../MainPage/BulletinBoardPage.aspx">戻る</a>         
   </div>
</asp:Content>
