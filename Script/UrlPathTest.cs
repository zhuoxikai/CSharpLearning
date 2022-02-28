using System;
using CSharpProfessional;
using Beisen.Security.Crypto;
using Newtonsoft.Json;
using Xunit;
using System.Security.Policy;

namespace Script
{
    public class UrlPathTest
    {

        public void Kafka()
        {
            try
            {
                // var time = (int) DateTime.Today.AddMinutes(5)
                //     .Subtract(DateTime.Now).TotalSeconds;
                // // while (true)
                // // {
                // //     Thread.Sleep(10);
                // //     var data = 1;
                // //     Console.WriteLine(data);
                // // }
                // //     
                // // var res = KafkaProtocol.Fetch("BeisenDTCCollectionRecruit", 21, 0, 0);
                string value = "M+jazQeyrrt2ikyfY7FX9UywMl8qa9eRHXO/NxczgCXaDzQ6dKQRkc0zEZ1MuPGj7o8+tGlUd17MQdRxYD6YDmXCHmQj1IryyMTxbUlfPhVFS1N7k8eTg4h3gTVwPpAsLf6Q5ryuLWJTYkCZZv1AZ8KPwz5b4m4g3ACoGkFLz+ak+N6fJwEXhRShKaHq8Dpz1fwBob2GSm3v9KUwbz3lXDErC7387VnAuHgXpS6Cujem9h2d0QeFLS5u18XdPK/ZhzVMdwnWud59DdrLKsrvWR6ybJT3v2h+xDaWTzACeRbi5zmWP84ahFHlioej3hsQk3V77I567SZhiKpldx3fRJulBC8JbAlYFNmHeSUVziIhn0cjapnoFrgqMll9t2+5+SN06WA44mFDA+8HtadpoozsrSDdO6Xwb2OfzsuUKsjyHZ2p8N0wu3WYaX29frPcBDBKmN/t9e3/XEFOWQmjKrNGwQRJhymPq0j4GDvlKvOMAZuY9+gOLn1/cybR58/dOTuBUvHtLQmLVxgsOUvO3W0IWG/w7FSjPCmtre0P9W9KiEcw7e6VsOpQFNdlWBPdXz73gt0SB+K1KCZ/OsZh5lRN6Yw8BCtQoT4+OcqAVpd8vRz6+hfLNuUM2t5Zfd1HtqcmgTIfo6nvwVABO3YaUCMQo/OqWNLidqO6BmiiqRbhttj9Pz1zK50g94WclPWusPqj4u3ASMSiszvmYbNjX7BUVnBD6rLI8bo7FntAqyXFkKDLLZ7";
                var s = cookis(value);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        // public ResponseModel<List<Department>> GetOrganizationNodes_After(bool isAuthorised,string queryMode,string type,string app,string metaObjName,PostParam postParam,string level,string rootOrgClickable,string includeDisable,List<Department> departmentList)
        // {
        //     logger.Debug($"isAuthorised:{isAuthorised}queryMode:{queryMode}type:{type}app:{app},metaObjName:{metaObjName},level:{level},rootOrgClickable:{rootOrgClickable},includeDisable:{includeDisable},postParam:{JsonConvert.SerializeObject(postParam)},departmentList:{JsonConvert.SerializeObject(departmentList)}");
        //     var appName = ApplicationContext.Current.ApplicationName;
        //     var userId = ApplicationContext.Current.UserId;
        //     var tenantId = ApplicationContext.Current.TenantId;
        //     logger.Debug($"appName{appName},UserId:{userId},TenantId:{tenantId}");
        // }
        private UserModel cookis(string value)
        {
            value = DecryptString(value);
            value = HttpContext.Current.Server.UrlDecode(value);
            return JsonConvert.DeserializeObject<Tests.UserModel>(value);
        }
        public static string DecryptString(string value)
        {
            string result = null;
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    result = BS_AES.AESDecrypt(Base64ToBytes(value), ProvidesKey.NoNeedKey, "DH9[0R]}Y}-ch,)na+{~GDyeb'>Q'9Qn", "R1Z.wI~\\-(k$)Py=");
                    return result;
                }

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public static byte[] Base64ToBytes(string base64)
        {
            char[] array = base64.ToCharArray();
            return Convert.FromBase64CharArray(array, 0, array.Length);
        }
        
        public class UserModel
        {
            public int TenantId { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserAvatar { get; set; }
            /// <summary>
            /// 推荐人ID
            /// </summary>
            public Guid ReferrerId { set; get; }
        
            public string Email { set; get; }
        
            public string Mobile { set; get; }
        
            public string DepartmentName { get; set; }
        
            public string StaffCode { get; set; }
        
            public string OpenId  { get; set; }

            public string UniqueKey { get; set; }

            public int RecommenderType { get; set; }

            public int BindingStatus { get; set; }        
        }
    }
}