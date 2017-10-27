using MSharp;

namespace Model
{
    public class ApplicationEvent : EntityType
    {
        public ApplicationEvent()
        {
            Cachable(false).Implements("IApplicationEvent");
            
            String("User id", 200);
            
            DateTime("Date").Mandatory().Default("c#:LocalTime.Now").DefaultFormatString("g");
            
            String("Event", 200).Mandatory();
            
            String("Item type", 200);
            
            String("Item key", 500);
            
            String("Data");
            
            String("IP", 200);
        }
    }
}