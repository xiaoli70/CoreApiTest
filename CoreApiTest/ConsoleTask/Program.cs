// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Routing;
using Net6Api.Domain.Helper;
using SqlSugar;



int[] nums = { 1, 2, 3, 4, 5, 6, 7 };
//RemoveDuplicates(nums);
Rotate(nums, 3);

int RemoveDuplicates(int[] nums)
{
    if (nums.Length <= 2)
        return nums.Length; ;
    int lastNumberIndex = 1;
    for (int i = 2; i < nums.Length; i++)
    {
        if (nums[i] == nums[lastNumberIndex] && nums[lastNumberIndex] == nums[lastNumberIndex - 1])
            continue; ;
        nums[++lastNumberIndex] = nums[i];
    }

    return ++lastNumberIndex;

}
void Rotate(int[] nums, int r)
{
    int count = 0;
    int len = nums.Length;
    while (count < r)
    {
        int k7 = nums[len - 1];
        for (int k = 1; k <= len; k++)
        {
            if (k == len)
            {
                nums[0] = k7;
                break;
            }
            nums[len - k] = nums[len - k - 1];
        }
        count++;
    }
}
#region sqlSugar添加表

//SqlSugarScope scope = new SqlSugarScope(new ConnectionConfig()
//{
//    //创建数据表时可以直接在一个类似NUnit的测试类中来做，比较方便
//    //但在实际的.NET CORE Web开发使用时，这些配置信息都要在StartUp中读取项目的配置文件来获得
//    ConnectionString = "Data Source=ZEROTPC003\\MSSQLSERVER2022;Initial Catalog=MyFristDB; User ID=sa;Password=sa123456;",
//    //SqlSugar默认支持多种数据库，你也不需要数据库连接的依赖
//    DbType = DbType.SqlServer,

//    //是否自动关闭连接池
//    IsAutoCloseConnection = true

//});
//scope.CodeFirst.InitTables(typeof(Student));
#endregion

//while (boole < 5)
//{
//    Console.WriteLine("请输入");
//     string ad= Console.ReadLine();
//    if (ad == "1")
//    {
//        async5S();
//    }
//    else {
//        Putou();
//    }
//    boole++;
//}

//Console.WriteLine("111--"+Thread.CurrentThread.ManagedThreadId);

//Console.ReadLine();



//async Task async5S()
//{
//    await Task.Run(() =>
//    {
//        Thread.Sleep(5000);
//        Console.WriteLine("延迟五秒----" + Thread.CurrentThread.ManagedThreadId);

//    });

//}

//async Task TaskAsync() {
//    Thread.Sleep(5000);
//    Console.WriteLine("延迟五秒");
//}

//void Putou() {
//    Console.WriteLine("普通方法----"+ Thread.CurrentThread.ManagedThreadId);
//}