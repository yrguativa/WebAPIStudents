using System.Collections.Generic;

namespace StudentsAPI.Models.Security
{
    public class TokenModel
    {
        public string Token { get; set; }
        public bool Succeeded { get; set; }
        public IEnumerable<(string, string)> Errors { get; set; }
    }
}
