using MSharp;
using Domain;

namespace Admin.Settings.ContentBlocks
{
    public class EnterPage : SubPage<ContentBlocksPage>
    {
        public EnterPage()
        {
            Add<Modules.ContentBlockForm>();
        }
    }
}