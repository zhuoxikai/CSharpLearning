using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using Beisen.AppFSystem.ServiceInterface;
using Beisen.Common.Context;
using Beisen.Kafka.Protocol;
using Beisen.Kafka.Serialization;
using CSharpProfessional;
using Beisen.Security.Crypto;
using Newtonsoft.Json;
using Xunit;

namespace Test
{
    public class Tests
    {
        
        public void TreadTest()
        {
            new MultiThread().ThreadIncreasing();
        }

        public static void  Main()
        {
            try
            {
                int n = 128;

                n |= n >> 1;
                n |= n >> 2;
                n |= n >> 4;
                n |= n >> 8;
                n |= n >> 16;
                int res = (n + 1) >> 1;
                
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
                //内推cookies加密
                // string value = "M+jazQeyrrt2ikyfY7FX9UywMl8qa9eRHXO/NxczgCXaDzQ6dKQRkc0zEZ1MuPGj7o8+tGlUd17MQdRxYD6YDmXCHmQj1IryyMTxbUlfPhVFS1N7k8eTg4h3gTVwPpAsLf6Q5ryuLWJTYkCZZv1AZ8KPwz5b4m4g3ACoGkFLz+ak+N6fJwEXhRShKaHq8Dpz1fwBob2GSm3v9KUwbz3lXDErC7387VnAuHgXpS6Cujem9h2d0QeFLS5u18XdPK/ZhzVMdwnWud59DdrLKsrvWR6ybJT3v2h+xDaWTzACeRbi5zmWP84ahFHlioej3hsQk3V77I567SZhiKpldx3fRJulBC8JbAlYFNmHeSUVziIhn0cjapnoFrgqMll9t2+5+SN06WA44mFDA+8HtadpoozsrSDdO6Xwb2OfzsuUKsjyHZ2p8N0wu3WYaX29frPcBDBKmN/t9e3/XEFOWQmjKrNGwQRJhymPq0j4GDvlKvOMAZuY9+gOLn1/cybR58/dOTuBUvHtLQmLVxgsOUvO3W0IWG/w7FSjPCmtre0P9W9KiEcw7e6VsOpQFNdlWBPdXz73gt0SB+K1KCZ/OsZh5lRN6Yw8BCtQoT4+OcqAVpd8vRz6+hfLNuUM2t5Zfd1HtqcmgTIfo6nvwVABO3YaUCMQo/OqWNLidqO6BmiiqRbhttj9Pz1zK50g94WclPWusPqj4u3ASMSiszvmYbNjX7BUVnBD6rLI8bo7FntAqyXFkKDLLZ7K04eB+qaeMrO5";
                // string value = "M+jazQeyrrt2ikyfY7FX9WKEGN7iTJ5kpWhtFSRXp0rPO39Xr3jX6HWsFSCBK+BxWBlLuhdeAcoQxn09/02W2H9kRU7P6OdqiWXEXU+6/qd/ToexY2CkKs79ipZPLgEUHH8oWudEFNXt4o4XVEPM6JYPa02pyMy+CLSNPFei8XHuWWi9ReFstckU4ESuwU+PQIKzs9jcM5JfewdfRYX+E9snu8arIp6wR3oWV0d9AJ4mqFtDwC2dJPht2M7wieg0EbSflOdQPXuMsPStRETqFwizfoJDuYudoVkeH7jRmo6mB4nv9bZdXr7f0FnnUMK5nc/2OrbF/7MQwtoWEzo8yznwZMyldJCg8/oIQsmbDj9EOa3xYJnlIgbKeO21+S550thYglnngeRSBDjH5TMJhsYFpt5tNPr1caUssr9DglpuuLM4KfCibHh5wthZlwQPpSTBTFoGZF6hUOjcfhFTbKWgDOmj5+fTlSJXHhsSLR5cf1CZMvtMveFJbm8xOAdifvD1U7F6llRcZOm7dWYWa8G+RzSLOY8l0u74OAmeE66D8l6wOKnD/jVQNfOHXut61UKgZo0dJp50S+zp/gUZWbGDCbzBObSw1efWf3Z1yqj6KrsVscdAoI/aOvDMD3venesRH53sQTi4jIc9vhhdeDVrEUNnmsoaSKl7EN9UAVq7zVToDfAaqvwHd1z+AP6hnBnmnLJ+J+h1dE/XbmpV1TXqvM6DqDu4+o94/xIwyKIebNqdmq55DYves6xs+i/r";
                // var s = cookis(value);

                // var date = default(DateTime?);
                

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
        private static UserModel cookis(string value)
        {
            value = DecryptString(value);
            value = HttpUtility.UrlDecode(value);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
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