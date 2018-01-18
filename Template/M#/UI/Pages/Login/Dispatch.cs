using MSharp;
using Domain;

namespace Login
{
    public class DispatchPage : SubPage<LoginPage>
    {
        public DispatchPage()
        {
            OnStart(x =>
            {
                x.If(AppRole.Admin).Go<AdminPage>().RunServerSide();
                x.GentleMessage("TODO: Add redirect logic here and then delete this activity!");
            });
        }
    }
}