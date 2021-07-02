using System;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace DatabaseSchemaBuilder
{
    class Program
    {
        static void Main(string[] args)
        {

            using SqlConnection sqlConnection = new SqlConnection();


            sqlConnection.ConnectionString = Properties.Resources.ResourceManager.GetString("ConnectionString");



            sqlConnection.Open();

            Console.WriteLine($"Connection is {sqlConnection.State}.");

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

            while (true)
            {
                DatabaseBuilder databaseBuilder = new DatabaseBuilder();

                Console.Write("Enter database name: ");

                var dbName = Console.ReadLine();


                sqlCommand.CommandText = databaseBuilder.SetDatabaseName(dbName).Build();

                sqlCommand.ExecuteNonQuery();

                Console.WriteLine();
                Console.WriteLine("Database created");
                Console.WriteLine();
                // todo: create db


                while (true)
                {
                    var query = $"USE {dbName}; ";

                    TableBuilder tableBuilder = new TableBuilder();

                    bool identityStatus = false;

                    Console.Write("Enter table name: ");

                    var tableName = Console.ReadLine();

                    tableBuilder.SetTableName(tableName);

                    // todo: create table


                    while (true)
                    {
                        ColumnBuilder columnBuilder = new ColumnBuilder();


                        Console.Write("Enter column name: ");

                        var columnName = Console.ReadLine();

                        Console.Write("Choice data type (1. Int, 2. Varchar, 3. DateTime) : ");

                        var typeChoice = Convert.ToInt32(Console.ReadLine());

                        int varcharSize = 0;

                        if ((DataType) typeChoice == DataType.Varchar)
                        {
                            Console.Write("Enter varchar size: ");

                            varcharSize = Convert.ToInt32(Console.ReadLine());
                        }
                        // todo: create column

                        Console.Write("Do you want to set Not Null? y/n: ");

                        bool isNotNull = Console.ReadLine()[0] == 'y';

                        bool isIdentity = false;

                        if ((DataType)typeChoice == DataType.Int && !identityStatus)
                        {
                            Console.Write("Do you want to activate identity? y/n : ");

                            isIdentity = Console.ReadLine()[0] == 'y';

                            identityStatus = isIdentity;

                            // todo: set identity
                        }

                        Console.Write("Do you want to set Primary Key? y/n: ");

                        bool isPrimaryKey = Console.ReadLine()[0] == 'y';



                        columnBuilder.SetColumnName(columnName)
                            .SetColumnType((DataType)typeChoice, varcharSize);

                        if (isNotNull)
                            columnBuilder.SetNotNull();

                        if (isIdentity)
                            columnBuilder.SetIdentity();

                        if (isPrimaryKey)
                            columnBuilder.SetPrimaryKey();

                        string column = columnBuilder.Build();
                        tableBuilder.AddColumn(column);

                        Console.WriteLine();
                        Console.WriteLine("Column added");
                        Console.WriteLine();
                        Console.Write("Do you want to add more column? y/n: ");
                        if (Console.ReadLine()[0] == 'n')
                            break;
                    }


                    sqlCommand.CommandText = query + tableBuilder.Build();
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine();
                    Console.WriteLine("Table Created");
                    Console.WriteLine();

                    Console.Write("Do you want to create more table? y/n: ");


                    if (Console.ReadLine()[0] == 'n')
                        break;
                }

                Console.Write("Do you want to create more database? y/n: ");

                if (Console.ReadLine()[0] == 'n')
                    break;
            }
            

            sqlConnection.Close();

            Console.WriteLine($"Connection is {sqlConnection.State}.");
        }
    }
}
