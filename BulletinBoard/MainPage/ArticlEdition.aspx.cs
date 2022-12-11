/*
============================================================================================================
   スレのタイトル(title)と内容(contents)を編集す
=============================================================================================================
*/
using System;
using System.Diagnostics;


namespace Board.BulletinBoard.MainPage
{
    public partial class ArticlEdition : System.Web.UI.Page
    {
        CommonSql cs = new CommonSql();
        CommonMethod cm = new CommonMethod();
        SessionManager sm = new SessionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userID = sm.Current.userID;
                if (string.IsNullOrEmpty(userID))
                {
                    Response.Redirect("../Form/Logout.aspx");
                }
                ///記事のID
                var articlID = Request.QueryString["articlID"];
                ///記事のタイトルを取得
                var sqlQuery_getTitle = string.Format("SELECT articlTitle " +
                                                         "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                                         "WHERE articlID = '{0}'", articlID);
                var oldTitle = cs.NewGetTableElement(sqlQuery_getTitle, "articlTitle");
                pOldTitle.InnerText = oldTitle;
                ///記事の内容を取得
                var sqlQuery_getContents = string.Format("SELECT articlContents " +
                                                        "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                                        "WHERE articlID = '{0}'", articlID);
                var oldContents = cs.NewGetTableElement(sqlQuery_getContents, "articlContents");

                pOldContents.InnerText = oldContents;


            }
        }

        /// <summary>
        /// タイトル・内容の変更をする処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lbEditArticlButton_Click(object sender, EventArgs e)
        {
            var articlID = Request.QueryString["articlID"];
            var newTitle = tbNewTitle.Text;
            var newContents = tbNewContents.Text;

            if (cm.CheckCharacterIntegrity(newTitle, 1, 100, false) && cm.CheckCharacterIntegrity(newContents, 1, 500, false))
            {
                var sqlQuery = string.Format("UPDATE [dbo].[TABLE_BULLETINBOARD] " +
                                              "SET articlTitle=N'{0}'," +
                                                   "articlContents=N'{1}' " +
                                              "WHERE articlID='{2}'", newTitle, newContents, articlID);
                cs.OperateSql_Insert_Delete_Update(sqlQuery);
                Debug.WriteLine("編集成功です");
                Response.Redirect("./BulletinBoardPage.aspx");
            }
            else
            {
                pAlertMessage.InnerText = "*入力内容に誤りがあります";
            }
        }

    }
}