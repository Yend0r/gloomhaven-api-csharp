using System;
using Bearded.Monads;
using GloomChars.Authentication.Interfaces;
using GloomChars.Authentication.Models;
using GloomChars.Common;
using GloomChars.Common.Config;
using Microsoft.Extensions.Options;

namespace GloomChars.Authentication.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        readonly string _connStr;

        public AuthRepository(IOptions<DatabaseConfig> dbConfig)
        {
            _connStr = dbConfig.Value.ConnectionString;
        }

        public Option<AuthenticatedUser> GetAuthenticatedUser(string accessToken)
        {
            string sql = @"
                SELECT users.id           AS Id,
                    users.email           AS Email, 
                    tokens.access_token   AS AccessToken, 
                    tokens.date_expires   AS AccessTokenExpiresAt,
                    users.is_system_admin AS IsSystemAdmin
                FROM users 
                    INNER JOIN auth_tokens AS tokens ON users.id = tokens.user_id
                WHERE tokens.access_token = @AccessToken
                    AND tokens.is_revoked = false
                ";

            return DapperUtils.SingleQuery<AuthenticatedUser>(_connStr, sql, new { AccessToken = accessToken });
        }

        public Option<PreAuthUser> GetUserForAuth(string email)
        {
            string sql = @"
                SELECT id                AS Id,
                    email                AS Email, 
                    password_hash        AS PasswordHash, 
                    is_locked_out        AS IsLockedOut, 
                    login_attempt_number AS LoginAttemptNumber, 
                    date_created         AS DateCreated, 
                    date_updated         AS DateUpdated,
                    date_locked_out      AS DateLockedOut
                FROM users
                WHERE email = @Email
                ";

            return DapperUtils.SingleQuery<PreAuthUser>(_connStr, sql, new { Email = email });
        }

        public Either<NewLogin, string> InsertNewLogin(NewLogin newLogin)
        {
            string sql = @"
                INSERT INTO auth_tokens
                    (user_id, 
                    access_token, 
                    is_revoked,  
                    date_created, 
                    date_expires)
                VALUES 
                    (@UserId, 
                    @AccessToken, 
                    false, 
                    @DateCreated, 
                    @DateExpires)
                RETURNING id
                ";

            var result = DapperUtils.TryExecuteScalar(_connStr, sql, newLogin, "Access token already exists.");
            return result.Map(_ => newLogin);
        }

        public int RevokeToken(string accessToken)
        {
            string sql = @"
                UPDATE auth_tokens
                SET is_revoked     = true,
                    date_revoked   = current_timestamp
                WHERE access_token = @AccessToken
                ";

            return DapperUtils.Execute(_connStr, sql, new { AccessToken = accessToken });
        }

        public void UpdateLoginStatus(LoginStatusUpdate statusUpdate)
        {
            string sql = @"
                UPDATE users 
                SET is_locked_out        = @IsLockedOut,
                    login_attempt_number = @AttemptNumber,
                    date_locked_out      = @DateLockedOut
                WHERE id                 = @UserId
                ";

            DapperUtils.Execute(_connStr, sql, statusUpdate);
        }
    }
}
