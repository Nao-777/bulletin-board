<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticlEdition.aspx.cs" Inherits="Board.BulletinBoard.MainPage.ArticlEdition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>記事の編集</h1>
            <br />
            <br />
            <div>
                <div>
                    タイトル<br />
                    <p id="pOldTitle" runat="server"></p>
                    <asp:TextBox ID="tbNewTitle" runat="server"></asp:TextBox>
                </div>
                <br />
                <div>
                    内容<br />
                    <p id="pOldContents" runat="server"></p>
                    <asp:TextBox ID="tbNewContents" runat="server"></asp:TextBox>
                </div>
                <br />
                <p id="pAlertMessage" runat="server"></p>
                <br />
                <div>
                    <a href="BulletinBoardPage.aspx">戻る</a>
                </div>
                
                <div>
                    <asp:LinkButton ID="lbEditArticlButton" runat="server" OnClick="lbEditArticlButton_Click" Text="確定"/>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
