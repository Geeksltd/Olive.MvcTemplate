using MSharp;

namespace Model
{
    public class ApplicationEvent : EntityType
    {
        public ApplicationEvent()
        {
            Cachable(false).Implements("IApplicationEvent");
            
            String("User id");
            DateTime("Date").Mandatory().Default("c#:LocalTime.Now").DefaultFormatString("g");
            String("Event").Mandatory();
            String("Item type");
            String("Item key", 500);
            BigString("Data");
            String("IP");
        }
    }
}