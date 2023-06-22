using System.Text.Json.Serialization;

namespace NewsAPIs.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }

        public List<string> Roles { get; set; }

        public string Token { get; set; }



    }
}
