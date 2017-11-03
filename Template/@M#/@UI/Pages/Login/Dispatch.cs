using MSharp;
using Domain;

namespace Login
{
    public class DispatchPage : SubPage<Root.LoginPage>
    {
        public DispatchPage()
        {
            StartUp(x =>
            {
                x.If(Role.Administrator).Go<Root.AdminPage>().RunServerSide();
                x.GentleMessage("TODO: Add redirect logic here and then delete this activity!");
            });
        }
    }
}