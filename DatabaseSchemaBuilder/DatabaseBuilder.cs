using System.Text;

namespace DatabaseSchemaBuilder
{
    public class DatabaseBuilder
    {
        private readonly StringBuilder _str;

        public DatabaseBuilder()
        {
            _str = new StringBuilder();
        }

        public DatabaseBuilder SetDatabaseName(string dbName)
        {
            _str.Append($"CREATE DATABASE {dbName};");

            return this;
        }

        public string Build()
        {
            return _str.ToString();
        }
    }
}