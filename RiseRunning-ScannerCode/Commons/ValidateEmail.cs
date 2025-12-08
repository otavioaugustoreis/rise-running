using System.Net.Mail;

namespace RiseRunning_ScannerCode.Commons
{
    public static class ValidateEmail
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

