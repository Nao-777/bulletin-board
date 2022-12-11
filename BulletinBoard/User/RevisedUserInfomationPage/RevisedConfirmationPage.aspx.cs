using System;

namespace Board.BulletinBoard.User.RevisedUserInfomationPage
{
    public partial class RevisedConfirmationPage : System.Web.UI.Page
    {
        CommonSql cs = new CommonSql();
        SessionManager sm = new SessionManager();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var userID = sm.Current.userID;
                if (string.IsNullOrEmpty(userID))
                {
                    Response.Redirect("../../Form/Logout.aspx");
                }

                var loginID = Request.QueryString["loginID"];
                pConfirmationNewLoginID.InnerText = loginID;
                var userName = Request.QueryString["userName"];
                pConfirmationNewUserName.InnerText = userName;
                var userPassword = Request.QueryString["userPassword"];
                pConfirmationNewUserPassword.InnerText = userPassword;

            }
        }
        /// <summary>
        /// キャンセルの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("./RevisedUserInfomation.aspx");
        }

        /// <summary>
        /// 変更の決定をする処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbDecisionButton_Click(object sender, EventArgs e)
        {
            var userID = sm.Current.userID;
            var loginID = Request.QueryString["loginID"];
            var userName = Request.QueryString["userName"];
            var userPassword = Request.QueryString["userPassword"];
            var sqlQuery = string.Format("UPDATE [dbo].[UserInfomation] " +
                                              "SET loginID=N'{0}'," +
                                                  "userName=N'{1}'," +
                                                  "userPassword='{2}' " +
                                              "WHERE userID='{3}'", loginID, userName, userPassword, userID);
            cs.OperateSql_Insert_Delete_Update(sqlQuery);


            var squQuery_articlUserName = string.Format("UPDATE [dbo].[TABLE_BULLETINBOARD] " +
                                          "SET userName=N'{0}' " +
                                             "WHERE userID='{1}'", userName, userID);
            cs.OperateSql_Insert_Delete_Update(squQuery_articlUserName);

            var sqlQuery_userMessage = string.Format("UPDATE [dbo].[TABL_IN_ARTICL] " +
                                          "SET userName=N'{0}' " +
                                          "WHERE userID='{1}'", userName, userID);
            cs.OperateSql_Insert_Delete_Update(sqlQuery_userMessage);
            Response.Redirect("../MyPage.aspx");
        }

    }
}