<%@ Page Title="" Language="C#" MasterPageFile="~/BulletinBoard/MainPage/MainPage.Master" AutoEventWireup="true" CodeBehind="InArticlPage.aspx.cs" Inherits="Board.BulletinBoard.MainPage.ArticlPage.InArticlPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
     <style>
        .mainPage{
            
            display:flex;
           
            
        }
        .dMainArticl{
            text-align:center;
            padding: 0.5em 1em;
            margin: 2em 0;
            color: #FFF;
            background: #6eb7ff;
            border-bottom: solid 6px #3f87ce;
            box-shadow: 0 3px 6px rgba(0, 0, 0, 0.25);
            border-radius: 9px;
        }
        .messages{
            width:60%;
            margin-left:5%
        }
        .message{
            margin:50px
        }
        .pLabelMessage{
            display:flex;
        }
        .UserName{
            margin-left:10px;
            margin-top:0%;
            width:60%;
        }
        #pMessageNumber{
            display:flex;
            align-content:center;
            width:10%;
        }
        .Days{
            margin-top:0%;
            width:30%;
        }
        #dMessageForm{
            
            padding: 0.5em 1em;
            margin: 2em 0;
            font-weight: bold;
            color: black;/*文字色*/
            background-color:white;
            border: solid 3px #6091d3;/*線*/
            border-radius: 10px;/*角の丸み*/
        }
    </style>
 </asp:Content>
   
<asp:Content ID="cInputForm" ContentPlaceHolderID="cInputForm" runat="server">
    <!--どのスレの画面に入ったかわかるようにタイトルと内容、作成者を表示する場所-->
    <div class="dMainArticl">
                <div>
                    <h1 id="hArticlTitle" runat="server">
                        タイトル
                    </h1>
                    <p>内容</p> <p id="pArticlContents" runat="server">コンテンツ</p>
                </div>
                <div>
                    <p id="pUserName" runat="server">名前</p>
                </div>
    </div>
     <!--メッセージ送信-->
     <div id="dMessageForm">
                <div>
                    <p>メッセージ作成</p>
                    <asp:TextBox ID="tbMessage" runat="server" ></asp:TextBox>
                    <asp:LinkButton ID="lbSendMessage" runat="server" OnClick="lbSendMessage_Click" Text="送信"></asp:LinkButton>
                    <p id="pAlertMessage" runat="server"></p>
                </div>

    </div>
     <br />
                 <a href="../BulletinBoardPage.aspx">戻る</a>
</asp:Content>
<asp:Content ID="cMainDisplay" ContentPlaceHolderID="cMainDisplay" runat="server">
     <!--メッセージの一覧-->
    <div class="messages">
        <asp:Repeater ID="rMessage" runat="server">
            <ItemTemplate>
                <div class="message">
                    <div class="pLabelMessage">
                        <asp:Literal  ID="pMessageNumber" runat="server">--</asp:Literal>      
                            <p class="UserName">名前：<%# Eval("userName") %></p>
                            <p class="Days"><%# Eval("days") %></p>
                   </div>
                   <p><%# Eval("userMessage") %></p>         
                </div>
            </ItemTemplate>
        </asp:Repeater>
     </div>

</asp:Content>
