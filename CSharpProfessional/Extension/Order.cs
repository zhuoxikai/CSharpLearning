using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CSharpProfessional.extend;
using Newtonsoft.Json;

namespace CSharpProfessional.Extension
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }

        public DateTime CreateTime { get; set; }
    }
    // List<Products> templateData = new List<Products>
    // {
    //     new Products() {CategoryId = "校园" , Id = "jobs"},
    //     new Products() {CategoryId = "社会" , Id = "jobs?rt=1"},
    //     new Products() {CategoryId = "实习" , Id = "jobs?rt=2"},
    //     new Products() {CategoryId = "自定义" , Id = "jobs?rt=3"}
    //     };
    // foreach (var data in templateData)
    // {
    //     var url = data.Id;
    //     var index = url.IndexOf("rt=", StringComparison.Ordinal);
    //     if (index != -1)
    //     {
    //         var rt = url.Substring(index+3, 1);
    //         data.Name = rt;
    //     }
    // }
    // Console.WriteLine(JsonConvert.SerializeObject(templateData));

    public class Order
    {
        static void Main(string[] args)
        {
            var input = new User()
            {
                UserId =  Guid.NewGuid(),
                ID = 1,
                Name = "zhangsan",
                List = new List<int>{ 1,2,3}
            };
            var input1 = new User()
            {
                UserId =  Guid.NewGuid(),
                ID = 1,
                Name = "zhangsan",
                List = new List<int>{ 1,2,3}
            };
            var input2 = new User()
            {
                UserId =  Guid.NewGuid(),
                ID = 1,
                Name = "zhangsan",
                List = new List<int>{ 1,2,3}
            };
            var list = new List<User>();
            list.Add(input);
            list.Add(input1);
            list.Add(input2);
            var res = list.First(x => x.ID == 0);
            var dat = res.List;
            
            //
            // GetData(input);
            //
            // input = null;
            // foreach (var rwe in input.List)
            // {
            //     Console.WriteLine(1);
            // }
        }
        
        private static void GetData(Base base2)
        {
            var input = base2 as User;
            var serializeObject = JsonConvert.SerializeObject(input);

        }
    }



}


// Set<string> sets = new Set<string>();
// String data = "123";
// var data2 = "123";
// sets.Add(data);
// sets.Add(data2);
// sets.Add(data2);
// var res = listProduct.Select(x => x.Name).ToArray();
// listProduct.AddRange(new List<Products>());


//
//
// int left = 1;
// var add = "12.00";
// var dec = "-12.00";
// var add1 = "+12";
// var dec1 = "-12";
//
// if (double.TryParse(add1,out double test) && int.TryParse(test.ToString(CultureInfo.CurrentCulture),out int point))
// {
//     Console.WriteLine(point);
// }
// int? a = left + int.Parse(add);
// Console.WriteLine($"a:{a}");

// var products = new Products();
// var hashCode = products.GetHashCode();


// 初始化数据
// List<Products> listProduct = new List<Products>()
// {
//     new Products() 
//         {Id = 1, CategoryId = 1, Name = "", Price = 100},
//     new Products()
//         {Id = 1, CategoryId = 1, Name = "", Price = 100},
//     new Products()
//         {Id = 3, CategoryId = 2, Name = "", Price = 90, CreateTime = DateTime.Now.AddMonths(-3)},
//     new Products()
//         {Id = 4, CategoryId = 3, Name = "  ", Price = 97, CreateTime = DateTime.Now.AddMonths(-1)}
// };
// var productsEnumerable = listProduct.ToList().Distinct();
// Set<Products> set = new Set<Products>(null);
// set.Add(new Products() {Id = 1, CategoryId = 1, Name = "", Price = 100});
// set.Add(new Products() {Id = 1, CategoryId = 1, Name = "", Price = 100});
// set.Add(new Products() {Id = 1, CategoryId = 1, Name = "", Price = 100});



// var result = listProduct.Distinct();

            // List<int> listInt = new List<int>() {-1,0000,};
            // // 注意：ThenBy()的方法语法和查询表达式写法有些不同。
            // Console.WriteLine("方法语法升序排序");
            // // 1、查询方法，按照商品分类升序排序,如果商品分类相同在按照价格升序排序 返回匿名类
            // var list = listProduct.OrderBy(p => p.CategoryId).ThenBy(p => p.Price).Select(p =>
            //         new {id = p.CategoryId, ProductName = p.Name, ProductPrice = p.Price, PublishTime = p.CreateTime})
            //     .ToList();
            // foreach (var item in list)
            // {
            //     Console.WriteLine($"item:{item}");
            // }
            //
            // Console.WriteLine("查询表达式升序排序");
            // var listExpress = from p in listProduct
            //     orderby p.CategoryId, p.Price
            //     select new
            //     {
            //         id = p.CategoryId, ProductName = p.Name, ProductPrice = p.Price, PublishTime = p.CreateTime
            //     };
            // foreach (var item in listExpress)
            // {
            //     Console.WriteLine($"item:{item}");
            // }
            //
            // Console.WriteLine("方法语法降序排序");
            // // 1、查询方法，按照商品分类降序排序,如果商品分类相同在按照价格升序排序 返回匿名类
            // var listDesc = listProduct.OrderByDescending(p => p.CategoryId).ThenBy(p => p.Price).Select(p =>
            //         new {id = p.CategoryId, ProductName = p.Name, ProductPrice = p.Price, PublishTime = p.CreateTime})
            //     .ToList();
            // foreach (var item in listDesc)
            // {
            //     Console.WriteLine($"item:{item}");
            // }
            //
            // Console.WriteLine("查询表达式降序排序");
            // var listExpressDesc = from p in listProduct
            //     orderby p.CategoryId descending, p.Price
            //     select new
            //     {
            //         id = p.CategoryId, ProductName = p.Name, ProductPrice = p.Price, PublishTime = p.CreateTime
            //     };
            // foreach (var item in listExpressDesc)
            // {
            //     Console.WriteLine($"item:{item}");
            // }
            //
            // Console.ReadKey();