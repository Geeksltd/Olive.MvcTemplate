using MSharp;

namespace Domain
{
    public class Settings : EntityType
    {
        public Settings()
        {
            PluralName("Settings").TableName("Settings").InstanceAccessors("Current");

            String("Name").Mandatory();
            Int("Password reset ticket expiry minutes").Mandatory();
            Int("Cache version").Mandatory();
        }
    }
}