namespace CoreApiTest.IClass
{
    public interface IUser
    {
        string GetUser(string id);
    }
    public class UserModel { 
        public string Id { get; set; }
        public string Name { get; set; }

    
    }
}
