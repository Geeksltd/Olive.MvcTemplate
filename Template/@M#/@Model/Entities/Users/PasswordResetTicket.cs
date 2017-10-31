using MSharp;

namespace Model
{
    public class PasswordResetTicket : EntityType
    {
        public PasswordResetTicket()
        {
            DateTime("Date created").Mandatory().Default("c#:LocalTime.Now");
            Bool("Is expired").Mandatory().Calculated()
                .Getter("LocalTime.Now >= DateCreated.AddMinutes(Settings.Current.PasswordResetTicketExpiryMinutes)");
            Bool("Is used").Mandatory();
            Associate<User>("User").Mandatory().DatabaseIndex().OnDelete(CascadeAction.CascadeDelete);
        }
    }
}