using MSharp;

namespace Model
{
    public class ContentBlock : EntityType
    {
        public ContentBlock()
        {
            InstanceAccessors("PasswordSuccessfullyReset", "LoginIntro");
            
            String("Key", 200).Mandatory().Unique();
            
            String("Content").Mandatory().Lines(5);
        }
    }
}