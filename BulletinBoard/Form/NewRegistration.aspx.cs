/*
=======================================================================================================
新規登録ページ
=======================================================================================================
*/using System;

namespace Board.BulletinBoard.Form
{
    public partial class NewRegistration : System.Web.UI.Page
    {
        CommonMethod cm = new CommonMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        /// <summary>コーディングナレッジに従って書き直したコード
        /// </summary>確認画面へ遷移するボタン
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lbNewRegistrationButton_Click(object sender, EventArgs e)
        {
            //入力されたユーザ名、ユーザログインID、パスワード
            var newUserName = tbNewNameID.Text;
            var newLoginID = tbNewLoginID.Text;
            var newUserPassword = tbNewPasswordID.Text;

            //入力された上記の値が条件に合うか検証している
            //trueの場合：新規登録確認画面（NewRegistrationConfirmation.aspx）に飛ぶ
            //falseの場合；入力内容に誤りがないか確認する
            if (cm.CheckCharacterIntegrity(newLoginID, 4, 20, true) && cm.CheckCharacterIntegrity(newUserName, 0, 20, false) && cm.CheckCharacterIntegrity(newUserPassword, 8, 20, true))
            {
                UrlCreator uc = new UrlCreator("./NewRegistrationConfirmation.aspx");
                uc.SetParams("userName", newUserName);
                uc.SetParams("loginID", newLoginID);
                uc.SetParams("userPassword", newUserPassword);
                uc.CreateUrl();
            }
            else
            {
                pAlertMsg.InnerText ="*入力内容に誤りがございます。";
            }
        }
    }
}