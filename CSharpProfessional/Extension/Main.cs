using System;
using System.Collections.Generic;
using System.Linq;
using EmitMapper;
using EmitMapper.MappingConfiguration;


namespace CSharpProfessional.Extension
{
    public class Main
    {
        public class UserInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
        }
        public class UserInfoDTO
        {
            public string name { get; set; }

            public string userAddress { get; set; }
        }
        public static void main()
        {
            var value = new MyDTOChild()
            {
                lists = {1,2}
            };
            // List<UserInfo> lists = new List<UserInfo>();
            // lists.Add(new UserInfo());
            // lists.Add(null);
            // lists.Add(new UserInfo()
            // {
            //     id = 1
            // });
            // if (lists.All(n => n != null))
            // {
            //     Console.WriteLine("here");
            // }

            // var date =  string.Format("内推运营二维码{0:yyMMddHHmmss}.{1}", DateTime.Now, ".png");
            //     var user = new UserInfo { 
            //         id = 12, 
            //         name = "张三", 
            //         address = "北京",
            //     };
            //     ObjectsMapper<UserInfo, UserInfoDTO> mapper = ObjectMapperManager.DefaultInstance.GetMapper<UserInfo, UserInfoDTO>(
            //     new DefaultMapConfig()
            //         .MatchMembers((x, y) =>
            //         {
            //             if (x == "address" && y == "userAddress")
            //             {
            //                 return true;
            //             }
            //             return x == y;
            //         })
            // );
            // UserInfoDTO userdto = mapper.Map(user);

            // var value1 = new MyDTOChild("Grui", new Guid()).GetName();
            // Console.WriteLine("value:{0},value1:{1}",value,value1);

        }
    }
}