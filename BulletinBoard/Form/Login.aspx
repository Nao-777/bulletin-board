<%@ Page Title="ログイン画面" Language="C#" MasterPageFile="~/BulletinBoard/Form/Form.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Board.BulletinBoard.Form.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--ログインページ-->
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div>
            ログインIDを入力してください
            <br />
            <asp:TextBox ID="tbLoginID" runat="server"></asp:TextBox>
            <br />
            <br />
            パスワードを入力してください
            <br />
            <asp:TextBox ID="tbPasswordID" runat="server"></asp:TextBox>
            <br />
            <div>
                <asp:Label ID="pAlertMessage" runat="server"></asp:Label>
            </div>
            <br />
            <asp:LinkButton ID="lbLogin" runat="server" onClick="lbLogin_Click" Text="ログインボタン"/>
            <br />
            <br />
            <a href="NewRegistration.aspx">新規登録の方はこちら</a>
            <br />
        </div>
    
</asp:Content>
