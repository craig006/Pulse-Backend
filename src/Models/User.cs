using System.Collections.Generic;
using Newtonsoft.Json;
using ServeUp.Database;

namespace ServeUp.Models
{
    public class User : Document
    {
        public int CellNumber { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<Claim> ExtraClaims { get; set; }

        [JsonIgnore]
        public List<Claim> Claims 
        {
            get 
            {
                var claims = new List<Claim>();
                claims.Add(new Claim { Key = "cell", Value = CellNumber.ToString() });
                claims.Add(new Claim { Key = "email", Value = Email });
                claims.Add(new Claim { Key = "id", Value = Id });

                if((ExtraClaims?.Count).GetValueOrDefault() > 0)
                {
                    claims.AddRange(ExtraClaims);
                }
                return claims;
            }
        }
    }
}