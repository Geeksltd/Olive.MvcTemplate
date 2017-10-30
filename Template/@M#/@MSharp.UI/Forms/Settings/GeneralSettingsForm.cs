using MSharp;
using Domain;

namespace Modules
{
    public class GeneralSettingsForm : FormModule<Domain.Settings>
    {
        public GeneralSettingsForm()
        {
            HeaderText("Settings").DataSource("Domain.Settings.Current");
            
            Field(x => x.PasswordResetTicketExpiryMinutes).Control(ControlType.NumericUpDown);
            Button("Save").IsDefault()
            .Action(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Updated");
            });
        }
    }
}