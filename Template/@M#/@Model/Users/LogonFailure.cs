using MSharp;

namespace Domain
{
    public class LogonFailure : EntityType
    {
        public LogonFailure()
        {
            String("Email").Mandatory();
            String("IP").Mandatory();
            Int("Attempts").Mandatory();
            DateTime("Date").Mandatory();
        }
    }
}