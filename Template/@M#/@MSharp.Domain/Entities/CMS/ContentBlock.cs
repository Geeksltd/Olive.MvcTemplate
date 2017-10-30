using MSharp;

namespace Model
{
    public class ContentBlock : EntityType
    {
        public ContentBlock()
        {
            InstanceAccessors();
            
            String("Key").Mandatory().Unique();
            BigString("Content").Mandatory().Lines(5);
        }
    }
}