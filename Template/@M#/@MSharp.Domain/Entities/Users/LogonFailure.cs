using MSharp;

namespace Model
{
    public class LogonFailure : EntityType
    {
        public LogonFailure()
        {
            String("Email", 200).Mandatory();
            
            String("IP", 200).Mandatory();
            
            Int("Attempts").Mandatory();
            
            DateTime("Date").Mandatory();
        }
    }
}