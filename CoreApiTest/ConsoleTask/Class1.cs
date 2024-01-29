//DateTime dt1 = DateTime.Now;
//Console.WriteLine("Hello, World!");
//Task.Run(async () => {
//    var sre = Tesk1();
//    Console.WriteLine(await sre);
//});
//Console.WriteLine("+++");
//Tesk2();
//Console.WriteLine("完成" + "----" + Thread.CurrentThread.ManagedThreadId);
//DateTime dt2 = DateTime.Now;
//TimeSpan ts = dt2.Subtract(dt1);
//Console.WriteLine("程序耗时：{0}ms.", ts.TotalMilliseconds);
//Thread.Sleep(5000);


//async Task<string> Tesk1()
//{
//    string str = await Test();
//    Console.WriteLine(str + "----" + Thread.CurrentThread.ManagedThreadId);
//    return str;
//}

//void Tesk2()
//{
//    Console.WriteLine(" Task2执行----" + Thread.CurrentThread.ManagedThreadId);
//}
//async Task<string> Test()
//{
//    var a = await Task.Run(() =>
//    {
//        Console.WriteLine("111" + "----" + Thread.CurrentThread.ManagedThreadId);
//        Thread.Sleep(5000);
//        Console.WriteLine("222" + "----" + Thread.CurrentThread.ManagedThreadId);
//        return "444";
//    });
//    return a.ToString();
//}



/*int boole=1;
while (boole < 5)
{
    Console.WriteLine("请输入");
     string ad= Console.ReadLine();
    if (ad == "1")
    {
        async5S();
    }
    else {
        Putou();
    }
    boole++;
}

Console.WriteLine("111--"+Thread.CurrentThread.ManagedThreadId);

Console.ReadLine();



async Task async5S()
{
    await Task.Run(() =>
    {
        Thread.Sleep(5000);
        Console.WriteLine("延迟五秒----" + Thread.CurrentThread.ManagedThreadId);

    });

}

async Task TaskAsync() {
    Thread.Sleep(5000);
    Console.WriteLine("延迟五秒");
}

void Putou() {
    Console.WriteLine("普通方法----"+ Thread.CurrentThread.ManagedThreadId);
}*/