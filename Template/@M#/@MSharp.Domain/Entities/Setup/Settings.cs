using MSharp;

namespace Model
{
    public class Settings : EntityType
    {
        public Settings()
        {
            InstanceAccessors("Current").PluralName("Settings").TableName("Settings");
            
            String("Name", 200).Mandatory();
            
            Int("Password reset ticket expiry minutes").Mandatory();
            
            Int("Cache version").Mandatory();
        }
    }
}