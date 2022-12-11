/*
 ログインしているユーザの「ログインID」を管理
 */
using System.Web;

namespace Board
{
    public class SessionManager
    {
        /// <summary>
        /// 今取得しているセッションIDを保持する
        /// </summary>
        public SessionManager Current
        {
            get
            {
                SessionManager session = (SessionManager)HttpContext.Current.Session["__SessionManager__"];
                if (session == null)
                {
                    session = new SessionManager();
                    HttpContext.Current.Session["__SessionManager__"] = session;
                }
                //セッションID保有時間
                HttpContext.Current.Session.Timeout = 10;

                return session;
            }
        }
        public string userID { get; set; }
    }
}