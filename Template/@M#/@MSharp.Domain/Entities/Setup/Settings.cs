using MSharp;

namespace Model
{
    public class Settings : EntityType
    {
        public Settings()
        {
            InstanceAccessors().PluralName("Settings").TableName("Settings");
            
            String("Name").Mandatory();
            Int("Password reset ticket expiry minutes").Mandatory();
            Int("Cache version").Mandatory();
        }
    }
}