namespace CoreApiTest.IClass
{
    public class User : IUser
    {
        public string GetUser(string id)
        {
            List<UserModel> userm = new List<UserModel>();
            userm.Add(new UserModel() { Id="1",Name="李四"});
            userm.Add(new UserModel() { Id="2",Name="王五"});
            userm.Add(new UserModel() { Id="3",Name="张八"});

            UserModel us=userm.Where(u=>u.Id==id).First();
            return us.Name.ToString();
        }
    }
}
