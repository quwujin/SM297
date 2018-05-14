using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    /// <summary>
    /// 企业付款
    /// </summary>
    public class TransferAPI
    {
        private static string TransferAPIURL = "http://redpack.esmartwave.com/Controller/TransferAPI.ashx";

        /// <summary>
        /// 微信转账
        /// </summary>
        /// <param name="ProjectId">项目ID</param>
        /// <param name="SecretKey">项目秘钥</param>
        /// <param name="openid">用户openid</param>
        /// <param name="partner_trade_no">转账单号 格式：红包订单号+项目编号，如：201601021001000010SM184</param>
        /// <param name="amount">转账金额（100为一元）</param>
        /// <returns></returns>
        public static bool Transfer(int ProjectId, string SecretKey, string openid, string partner_trade_no, int amount)
        { 
            StringBuilder Parameter = new StringBuilder();
            Parameter.AppendFormat("ProjectId={0}", ProjectId);
            Parameter.AppendFormat("&openid={0}", openid);
            Parameter.AppendFormat("&partner_trade_no={0}", partner_trade_no);
            Parameter.AppendFormat("&amount={0}", amount);

            try
            {
                string VildSign = Common.getMD5.MD5(ProjectId + partner_trade_no + openid + amount + SecretKey).ToUpper();
                Parameter.AppendFormat("&VildSign={0}", VildSign);
                string Result = Common.WebNet.doPost(TransferAPIURL, Parameter.ToString());

                ReturnMessage Return = Common.JsonHelper.JsonDeserialize<ReturnMessage>(Result);

                if (Return.IsSuccess) {
                    return true;
                }

                //Log(Return.Message)//记录失败信息

                //HttpContext.Current.Response.Write(Return.Message);
                //HttpContext.Current.Response.End();
            }
            catch (Exception ex) { 
                
            }
            return false;
         
        }

        public class ReturnMessage
        {
            public bool IsSuccess { get; set; } 
            public string Message { get; set; } 
            public string Text { get; set; }
            public string ReturnUrl { get; set; }
            public IDictionary<string, object> ResultData{ get; set; }  
            public Exception Exception { get; set; }
        } 
    }
}
