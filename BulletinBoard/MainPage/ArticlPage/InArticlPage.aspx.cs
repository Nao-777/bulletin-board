/*
=============================================================================================================
    スレにレスをするページ
=============================================================================================================
*/
using System;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Board.BulletinBoard.MainPage.ArticlPage
{
    public partial class InArticlPage : System.Web.UI.Page
    {
        CommonSql cs = new CommonSql();
        CommonMethod cm = new CommonMethod();
        SessionManager sm = new SessionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            ///ユーザIDを取得
            var userID = sm.Current.userID;
            Debug.WriteLine("SESSION_KEY_USERID:" + userID);
            ///ログインしているか確認
            if (string.IsNullOrEmpty(userID))
            {
                Response.Redirect("../../Form/Logout.aspx");
            }
            var articlID = Request.QueryString["EntryID"];
            Debug.WriteLine("articlID:" + articlID);

            if (!IsPostBack)
            {

                //タイトルの取得・表示
                var sqlQuery_getTitle = string.Format("SELECT articlTitle " +
                                                        "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                                        "WHERE articlID = '{0}'", articlID);
                hArticlTitle.InnerText = cs.NewGetTableElement(sqlQuery_getTitle, "articlTitle");
                //内容の取得・表示
                var sqlQuery_getContents = string.Format("SELECT articlContents " +
                                                       "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                                       "WHERE articlID = '{0}'", articlID);
                pArticlContents.InnerText =cs.NewGetTableElement(sqlQuery_getContents, "articlContents");
                //ユーザ名の取得・表示
                var sqlQuery_getUserName = string.Format("SELECT userName " +
                                                        "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                                        "WHERE articlID = '{0}'", articlID);
                pUserName.InnerText = String.Format("制作者：{0}", cs.NewGetTableElement(sqlQuery_getUserName, "userName"));

                var sqlQuery = @"SELECT userMessage,userName,days 
                             FROM [dbo].[TABL_IN_ARTICL] 
                             WHERE articlID='" + articlID + @"' 
                             ORDER BY days DESC";

                var sqlQuery_display = string.Format("SELECT userMessage,userName,days " +
                                                     "FROM [dbo].[TABL_IN_ARTICL] " +
                                                     "WHERE articlID = '{0}' " +
                                                     "ORDER BY days DESC", articlID);
                cs.DisplayElements(rMessage, sqlQuery_display);

                //表示するメッセージの内容にインデントを振る
                var count = 1;
                foreach (RepeaterItem item in rMessage.Items)
                {
                    count++;
                }
                foreach (RepeaterItem item in rMessage.Items)
                {
                    count--;
                    var number = item.FindControl("pMessageNumber") as Literal;
                    number.Text = count.ToString() + ":";


                }
            }
        }

        /// <summary>
        /// レスを返す処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSendMessage_Click(object sender, EventArgs e)
        {
            if (cm.CheckCharacterIntegrity(tbMessage.Text, 1, 200, false) == false)
            {
                pAlertMessage.InnerText = "*入力内容に誤りがございます";
                return;
            }

            var nowUserID = sm.Current.userID;

            var messageID = cm.CreateRandomID(10, "TABL_IN_ARTICL", "messageID");

            var articlID = Request.QueryString["EntryID"];


            var userID = sm.Current.userID;

            var sqlQuery_getUserName = string.Format("SELECT userName " +
                                                        "FROM [dbo].[UserInfomation] " +
                                                        "WHERE userID = '{0}'", userID);
            var userName = cs.NewGetTableElement(sqlQuery_getUserName, "userName");

            var userMessage = tbMessage.Text;
            //時間の取得
            DateTime dt = DateTime.Now;

            string days = dt.ToString("yyyy/MM/dd HH:mm:ss");
            var sqlQueryold = @"INSERT INTO [dbo].[TABL_IN_ARTICL] (messageID,articlID,userID,userName,userMessage,days) 
                         VALUES ('" + messageID + "','" + articlID + "','" + userID + "',N'" + userName + "',N'" + userMessage + "','" + days + "')";

            var sqlQuery = string.Format("INSERT [dbo].[TABL_IN_ARTICL] " +
                                         "(" +
                                            "messageID," +
                                            "articlID," +
                                            "userID," +
                                            "userName," +
                                            "userMessage," +
                                            "days) " +
                                         "VALUES (" +
                                                   "'{0}'," +
                                                   "'{1}'," +
                                                   "'{2}'," +
                                                   "N'{3}'," +
                                                   "N'{4}'," +
                                                   "'{5}'" +
                                                ")", messageID, articlID, userID, userName, userMessage, days);


            cs.OperateSql_Insert_Delete_Update(sqlQuery);
            Response.Redirect(Request.RawUrl);

        }
    }
}