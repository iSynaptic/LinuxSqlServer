using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LinuxSqlServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = null;
            string query = null;

            if(args.Length != 2)
            {
                Console.WriteLine("Please enter or paste connection string: ");
                connectionString = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Please enter or paste query: ");
                query = Console.ReadLine();

                Console.WriteLine();
            }
            else
            {
                connectionString = args[0];
                query = args[1];
            }

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    var fields = new List<string>();
                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        fields.Add(reader.GetName(i));
                    }

                    while(reader.Read())
                    {
                        for(int i = 0; i < fields.Count; i++)
                        {
                            Console.WriteLine($"{fields[i]}: {reader.GetValue(i)}");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
