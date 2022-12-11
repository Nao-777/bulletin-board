/*
 =============================================
マイページ
==============================================
 */
using System;
using System.Diagnostics;


namespace Board.BulletinBoard.User
{
    public partial class MyPage : System.Web.UI.Page
    {
        CommonSql cs = new CommonSql();
        SessionManager sm = new SessionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ///ユーザIDを取得
                var userID = sm.Current.userID;
                Debug.WriteLine("SESSION_KEY_USERID:" + userID);

                ///ログインしているか確認
                if (string.IsNullOrEmpty(userID))
                {
                    Response.Redirect("../Form/Logout.aspx");
                }


                var sqlQuery_getLoginID = string.Format("SELECT loginID " +
                                                        "FROM [dbo].[UserInfomation] " +
                                                        "WHERE userID = '{0}'", userID);
                var loginID = cs.NewGetTableElement(sqlQuery_getLoginID, "loginID");
                pLoginID.InnerText = loginID;

                var sqlQuery_getUserName = string.Format("SELECT userName " +
                                                        "FROM [dbo].[UserInfomation] " +
                                                        "WHERE userID = '{0}'", userID);
                var userName = cs.NewGetTableElement(sqlQuery_getUserName, "userName");
                pUserName.InnerText = userName;
            }
        }
        /// <summary>
        /// ユーザ情報編集ページへ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbModifyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("./RevisedUserInfomationPage/RevisedUserInfomation.aspx");
        }
        /// <summary>
        /// 退会ページに遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbDeleteUserInfomationButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("./DeleteUserInformation/DeleteUserInformationPage.aspx");
        }
    }
}