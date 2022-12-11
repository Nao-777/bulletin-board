<%@ Page Title="" Language="C#" MasterPageFile="~/BulletinBoard/Form/Form.Master" AutoEventWireup="true" CodeBehind="NewRegistrationConfirmation.aspx.cs" Inherits="Board.BulletinBoard.Form.NewRegistrationConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
            <h1>確認画面</h1>
        </div>
        <div>
            氏名
            <br />
            <p id="pUserName" runat="server"></p>
            <br />
            ログインID
            <br />
            <p id="pLoginID" runat="server"></p>
            <br />
            パスワード設定
            <br />
            <p id="pUserPassword" runat="server"></p>
            <br />
            <div>
                <p id="alertMsg" runat="server"></p>
            </div>
            <br />
            <asp:LinkButton ID="lbCancelBotton" runat="server" OnClick="lbCancelBotton_Click" Text="キャンセル"></asp:LinkButton>
            <br />
            <asp:LinkButton ID="lbNewRegistrationButton" runat="server" onClick="lbNewRegistrationButton_Click" Text="登録" />
        </div>

</asp:Content>
