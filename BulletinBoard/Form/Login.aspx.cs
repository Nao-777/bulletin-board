/*
 ==========================================================================================================
ログインページ
==========================================================================================================
 */
using System;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Board.BulletinBoard.Form
{
    public partial class Login : System.Web.UI.Page
    {
        SessionManager sm = new SessionManager();
        CommonSql cs = new CommonSql();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        protected void lbLogin_Click(object sender, EventArgs e)
        {
            var MasterPage = Master.FindControl("ContentPlaceHolder1");
            var loginId = (TextBox)MasterPage.FindControl("tbLoginID");
            Debug.WriteLine("loginId.Text=" + loginId.Text);
            var loginIdText = loginId.Text;
            var loginPassword =(TextBox)MasterPage.FindControl("tbPasswordID");
            var loginPasswordText = loginPassword.Text;
            var alertMsg = (Label)MasterPage.FindControl("pAlertMessage");
            if (string.IsNullOrEmpty(loginIdText) || string.IsNullOrEmpty(loginPasswordText))
            {
                //pAlertMessage.InnerText = "*入力内容に誤りがございます";
                alertMsg.Text = "*入力内容に誤りがございます";
            }
            else
            {
                var SqlQuery = string.Format("SELECT userID " +
                                             "FROM [dbo].[UserInfomation] " +
                                             "WHERE loginID='{0}' " +
                                             "AND " +
                                             "userPassword='{1}'", loginIdText, loginPasswordText);
                //入力文字列がUserInfomationに存在するか確認メソッド
                var userID = cs.NewGetTableElement(SqlQuery, "userID");
                Debug.WriteLine("login screen:userID=" + userID);

                if (!userID.Equals(""))
                {

                    sm.Current.userID = userID;

                    Response.Redirect("../MainPage/BulletinBoardPage.aspx");
                }
                else
                {
                     alertMsg.Text = "*入力内容に誤りがございます";
                    Debug.WriteLine("文字認識");
                }
            }
        }
    }
}