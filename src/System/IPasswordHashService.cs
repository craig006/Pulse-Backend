namespace ServeUp.System
{
    public interface IPasswordHashService
    {
        string Hash(string password);

        bool Verify(string password, string hashedpassword);

        void EnsureVerified(string password, string hashedpassword);
    }
}