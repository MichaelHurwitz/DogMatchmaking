namespace dogMatchmaking.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public List<DogModel> Dogs { get; set; } = [];
    }
}
