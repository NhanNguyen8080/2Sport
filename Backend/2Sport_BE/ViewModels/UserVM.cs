using Newtonsoft.Json;

namespace _2Sport_BE.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
    }
    public class UserCreateVM
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
    public class UserLogin
    {
        [JsonProperty("userName")]
        public string? UserName { get; set; }
        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
