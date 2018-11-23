using System;
using System.Text;
using Bearded.Monads;
using GloomChars.Authentication.Models;

namespace GloomChars.Authentication.Services
{
    //Letting password hashing exceptions go to the global error handler 

    public class PasswordHasher
    {
        public static Either<string, AuthenticationError> HashPassword(string email, string password)
        {
            var userForHasher = new PasswordHashUser(email);
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<PasswordHashUser>();
            return hasher.HashPassword(userForHasher, password);
        }

        public static bool VerifyHashedPassword(string email, string hashedPassword, string plainPassword)
        {
            var userForHasher = new PasswordHashUser(email);
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<PasswordHashUser>();
            var result = hasher.VerifyHashedPassword(userForHasher, hashedPassword, plainPassword);

            switch (result)
            {
                case Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success:
                    return true;
                default:
                    return false; //Ignoring the case of "Success - rehash required" for now
            }
        }

        public static void HashFakePassword()
        {
            var fakePwdGuid = Guid.NewGuid().ToString();
            var fakePwdBytes = Encoding.UTF8.GetBytes(fakePwdGuid);
            var fakePwd = Convert.ToBase64String(fakePwdBytes);
            PasswordHasher.VerifyHashedPassword("", fakePwd, "");
        }
    }
}
