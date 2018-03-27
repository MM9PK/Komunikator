using System;
using System.Data;
using Npgsql;

namespace KomunikatorServerTest
{

    class DataBaseAccessor
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private NpgsqlConnection conn;
        private NpgsqlCommand executeQuery(String query)
        {
            try
            {
                // Data adapter making request from our connection   
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                return command;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
                throw;
            }
        }
        public int connectToDataBase(String hostName, int port, String userId,
            String password, String dataBaseName)
        {
            // PostgeSQL-style connection string
            String connectString = String.Format("Server = {0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    hostName, port, userId,
                    password, dataBaseName);
            try
            {
                // Making connection with Npgsql provider
                conn = new NpgsqlConnection(connectString);
                conn.Open();
                return 0;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.ToString());
                throw;
            }
        }
        public int register(String name, String surname, int albumNo, String email, String password)
        {
            String query = String.Format("SELECT FROM users WHERE email='{0}'", email);

            NpgsqlCommand da = executeQuery(query);

            NpgsqlDataReader checkIfUserExists = da.ExecuteReader();
            if (checkIfUserExists.HasRows)
            {
                Console.WriteLine("User exists");
                return 1;

                // TODO Send info to WEB service and pass to user
            }
            else
            {
                query = String.Format("INSERT INTO users VALUES ('{0}', '{1}', {2}, '{3}', '{4}');",
                    name, surname, albumNo, email, password);
                executeQuery(query).ExecuteNonQuery();
                return 0;
            }
        }
        public int login(String email, String password)
        {
            String query = String.Format("SELECT password FROM users WHERE email='{0}'", email);

            NpgsqlCommand da = executeQuery(query);

            NpgsqlDataReader dr = da.ExecuteReader();
            // Checking if user exists
            if (dr.HasRows)
            {
                NpgsqlDataReader checkIfPasswordIsCorrect = dr;
                while (checkIfPasswordIsCorrect.Read())
                {
                    if (checkIfPasswordIsCorrect[0].Equals(password))
                    {
                        Console.WriteLine("Access granted.");

                        // TODO Grand access for real
                    }
                    else
                    {
                        Console.WriteLine("Wrong password");

                        // TODO Send info to WEB service and pass to user
                    }
                }
                return 0;
            }
            else
            {
                Console.WriteLine("User does not exist");
                return 1;
            }
        }
        public int selectUserById(int id)
        {
            String query = String.Format("SELECT * FROM users WHERE id={0}", id);
            NpgsqlCommand da = executeQuery(query);

            NpgsqlDataReader dr = da.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine("{0} \t {1} \t {2} \t {3}", dr[0], dr[1], dr[2], dr[3]);
            }
            return 0;
        }

        public int getOrCreateConversation(int[] usersId)
        {
            String query = String.Format("SELECT * FROM conversations WHERE users_id='{0}'",);
            return 0;
        }


    }
}
