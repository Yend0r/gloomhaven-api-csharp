using System;
using System.Collections.Generic;
using System.Linq;
using Bearded.Monads;
using Dapper;
using Npgsql;

namespace GloomChars.Common
{
    public static class DapperUtils
    {
        public static bool IsUniqueError(Exception ex)
        {
            return ex.Message.ToLower().Contains("unique constraint");
        }

        public static Option<T> SingleQuery<T>(string connString, string sql, object param)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                var queryResult = conn.Query<T>(sql, param);
                if (queryResult.Any())
                {
                    return queryResult.First();
                }

                return Option<T>.None;
            }
        }

        public static List<T> Query<T>(string connString, string sql, object param)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                //The 'ToList' will force the enumeration to resolve itself but we want the data, so no problemo.
                return conn.Query<T>(sql, param).ToList();
            }
        }

        public static (IEnumerable<T1>, IEnumerable<T2>) QueryMulti2<T1,T2>(string connString, string sql, object param)
        {
            using (var conn = new NpgsqlConnection(connString))
            using (var multi = conn.QueryMultiple(sql, param))
            {
                var result1 = multi.Read<T1>();
                var result2 = multi.Read<T2>();
                return (result1, result2);
            }
        }

        public static Either<int, string> TryExecuteScalar(string connString, string sql, object param, string error)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                try
                {
                    return conn.ExecuteScalar<int>(sql, param);
                }
                catch (Exception ex)
                {
                    if (IsUniqueError(ex))
                    {
                        return Either<int, string>.Create(error);
                    }
                    throw; //Handle other exceptions with the global handler
                }
            }
        }

        public static Either<int, string> TryExecute(string connString, string sql, object param, string error)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                try
                {
                    return conn.Execute(sql, param);
                }
                catch (Exception ex)
                {
                    if (IsUniqueError(ex))
                    {
                        return Either<int, string>.Create(error);
                    }
                    throw; //Handle other exceptions with the global handler
                }
            }
        }

        public static int Execute(string connString, string sql, object param)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                return conn.Execute(sql, param);
            }
        }
    }
}
