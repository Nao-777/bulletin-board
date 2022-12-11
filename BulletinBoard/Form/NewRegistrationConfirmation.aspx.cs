/*
 =======================================================================================================
  新規登録（確認）ページ
 =======================================================================================================
 */
using System;

namespace Board.BulletinBoard.Form
{
    public partial class NewRegistrationConfirmation : System.Web.UI.Page
    {
        CommonSql cs = new CommonSql();
        CommonMethod cm = new CommonMethod();
        SessionManager sm = new SessionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //NewRegistration.aspxから値を取得する
                pUserName.InnerText = Request.QueryString["userName"];
                pLoginID.InnerText = Request.QueryString["loginID"];
                pUserPassword.InnerText = Request.QueryString["userPassword"];
            }

        }

        protected void lbCancelBotton_Click(object sender, EventArgs e)
        {
            Response.Redirect("./NewRegistration.aspx");

        }
        /// <summary>
        /// 確定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbNewRegistrationButton_Click(object sender, EventArgs e)
        {
            //入力されたユーザ名、ログインID、パスワードをクエリ文字列として受け取る
            var newUserName = Request.QueryString["userName"];
            var newLoginID = Request.QueryString["loginID"];
            var newUserPassword = Request.QueryString["userPassword"];
            //ユーザIDをランダムに生成
            var userID = cm.CreateRandomID(8, "UserInfomation", "userID");
            var sqlQuery = string.Format("INSERT [dbo].[userInfomation] " +
                                          "(" +
                                            "userID," +
                                            "loginID," +
                                            "userName," +
                                            "userPassword" +
                                          ") " +
                                          "VALUES (" +
                                                    "'{0}'," +
                                                    "'{1}'," +
                                                    "N'{2}'," +
                                                    "'{3}'" +
                                                   ")", userID, newLoginID, newUserName, newUserPassword);
            //cs.OperateTableInfomation(sqlQuery);
            cs.OperateSql_Insert_Delete_Update(sqlQuery);
            sm.Current.userID = userID;
            Response.Redirect("../MainPage/BulletinBoardPage.aspx");
        }
    }
}