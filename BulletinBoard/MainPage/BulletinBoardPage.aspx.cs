/*
=============================================================================================================
    スレを表示・投稿するページ、掲示板トップ
=============================================================================================================
*/

using System;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Board.BulletinBoard.MainPage
{
    public partial class BulletinBoardPage : System.Web.UI.Page
    {
        CommonSql cs = new CommonSql();
        CommonMethod cm = new CommonMethod();
        SessionManager sm = new SessionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //ユーザIDを取得
                //ユーザIDを外部クラス経由で取得する
                var userID = sm.Current.userID;
                Debug.WriteLine("ユーザIDを外部クラス経由で取得する" + userID);


                //ログインしているか確認
                if (string.IsNullOrEmpty(userID))
                {
                    Response.Redirect("../Form/Logout.aspx");
                }

                //スレの表示
                //10ページごとにページング処理
                var value = Request.QueryString["value"];
                //「前へ」ボタンの表示処理
                if (string.IsNullOrEmpty(value) || value.Equals("0"))
                {
                    value = "0";
                    lbBeforeButton.Visible = false;
                }
                else
                {
                    lbBeforeButton.Visible = true;
                }
                Debug.WriteLine("value:" + value);
                //Repeaterコントロールを使った表示
                //1ページに表示するためのデータを取得
                var getRecodeCount = 10;
                //１ページずつ表示する件数
                var skipRecodeCount = 10 * int.Parse(value);
                var sqlQuery = string.Format("SELECT articlID,userName,articlTitle,articlContents,days " +
                                             "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                             "ORDER BY days DESC " +
                                             "OFFSET {0} ROWS " +
                                             "FETCH NEXT {1} ROWS ONLY", skipRecodeCount, getRecodeCount);
                cs.DisplayElements(rArticle, sqlQuery);
                //次へボタンの表示切替
                var nextSqlQuery = string.Format("SELECT articlID " +
                                              "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                              "ORDER BY days DESC " +
                                              "OFFSET {0} ROWS " +
                                              "FETCH NEXT {1} ROWS ONLY", skipRecodeCount + 10, getRecodeCount);
                if (cs.JudgeNextData(nextSqlQuery) == false)
                {
                    lbNextbutton.Visible = false;
                }
                //削除・変数ボタンの表示切替
                foreach (RepeaterItem item in rArticle.Items)
                {
                    var deleteButton = item.FindControl("bDeleteArticlButton") as Button;
                    var editButton = item.FindControl("bToEditArticlPageButton") as Button;
                    if (deleteButton == null)
                    {
                        continue;
                    }
                    else if (editButton == null)
                    {
                        continue;
                    }
                    //commandNameを取得
                    var deleteButtonCommandName = deleteButton.CommandName;
                    var editButtonCommandName = editButton.CommandName;

                    if (JudgeDisplayButton(deleteButtonCommandName))
                    {
                        deleteButton.Visible = true;

                    }
                    if (JudgeDisplayButton(editButtonCommandName))
                    {
                        editButton.Visible = true;
                    }

                }



            }


        }

        /// <summary>
        /// ページング処理の次へボタンの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbNextButton_Click(object sender, EventArgs e)
        {
            UrlCreator uc = new UrlCreator("./BulletinBoardPage.aspx");
            var count = Request.QueryString["value"];
            if (string.IsNullOrEmpty(count))
            {
                count = "1";
            }
            else
            {
                //ToString()でなければerrorが発生してしまう
                count = (int.Parse(count) + 1).ToString();
            }

            uc.SetParams("value", count);
            uc.CreateUrl();



        }
        /// <summary>
        /// ページング処理の戻るボタンの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbBeforeButton_Click(object sender, EventArgs e)
        {
            UrlCreator uc = new UrlCreator("./BulletinBoardPage.aspx");
            var count = Request.QueryString["value"];
            count = (int.Parse(count) - 1).ToString();
            uc.SetParams("value", count);
            uc.CreateUrl();
        }

        /// <summary>
        /// ユーザIDを基に編集・削除ボタンの表示切替判定メソッド
        /// ボタンに含まれるユーザIDの情報とセッションIDで保持しているユーザIDで比較する
        /// </summary>
        /// <param name="buttonCommandName"></param>
        /// <returns></returns>
        private bool JudgeDisplayButton(string buttonCommandName)
        {
            var SqlQuery = string.Format("SELECT userID " +
                                         "FROM [dbo].[TABLE_BULLETINBOARD] " +
                                         "WHERE articlID = '{0}'", buttonCommandName);
            var userID = cs.NewGetTableElement(SqlQuery, "userID");
            var nowUserID = sm.Current.userID;
            if (userID.Equals(nowUserID))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// マイページ（ユーザ情報変更ページへの移行）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbToMyPageBotton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../User/MyPage.aspx");
        }
        /// <summary>
        /// ログアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbLogoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Form/Login.aspx");
        }
        /// <summary>
        /// スレの削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bDeleteArticlButton_Click(object sender, EventArgs e)
        {
            var articlID = ((Button)sender).CommandName;
            var sqlQuery = string.Format("DELETE FROM [dbo].[TABLE_BULLETINBOARD]" +
                                       " WHERE articlID = '{0}'", articlID);
            cs.OperateSql_Insert_Delete_Update(sqlQuery);
            Response.Redirect(Request.RawUrl);


        }
        /// <summary>
        /// スレの編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bToEditArticlPageButton_Click(object sender, EventArgs e)
        {
            var articlID = ((Button)sender).CommandName;
            UrlCreator uc = new UrlCreator("./ArticlEdition.aspx");
            uc.SetParams("articlID", articlID);
            uc.CreateUrl();
        }
        /// <summary>
        /// スレの投稿
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbArticlInsertButton_Click(object sender, EventArgs e)
        {

            var title = tbTitel.Text;
            var contents = tbContents.Text;
            if (cm.CheckCharacterIntegrity(title, 1, 100, false) && cm.CheckCharacterIntegrity(contents, 1, 500, false))
            {
                var articlID = cm.CreateRandomID(10, "TABLE_BULLETINBOARD", "articlID");
                var userID = sm.Current.userID;
                var sqlQuery_getUserName = string.Format("SELECT userName " +
                                                         "FROM [dbo].[UserInfomation] " +
                                                         "WHERE userID = '{0}'", userID);
                var userName = cs.NewGetTableElement(sqlQuery_getUserName, "userName");
                //var userName = cs.GetUserInfomation(userID, "userName");

                ///時間の取得
                DateTime dt = DateTime.Now;
                string days = dt.ToString("yyyy/MM/dd HH:mm:ss");
                Debug.WriteLine("days:" + days);
                var sqlQuery = string.Format("INSERT [dbo].[TABLE_BULLETINBOARD] " +
                                              "(" +
                                                "articlID," +
                                                "userID," +
                                                "userName," +
                                                "articlTitle," +
                                                "articlContents," +
                                                "days" +
                                                ") " +
                                                "VALUES (" +
                                                            "'{0}'," +
                                                            "'{1}'," +
                                                            "N'{2}'," +
                                                            "N'{3}'," +
                                                            "N'{4}'," +
                                                            "'{5}'" +
                                                       ")", articlID, userID, userName, title, contents, days);
                cs.OperateSql_Insert_Delete_Update(sqlQuery);
                Response.Redirect(Request.RawUrl);

            }
            else
            {
                pAlertMessage.InnerText = "*入力内容に誤りがございます。";
            }

        }

        /// <summary>
        /// レスをつけるためにそのスレにアクセスできるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbInArticlButton_Click(object sender, EventArgs e)
        {
            var articlID = ((LinkButton)sender).CommandName;
            UrlCreator uc = new UrlCreator("./ArticlPage/InArticlPage.aspx");
            uc.SetParams("EntryID", articlID);
            uc.CreateUrl();

        }
    }
}