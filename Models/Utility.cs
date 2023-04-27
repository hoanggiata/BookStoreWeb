using System.Text;
using System.Text;
using System.Security.Cryptography;

namespace BookStoreWeb.Models
{
    public class Utility
    {
        public Utility() { }
        public void CreateUser(Account account)
        {
            BookstoreContext db = new BookstoreContext();

            if (account.AccountId == null)
                account.AccountId = Guid.NewGuid().ToString();
            Utility utility = new Utility();
            account.Password = HashingPassword(account.Password); 
            db.Accounts.Add(account);
            db.SaveChanges();
        }
        public static string HashingPassword(string password)
        {
            //Hashing Password
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
            //
        }
    }
}
