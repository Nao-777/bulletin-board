<%@ Page Title="" Language="C#" MasterPageFile="~/BulletinBoard/User/User.Master" AutoEventWireup="true" CodeBehind="RevisedConfirmationPage.aspx.cs" Inherits="Board.BulletinBoard.User.RevisedUserInfomationPage.RevisedConfirmationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>確認画面</h1>
        <br />
        <br />
        <br />
        <div>
            <p>ログインID</p>
            <p id="pConfirmationNewLoginID" runat="server"></p>
        </div>
        <br />
        <br />
        <div>
            <p>名前</p>
            <p id="pConfirmationNewUserName" runat="server"></p>
        </div>
        <br />
        <br />
        <div>
            <p>パスワード</p>
            <p id="pConfirmationNewUserPassword" runat="server"></p>
        </div>
        <br />
        <br />
        <div>
            <asp:LinkButton ID="lbCancelButton" runat="server" OnClick="lbCancelButton_Click" Text="キャンセル" />
        </div>
        <div>
            <asp:Button ID="lbDecisionButton" runat="server" OnClick="lbDecisionButton_Click" Text="更新する"/>
        </div>
    </div>
</asp:Content>
