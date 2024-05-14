using Newtonsoft.Json;

namespace _2Sport_BE.ViewModels
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
    }
    public class UserVM : UserDTO
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }

    }
    public class UserCM : UserDTO
    {

    }
    public class UserUM : UserDTO
    {
        public int Id { get; set; }
        public string? Gender { get; set; }
        public string? Salary { get; set; }
        public string? Gmail { get; set; }
        public string? Phone { get; set; }
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
