using MSharp;

namespace Domain
{
    public class EmailQueueItem : EntityType
    {
        public EmailQueueItem()
        {
            Cachable(false).SoftDelete().Implements("Olive.Services.Email.IEmailQueueItem");

            BigString("Body").Lines(5);
            DateTime("Date").Mandatory().Default("c#:LocalTime.Now").DefaultFormatString("g");
            Bool("Enable ssl").Mandatory();
            Bool("Html").Mandatory();
            String("Sender address");
            String("Sender name");
            String("Subject").Mandatory();
            String("To");
            String("Attachments");
            String("Bcc");
            String("Cc");
            Int("Retries").Mandatory();
            String("VCalendar view");
            String("Username");
            String("Password");
            String("Smtp host");
            Int("Smtp port");
            String("Category");
        }
    }
}