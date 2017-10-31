using MSharp;

namespace Model
{
    public class Administrator : SubType<User>
    {
        public Administrator()
        {
            String("Impersonation token", 40);
        }
    }
}