namespace dogMatchmaking.Models
{
    public class DogModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
