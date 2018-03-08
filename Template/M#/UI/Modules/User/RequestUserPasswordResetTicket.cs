using MSharp;
using Domain;

namespace Modules
{
    public class RequestUserPasswordResetTicket : FormModule<Domain.User>
    {
        public RequestUserPasswordResetTicket()
        {
            SupportsAdd(false).SupportsEdit(false)
                .Header("<p> Please provide your email address. You will receive an email with instructions on how to reset your password. </p>")
                .HeaderText("Forgot Your Password?");

            Field(x => x.Email);
            Button("Send").IsDefault()
            .OnClick(x =>
            {
                x.CSharp("var user = await Domain.User.FindByEmail(info.Email.Trim());");

                x.If("user == null")
                .MessageBox("Invalid email address. Please try again.")
                .AndExit();

                x.CSharp("await PasswordResetService.RequestTicket(user);");
                x.Display(@"<h2> Forgot Your Password? </h2>
                <p> An email containing instructions to change your password has been sent to your email address. </p>").IsHtml();
            });
        }
    }
}