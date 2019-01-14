using ClaimsIdentity = System.Security.Claims;

namespace ServeUp.Models
{
    public class Claim
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Claim)obj;

            return other.Key == Key && other.Value == Value;
        }
        
        public override int GetHashCode()
        {
            return Key.GetHashCode() + Value.GetHashCode();
        }

        public ClaimsIdentity.Claim ToIdentityClaim()
        {
            return new ClaimsIdentity.Claim(Key, Value);
        }

    }
}