using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace MyFTP_Server.Classes
{
    class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userDirectory { get; set; }


        public bool loginUser(string userName, string userPassword, ref string message)
        {
            if (!File.Exists(@"sqlite.db"))
            {
                message = "O banco de dados não existe.";
                return false;
            }

            if (!this.checkUserByUsername(userName, ref message))
            {
                message = "Este usuário não existe.";
                return false;
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source=sqlite.db");

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SQLiteCommand userQuery = new SQLiteCommand("SELECT Users_ID, Users_Directory FROM Users WHERE Users_Name = @username AND Users_Password = @userpassword;", connection);
            userQuery.Parameters.AddWithValue("username", userName);
            userQuery.Parameters.AddWithValue("userpassword", userPassword);

            SQLiteDataReader queryRows = userQuery.ExecuteReader();

            if (queryRows.HasRows)
            {
                queryRows.Read();

                this.userId = Convert.ToInt32(queryRows["Users_ID"]);
                this.userName = userName;
                this.userDirectory = queryRows["Users_Directory"].ToString();

                queryRows.Close();
                connection.Close();
                message = "Login feito com sucesso.";
                return true;
            }

            queryRows.Close();
            connection.Close();
            message = "Senha incorreta.";
            return false;
        }

        public bool addUser(string userName, string userPassword, ref string message)
        {
            string returnUserName = String.Empty;
            if (this.checkUserByUsername(userName, ref returnUserName))
            {
                message = "Este usuário já está cadastrado.";
                return false;
            }

            if (!File.Exists(@"sqlite.db"))
            {
                message = "O banco de dados não existe.";
                return false;
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source=sqlite.db");

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SQLiteCommand userQuery = new SQLiteCommand("INSERT INTO Users (Users_Name, Users_Password, Users_Directory) VALUES (@username, @userpassword, @userdirectory);", connection);
            userQuery.Parameters.AddWithValue("username", userName);
            userQuery.Parameters.AddWithValue("userpassword", userPassword);
            userQuery.Parameters.AddWithValue("userdirectory", userName);

            try
            {
                if (userQuery.ExecuteNonQuery() > 0)
                {
                    message = "Usuário adicionado com sucesso.";
                    connection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
            }

            message = "Não foi possível adicionar o usuário.";
            connection.Close();
            return false;
        }

        public bool removeUser(string userName, string userPassword, ref string message)
        {
            if (!File.Exists(@"sqlite.db"))
            {
                message = "O banco de dados não existe.";
                return false;
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source=sqlite.db");

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SQLiteCommand userQuery = new SQLiteCommand("DELETE FROM Users WHERE Users_Name = @username AND Users_Password = @userpassword;", connection);
            userQuery.Parameters.AddWithValue("username", userName);
            userQuery.Parameters.AddWithValue("userpassword", userPassword);

            try
            {
                if (userQuery.ExecuteNonQuery() > 0)
                {
                    message = "Usuário apagado com sucesso.";
                    connection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            message = "Não foi possível apagar o usuário.";
            connection.Close();
            return false;
        }

        public bool changePw(string userName, string userPassword, string userNewPassword, ref string message)
        {
            if (!File.Exists(@"sqlite.db"))
            {
                message = "O banco de dados não existe.";
                return false;
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source=sqlite.db");

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SQLiteCommand userQuery = new SQLiteCommand("UPDATE Users SET Users_Password = @usernewpassword WHERE Users_Name = @username AND Users_Password = @userpassword;", connection);

            userQuery.Parameters.AddWithValue("usernewpassword", userNewPassword);
            userQuery.Parameters.AddWithValue("username", userName);
            userQuery.Parameters.AddWithValue("userpassword", userPassword);

            try
            {
                if (userQuery.ExecuteNonQuery() > 0)
                {
                    message = "Senha alterada com sucesso.";
                    connection.Close();
                    return true;
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            message = "Não foi possível alterar a senha";
            connection.Close();
            return false;
        }

        public bool checkUserByUsername(string userName, ref string message)
        {
            if (!File.Exists(@"sqlite.db"))
            {
                message = "O banco de dados não existe.";
                return false;
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source=sqlite.db");

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SQLiteCommand userQuery = new SQLiteCommand("SELECT Users_ID FROM Users WHERE Users_Name = @username;", connection);
            userQuery.Parameters.AddWithValue("username", userName);

            SQLiteDataReader queryRows = userQuery.ExecuteReader();

            if (queryRows.HasRows)
            {
                queryRows.Close();
                connection.Close();
                return true;
            }

            queryRows.Close();
            connection.Close();
            return false;
        }
    }
}
