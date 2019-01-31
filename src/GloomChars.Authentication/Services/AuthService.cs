using System;
using System.Linq;
using Bearded.Monads;
using GloomChars.Authentication.Interfaces;
using GloomChars.Authentication.Models;
using GloomChars.Common.Config;
using Microsoft.Extensions.Options;

namespace GloomChars.Authentication.Services
{
    public class AuthService : IAuthService
    {
        readonly IAuthRepository _authRepo;
        readonly AuthConfig _authConfig;
        readonly string _invalidCredentialsMsg = "Invalid email/password.";
        readonly string _invalidTokenMsg = "Invalid access token.";
        readonly string _invalidPasswordMsg = "Invalid password.";
        readonly string _pwdUpdateFailedMsg = "Password update failed";

        public AuthService(IOptions<AuthConfig> authConfig, IAuthRepository authRepo)
        {
            _authConfig = authConfig.Value;
            _authRepo = authRepo;
        }

        public Either<AuthenticatedUser, string> Authenticate(string email, string password)
        {
            return
                from preAuthUser  in GetUserForAuth(email)
                from pwdCheck     in CheckPassword(password, preAuthUser)
                from lockoutCheck in CheckIfLockedOut(preAuthUser)
                from newLogin     in CreateNewLogin(preAuthUser)
                from authUser     in GetAuthenticatedUser(newLogin.AccessToken)
                select authUser;
        }

        public Either<AuthenticatedUser, string> GetAuthenticatedUser(string accessToken)
        {
            return _authRepo.GetAuthenticatedUser(accessToken).AsEither("Invalid access token.");
        }

        public int RevokeToken(string accessToken)
        {
            return _authRepo.RevokeToken(accessToken);
        }

        public Either<int, string> ChangePassword(PasswordUpdate passwordUpdate)
        {
            return
                from preAuthUser in GetUserByAccessToken(passwordUpdate.AccessToken)
                from oldPwdCheck in CheckOldPassword(passwordUpdate.OldPassword, preAuthUser)
                from newHashedPassword in PasswordHasher.HashPassword(preAuthUser.Email, passwordUpdate.NewPassword)
                from updateResult in UpdatePassword(preAuthUser.Id, newHashedPassword)
                select updateResult;
        }

        internal void ClearLoginAttempts(int userId)
        {
            var status = new LoginStatusUpdate
            {
                UserId        = userId,
                AttemptNumber = 0,
                IsLockedOut   = false,
                DateLockedOut = null
            };
            _authRepo.UpdateLoginStatus(status);
        }

        internal void LogFailedAttempt(PreAuthUser user)
        {
            if (!_authConfig.UseLockout)
            {
                ClearLoginAttempts(user.Id);
            }
            else
            {
                var attemptNumber = user.LoginAttemptNumber + 1;
                var isLockedOut = (attemptNumber > _authConfig.LoginAttemptsBeforeLockout);
                //Use Utc time to avoid problems with ppl accessing in different timezones
                DateTime? dateLockedOut = null;
                if (isLockedOut)
                {
                    dateLockedOut = DateTime.UtcNow;
                }

                var status = new LoginStatusUpdate
                {
                    UserId        = user.Id,
                    AttemptNumber = attemptNumber,
                    IsLockedOut   = isLockedOut,
                    DateLockedOut = dateLockedOut
                };

                //Update details with the login attempt
                _authRepo.UpdateLoginStatus(status);
            }
        }

        internal bool IsLockoutStillValid(DateTime? dateLockedOut)
        {
            if (dateLockedOut == null)
            {
                //Either db has a problem or method was called incorrectly... 
                return false;
            }

            return dateLockedOut.Value.AddMinutes(_authConfig.LockoutDurationInMins) <= DateTime.UtcNow;
        }

        internal Either<PreAuthUser, string> CheckIfLockedOut(PreAuthUser user)
        {
            if (!_authConfig.UseLockout)
            {
                ClearLoginAttempts(user.Id);
                return user;
            }

            if (user.IsLockedOut && IsLockoutStillValid(user.DateLockedOut))
            {
                return $"Account is locked out. Please wait {_authConfig.LockoutDurationInMins} mins or contact an administrator."; 
            }

            return user;
        }

        internal Either<PreAuthUser, string> CheckPassword(string password, PreAuthUser user)
        {
            //If we got a user then always do the password check to hamper time based attacks
            var passwordVerified = PasswordHasher.VerifyHashedPassword(user.Email, user.PasswordHash, password);

            if (passwordVerified)
            {
                return user;
            }

            //Increment the number of login attempts... may result in user being locked out 
            LogFailedAttempt(user);
            return _invalidCredentialsMsg;
        }

        internal Either<PreAuthUser, string> CheckOldPassword(string oldPassword, PreAuthUser user)
        {
            var passwordVerified = PasswordHasher.VerifyHashedPassword(user.Email, user.PasswordHash, oldPassword);

            if (passwordVerified)
            {
                return user;
            }

            return _invalidPasswordMsg;
        }

        internal Either<PreAuthUser, string> GetUserForAuth(string email)
        {
            var user = _authRepo.GetUserForAuth(email);
            if (!user.IsSome)
            {
                //No user found... do a fake password check (to hamper time based attacks). 
                PasswordHasher.HashFakePassword();
            }

            return user.AsEither(_invalidCredentialsMsg);
        }

        internal Either<PreAuthUser, string> GetUserByAccessToken(string accessToken)
        {
            return _authRepo.GetUserByToken(accessToken)
                            .AsEither(_invalidTokenMsg);
        }

        internal Either<int, string> UpdatePassword(int userId, string passwordHash)
        {
            if (_authRepo.UpdatePassword(userId, passwordHash) != 1)
            {
                return _pwdUpdateFailedMsg;
            }
            return 1;         
        }

        internal Either<NewLogin, string> CreateNewLogin(PreAuthUser user)
        {
            var newLogin = new NewLogin
            {
                UserId      = user.Id,
                AccessToken = Guid.NewGuid().ToString(),
                DateExpires = DateTime.UtcNow.AddMinutes(_authConfig.LockoutDurationInMins),
                DateCreated = DateTime.UtcNow
            };

            return _authRepo.InsertNewLogin(newLogin);
        }
    }
}
