using MSharp;

namespace Domain
{
    public class Administrator : SubType<User>
    {
        public Administrator()
        {
            String("Impersonation token", 40);
        }
    }
}