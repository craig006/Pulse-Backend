namespace ServeUp.Models
{
    public class Credentials
    {
        public int CellNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Credentials(int cellNumber, string email, string password)
        {
            CellNumber = cellNumber;
            Email = email;
            Password = password;
        }
    }
}