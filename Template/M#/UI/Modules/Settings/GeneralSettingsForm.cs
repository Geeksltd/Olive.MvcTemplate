using MSharp;
using Domain;

namespace Modules
{
    public class GeneralSettingsForm : FormModule<Domain.Settings>
    {
        public GeneralSettingsForm()
        {
            HeaderText("Settings").DataSource("Domain.Settings.Current");

            Field(x => x.PasswordResetTicketExpiryMinutes).AsNumericUpDown();

            Button("Save")
                .IsDefault()
                .OnClick(x =>
                {
                    x.SaveInDatabase();
                    x.GentleMessage("Updated");
                });
        }
    }
}