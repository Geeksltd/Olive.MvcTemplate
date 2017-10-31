using MSharp;

namespace Model
{
    public class EmailTemplate : EntityType
    {
        public EmailTemplate()
        {
            InstanceAccessors("Recover password");
            
            String("Key").Mandatory().Unique();
            String("Subject").Mandatory();
            BigString("Body", 10).Mandatory().Lines(10);
            String("Mandatory placeholders");
        }
    }
}