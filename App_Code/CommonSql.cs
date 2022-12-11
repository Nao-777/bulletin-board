/*
    共通のSQLのメソッド
 */
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Board
{
    public class CommonSql
    {
        string CONNECTION_STRING = ConfigurationManager.AppSettings["ConnectionString"];


        /// <summary>
        /// 指定された、INSERT,DELETE,UPDATEのどれかの処理を行う。
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void OperateSql_Insert_Delete_Update(string sqlQuery)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var transcation = connection.BeginTransaction())
                using (var command = new SqlCommand()
                {
                    Connection = connection,
                    Transaction = transcation
                })
                {
                    try
                    {
                        //コマンドのセット
                        command.CommandText = sqlQuery;
                        //コマンドの実行
                        command.ExecuteNonQuery();
                        //コミット
                        transcation.Commit();

                        Debug.WriteLine("INSERT・DELETE・UPDATE成功です");

                    }
                    catch
                    {
                        Debug.WriteLine("INSERT・DELETE・UPDATE失敗しました");
                        //ロールバック
                        transcation.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// テーブル名と、取得したい要素がある列名　
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="comparison"></param>
        /// <param name="tableColumeName"></param>
        /// <returns></returns>
        public string NewGetTableElement(string sqlQuery, string tableColumeName)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                using (var command = new SqlCommand()
                {
                    Connection = connection,
                    Transaction = transaction
                })
                {
                    try
                    {
                        command.CommandText = sqlQuery;
                        //command.ExecuteReader();
                        //Debug.WriteLine(command.CommandText);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine("NewGetTableElement:" + reader[tableColumeName]);
                                return (string)reader[tableColumeName];
                            }
                            reader.Close();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        Debug.WriteLine("失敗しました");
                        //ロールバック
                        transaction.Rollback();
                        throw;
                    }
                }

            }
            //何も取得できなかったら空の値を変えす
            return "";
        }
        /// <summary>
        /// テーブルから要素を探す bool型
        /// </summary>
        /// <param name="sqlQuery">SQL文</param>
        /// <param name="comparison">比較する要素</param>
        /// <param name="tableColumName">テーブルの列名の文字列</param>
        /// <returns></returns>
        public bool FindTableElement(string sqlQuery, string comparison, string tableColumName)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                using (var cmd = new SqlCommand()
                {
                    Connection = connection,
                    Transaction = transaction
                })
                {
                    try
                    {
                        cmd.CommandText = sqlQuery;
                        using (var reader = cmd.ExecuteReader())
                        {
                            var result = "";
                            while (reader.Read())
                            {
                                result = (string)reader[tableColumName];
                                if (comparison.Equals(result))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("失敗しました");
                        //ロールバック
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// データテーブルを取得して、Repeaterコントロールで表示する
        /// </summary>
        /// <param name="repeaterId">表示したいRepeaterコントロールのID</param>
        /// <param name="sqlQuery">SQLの命令文</param>
        public void DisplayElements(Repeater repeaterId, string sqlQuery)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                using (var cmd = new SqlCommand()
                {
                    Connection = connection,
                    Transaction = transaction
                })
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        cmd.CommandText = sqlQuery;
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            //Repeaterタグにデータテーブルを入れる
                            repeaterId.DataSource = dt;
                            repeaterId.DataBind();
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.Message);
                        throw;
                    }

                }
            }
        }

        /// <summary>
        ///  データテーブルを取得して、行数があるか確認する（ページング処理の「次へボタン」表示の際に使用）
        /// </summary>
        /// <param name="sqlQuery_Next"></param>
        /// <returns></returns>
        public bool JudgeNextData(string sqlQuery)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                using (var cmd = new SqlCommand()
                {
                    Connection = connection,
                    Transaction = transaction
                })
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        cmd.CommandText = sqlQuery;
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            if (dt.Rows.Count == 0)
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception.Message);
                        throw;
                    }

                }
            }
            return true;
        }
    }
}