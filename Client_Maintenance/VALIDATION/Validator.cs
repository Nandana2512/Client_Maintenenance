using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client_Maintenance.VALIDATION
{
    public class Validator
    {
        public static bool IsValidClientNumber(string input, int size)
        {
            if (!Regex.IsMatch(input, @"^\d{" + size + "}$"))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidEmail(string email)
        {
            
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

           
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
           
            string phonePattern = @"^\(\d{3}\)\d{3}-\d{4}$";

            
            return Regex.IsMatch(phoneNumber, phonePattern);
        }
    }
}
