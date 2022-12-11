using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board.BulletinBoard.User.RevisedUserInfomationPage
{
    public partial class RevisedUserInfomation : System.Web.UI.Page
    {
        CommonSql cs = new CommonSql();
        CommonMethod cm = new CommonMethod();
        SessionManager sm = new SessionManager();
        UrlCreator uc = new UrlCreator("./RevisedConfirmationPage.aspx");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userID = sm.Current.userID;
                if (string.IsNullOrEmpty(userID))
                {
                    Response.Redirect("../../Form/Logout.aspx");
                }
                //ログインIDを取得
                var sqlQuery_getUserLoginID = string.Format("SELECT loginID " +
                                                        "FROM [dbo].[UserInfomation] " +
                                                        "WHERE userID = '{0}'", userID);
                var loginID = cs.NewGetTableElement(sqlQuery_getUserLoginID, "loginID");
                pOldLoginID.InnerText = loginID;
                //userName取得
                var sqlQuery_getUserName = string.Format("SELECT userName " +
                                                        "FROM [dbo].[UserInfomation] " +
                                                        "WHERE userID = '{0}'", userID);
                var userName = cs.NewGetTableElement(sqlQuery_getUserName, "userName");
                pOldUserName.InnerText = userName;
                //パスワードを取得
                var sqlQuery_getUserPassword = string.Format("SELECT userPassword " +
                                                       "FROM [dbo].[UserInfomation] " +
                                                       "WHERE userID = '{0}'", userID);
                var userPassword = cs.NewGetTableElement(sqlQuery_getUserPassword, "userPassword");
                pOldPassword.InnerText = userPassword;

            }
        }

        /// <summary>
        /// 入力されて値の文字チェックを行い確認画面へ遷移する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbRevisedConfirmationButton_Click(object sender, EventArgs e)
        {
            var newLoginID = tbNewLoginID.Text;
            var newUserName = tbNewUserName.Text;
            var newUserPassword = tbNewPasswordText.Text;


            if (cm.CheckCharacterIntegrity(newLoginID, 4, 20, true) && cm.CheckCharacterIntegrity(newUserName, 0, 20, false) && cm.CheckCharacterIntegrity(newUserPassword, 8, 20, true))
            {
                //クエリ文字列で確認画面へ変数を渡す
                uc.SetParams("loginID", newLoginID);
                uc.SetParams("userName", newUserName);
                uc.SetParams("userPassword", newUserPassword);
                uc.CreateUrl();

            }
            else
            {
                pAlertMessage.InnerText = "*入力された内容が違います";
            }

        }
    }
}