using MSharp;
using Domain;

namespace Modules
{
    public class ResetUserPassword : FormModule<Domain.User>
    {
        public ResetUserPassword()
        {
            SupportsAdd(false)
                .SupportsEdit()
                .HeaderText("Reset Your Password")
                .DataSource("info.Ticket.User")
                .SecurityChecks("Request.Has(\"Ticket\")");
            
            Field(x => x.Password).Mandatory().AfterControl("<div class='password-strength'></div>");
            CustomField().Label("Confirm new password")
                .Mandatory()
                .PropertyName("ConfirmPassword")
                .ExtraControlAttributes("type=\"password\"")
                .ViewModelAttributes("[System.ComponentModel.DataAnnotations.Compare(\"Password\",ErrorMessage=\"New password and Confirm password do not match. Please try again.\")]")
                .Control(ControlType.Textbox);
            Property<PasswordResetTicket>("Ticket").FromRequestParam("ticket");
            
            //================ Buttons: ================
            
            Button("Cancel").CausesValidation(false)
                .Action(x => x.Go<Root.LoginPage>());
            
            Button("Reset").IsDefault()
            .Action(x =>
            {
                x.If("info.Ticket.IsExpired || info.Ticket.IsUsed").MessageBox("This ticket is no longer valid. Please request a new ticket.").Exits();
                x.CSharp("await PasswordResetService.Complete(info.Ticket, info.Password.Trim());");
                x.Go<Login.ResetPassword.ConfirmPage>()
                    .Send("item", "info.Ticket.UserId");
            });
        }
    }
}