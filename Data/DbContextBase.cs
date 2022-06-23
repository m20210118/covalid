// using Microsoft.EntityFrameworkCore;
// using Oracle.EntityFrameworkCore.Infrastructure.Internal;
// using Oracle.ManagedDataAccess.Client;
// using Oracle.ManagedDataAccess.Types;
// using System;

// namespace Covalid.Data
// {
//     public class DbContextBase : DbContext
//     {
//         private string _connectionString = "";

//         // public DbContextBase(DbContextOptions options) : base(options)
//         // {
//         //     _connectionString = options.FindExtension<OracleOptionsExtension>().ConnectionString;
//         // }

//         // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         // {
//         //     if (!optionsBuilder.IsConfigured)
//         //     {
//         //         optionsBuilder.UseOracle(_connectionString);
//         //     }
//         // }

//         // protected override void OnModelCreating(ModelBuilder builder)
//         // {
//         //     base.OnModelCreating(builder);
//         // }

//         public T CallFunction<T>(string func) where T : new()
//         {
//             T result = new T();
//             using (OracleConnection conn = new OracleConnection(_connectionString))
//             {
//                 using (OracleCommand cmd = conn.CreateCommand())
//                 {
//                     conn.Open();
//                     cmd.BindByName = true;
//                     cmd.CommandText = string.Format("select {0} from dual", func);
//                     OracleDataReader reader = cmd.ExecuteReader();
//                     while (reader.Read())
//                     {
//                         result = (T)reader.GetOracleValue(0);
//                     }
//                     reader.Close();

//                     conn.Close();
//                 }

//             }
//             return result;
//         }

//         public long GetSeq(string seqName)
//         {
//             return Convert.ToInt64((decimal)CallFunction<OracleDecimal>(seqName + ".nextVal"));
//         }
//     }
// }
