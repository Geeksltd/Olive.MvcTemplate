using MSharp;

namespace Model
{
    public class ContentBlock : EntityType
    {
        public ContentBlock()
        {
            InstanceAccessors("PasswordSuccessfullyReset", "LoginIntro");

            String("Key").Mandatory().Unique();
            BigString("Content").Mandatory();
        }
    }
}