using MSharp;

namespace Domain
{
    public class Contact : EntityType
    {
        public Contact()
        {
            String("First name").Mandatory();
            String("Last name").Mandatory();
            String("Name").Calculated().Getter("FirstName + \" \" + LastName");
            String("Phone number").Mandatory();
        }
    }
}
