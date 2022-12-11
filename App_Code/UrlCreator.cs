/*
 指定したパスにクエリ文字列を付加する
 */
using System.Collections.Specialized;
using System.Web;

namespace Board
{
    public class UrlCreator
    {
        string pathName;
        NameValueCollection urlQuery = HttpUtility.ParseQueryString("");
        /// <summary>
        /// クエリ文字列を使用して、データを渡したいページのURLを渡す
        /// </summary>
        /// <param name="pathName">クエリ文字列を使用して、データを渡したいページのURL</param>
        public UrlCreator(string pathName)
        {
            this.pathName = pathName;

        }
        /// <summary>
        /// クエリ文字列を作成
        /// </summary>
        /// <param name="queryName">クエリ文字列の名前</param>
        /// <param name="valueName">クエリ文字列の値</param>
        public void SetParams(string queryName, string valueName)
        {

            this.urlQuery.Add(queryName, valueName);
            //HttpContext.Current.Response.Redirect(this.pathName+urlQuery,false);


        }
        /// <summary>
        /// クエリ文字列を使用して、データを渡したいページのURLとクエリ文字列を結合する
        /// </summary>
        public void CreateUrl()
        {
            HttpContext.Current.Response.Redirect(this.pathName + "?" + this.urlQuery, false);
        }

    }
}