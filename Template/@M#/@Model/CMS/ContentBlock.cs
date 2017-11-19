using MSharp;

namespace Domain
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