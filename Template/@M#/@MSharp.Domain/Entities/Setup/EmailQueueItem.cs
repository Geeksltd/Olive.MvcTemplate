using MSharp;

namespace Model
{
    public class EmailQueueItem : EntityType
    {
        public EmailQueueItem()
        {
            Cachable(false).SoftDelete().Implements("Olive.Services.Email.IEmailQueueItem");
            
            String("Body").Lines(5);
            
            DateTime("Date").Mandatory().Default("c#:LocalTime.Now").DefaultFormatString("g");
            
            Bool("Enable ssl").Mandatory();
            
            Bool("Html").Mandatory();
            
            String("Sender address", 200);
            
            String("Sender name", 200);
            
            String("Subject", 200).Mandatory();
            
            String("To", 200);
            
            String("Attachments", 200);
            
            String("Bcc", 200);
            
            String("Cc", 200);
            
            Int("Retries").Mandatory();
            
            String("VCalendar view", 200);
            
            String("Username", 200);
            
            String("Password", 200);
            
            String("Smtp host", 200);
            
            Int("Smtp port");
            
            String("Category", 200);
        }
    }
}