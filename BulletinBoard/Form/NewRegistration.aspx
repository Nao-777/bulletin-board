<%@ Page Title="" Language="C#" MasterPageFile="~/BulletinBoard/Form/Form.Master" AutoEventWireup="true" CodeBehind="NewRegistration.aspx.cs" Inherits="Board.BulletinBoard.Form.NewRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--新規登録ページ-->
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>新規登録画面</p>
        <br />
        <div>
            氏名
            <br />
            <asp:TextBox ID="tbNewNameID" runat="server"></asp:TextBox>
            <br />
            <br />
            ログインID
            <br />
            *英数字4文字以上20文字以下
            <br />
            <asp:TextBox ID="tbNewLoginID" runat="server"></asp:TextBox>
            <br />
            <br />
            パスワード設定
            <br />
            *英数字8文字以上20文字以下
            <br />
            <asp:TextBox ID="tbNewPasswordID" runat="server"></asp:TextBox>
            <br />
            <div>
                <p id="pAlertMsg" runat="server"></p>
            </div>
            <br />
            <asp:LinkButton ID="lbNewRegistrationButton" runat="server" onClick="lbNewRegistrationButton_Click" Text="確認画面へ" />
        </div>
        <br />
        <br />
        <a href="Login.aspx">ログイン画面へ</a>
</asp:Content>