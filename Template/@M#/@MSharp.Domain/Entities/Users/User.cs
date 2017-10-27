using MSharp;

namespace Model
{
    public class User : EntityType
    {
        public User()
        {
            Abstract();
            
            String("First name", 200).Mandatory();
            
            String("Last name", 200).Mandatory();
            
            String("Name", 200).Calculated().Getter("FirstName + \" \" + LastName");
            
            String("Email", 100).Mandatory().TrimValues(false).Unique().Accepts(TextPattern.EmailAddress);
            
            String("Password", 100).HashPassword().SaltProperty("Salt").Accepts(TextPattern.Password);
            
            String("Salt", 200);
            
            Bool("Is deactivated").Mandatory();
        }
    }
}