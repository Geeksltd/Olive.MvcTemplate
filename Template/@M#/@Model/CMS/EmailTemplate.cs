using MSharp;

namespace Model
{
    public class EmailTemplate : EntityType
    {
        public EmailTemplate()
        {
            InstanceAccessors("Recover password");

            String("Subject").Mandatory();
            String("Key").Mandatory().Unique();
            BigString("Body", 10).Mandatory().Lines(10);
            String("Mandatory placeholders");
        }
    }
}
