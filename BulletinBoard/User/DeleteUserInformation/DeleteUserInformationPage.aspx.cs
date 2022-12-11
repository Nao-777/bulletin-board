using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board.BulletinBoard.User.DeleteUserInformation
{
    public partial class DeleteUserInformationPage : System.Web.UI.Page
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
            }
        }
        /// <summary>
        /// ユーザ情報・削除されるユーザの記事を削除する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbDeleteButton_Click(object sender, EventArgs e)
        {
            var userID = sm.Current.userID;
            //記事の削除
            var articlDeleteSqlQuery = string.Format("DELETE [dbo].[TABLE_BULLETINBOARD] " +
                                                     "WHERE userID = '{0}'", userID);
            cs.OperateSql_Insert_Delete_Update(articlDeleteSqlQuery);
            //ユーザ情報の削除
            var userInformationSqlQuery = string.Format("DELETE FROM [dbo].[UserInfomation] " +
                                                        "WHERE userID = '{0}'", userID);
            cs.OperateSql_Insert_Delete_Update(userInformationSqlQuery);
            //レスの削除
            var userMessageDeleteSqlQuery = string.Format("DELETE FROM [dbo].[TABL_IN_ARTICL] " +
                                                          "WHERE userID = '{0}'", userID);
            cs.OperateSql_Insert_Delete_Update(userMessageDeleteSqlQuery);
            Response.Redirect("../../Form/Login.aspx");
        }
    }
}