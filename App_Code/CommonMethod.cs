/*
共通（文字列操作メソッド）
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Board
{
    public class CommonMethod
    {
        CommonSql commonSql = new CommonSql();
        /// <summary>
        /// 文字の整合性(characterIntegrity)を確認
        /// </summary>
        /// <param name="targetWord">整合性を確認したい文字列</param>
        /// <param name="lowerLimit">下限値の設定</param>
        /// <param name="upperLimit">上限値の設定</param>
        /// <param name="checkedAlphabet">整合したい文字列がアルファベット化確認する。</param>
        /// <returns></returns>
        public bool CheckCharacterIntegrity(string targetWord, int lowerLimit, int upperLimit, bool checkedAlphabet)
        {

            if (string.IsNullOrEmpty(targetWord))
            {
                return false;
            }
            if ((targetWord.Length > upperLimit) || (targetWord.Length < lowerLimit))
            {
                return false;
            }
            if (checkedAlphabet)
            {
                if (!CheckAlphabetNum(targetWord))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 英数字の判定するメソッド
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        private bool CheckAlphabetNum(string targetWord)
        {
            var words = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var wordCount = targetWord.Length;
            var judge = false;
            for (int i = 0; i < wordCount; i++)
            {
                var target = targetWord[i];
                Debug.WriteLine("targets[i]:" + targetWord[i]);
                for (int j = 0; j < words.Length; j++)
                {
                    Debug.WriteLine("words[j]:" + words[j]);
                    if (words[j] == target)
                    {
                        judge = true;
                        break;
                    }

                }
                if (judge == false)
                {
                    return false;
                }
            }

            return true;
        }



        /// <summary>
        ///  IDを作成するメソッド
        /// </summary>
        /// <param name="numberOfDigits">IDの桁数</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="tableColumName">列名</param>
        /// <returns></returns>
        public string CreateRandomID(int numberOfDigits, string tableName, string tableColumName)
        {
            var words = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var createdId = new char[numberOfDigits];
            var random = new Random();

            for (int i = 0; i < createdId.Length; i++)
            {
                ///Random.Next()引数に指定した数値を最大値として乱数を取得できる
                createdId[i] = words[random.Next(words.Length)];
            }
            //重複していないか確認
            var result = new String(createdId);
            var sqlQuery = @"SELECT " + tableColumName + @"  
                         FROM [dbo].[" + tableName + "]";
            if (commonSql.FindTableElement(sqlQuery, result, tableColumName) == false)
            {
                result = CreateRandomID(numberOfDigits, tableName, tableColumName);
            }
            return result;
        }

    }
}