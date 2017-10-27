using MSharp;

namespace Model
{
    public class EmailTemplate : EntityType
    {
        public EmailTemplate()
        {
            InstanceAccessors("RecoverPassword");
            
            String("Key", 200).Mandatory().Unique();
            
            String("Subject", 200).Mandatory();
            
            String("Body").Mandatory().Lines(10);
            
            String("Mandatory placeholders", 200);
        }
    }
}