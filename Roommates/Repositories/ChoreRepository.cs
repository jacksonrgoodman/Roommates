using Microsoft.Data.SqlClient;
using Roommates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Repositories
{
    class ChoreRepository : BaseRepository
    {
        //? 1. instantiate ChoreRepository and connect it to the database, thru connectionString
        public ChoreRepository(string connectionString) : base(connectionString) { }
        //? 2. List all chores in the database.
        public List<Chore> GetAll()
        {
            //TODO: use database connection
            //! MUST: open and close the connection when in use & not
            //? in C# using block is a tool to properly close
            using (SqlConnection conn = base.Connection)
            {
                //* open connection
                conn.Open();

                //Sql command, which we again "use"
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // sql command to get id and chores
                    cmd.CommandText = "SELECT Id, Name FROM Chore";

                    // execute the sql in the database, get "reader" of data
                    SqlDataReader reader = cmd.ExecuteReader();

                    // list to hold chores from database
                    List<Chore> chores = new List<Chore>;

                    // time to read()
                    while (reader.Read())
                    {
                        // Positioning the data for Id, "ORDINAL"
                        int idColumnPosition = reader.GetOrdinal("Id");
                        // Get the value for particular ordinal
                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");

                        string nameValue = reader.GetString(nameColumnPosition);

                        // new Chore object from database 
                        Chore chore = new Chore
                        {
                            Id = idValue,
                            Name = nameValue,
                        };
                        // add that chore object to our list
                        chores.Add(chore);
                    }
                    //we have to close the reader when we are done.
                    reader.Close();
                    // then we return the list when the method is called
                    return chores;
                }
            }
        }
        //? 3. Return single room by id
        public Chore GetByID(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Chore WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id",id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Chore> chores = new List<Chore>();

                    while (reader.Read())
                    {
                        // Positioning the data for Id, "ORDINAL"
                        int idColumnPosition = reader.GetOrdinal("Id");
                        // Get the value for particular ordinal
                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");

                        string nameValue = reader.GetString(nameColumnPosition);

                        Chore chore = new Chore
                        {
                            Id = idValue,
                            Name = nameValue
                        };
                        chores.Add(chore);
                    }
                    reader.Close();
                    return chores;
                }
            }
        }

    }
}
