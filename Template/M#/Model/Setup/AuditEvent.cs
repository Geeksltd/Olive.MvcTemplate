using MSharp;

namespace Domain
{
    public class AuditEvent : EntityType
    {
        public AuditEvent()
        {
            Cachable(false).Implements("Olive.Audit.IAuditEvent");

            String("User id");
            DateTime("Date").Mandatory().Default(cs("LocalTime.Now")).DefaultFormatString("g");
            String("Event").Mandatory();
            String("Item type");
            String("Item id", 500);
            BigString("Item data");
            BigString("Item group");
            String("User Ip");
        }
    }
}