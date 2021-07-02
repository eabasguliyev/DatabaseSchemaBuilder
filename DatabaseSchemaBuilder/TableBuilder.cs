using System.Text;

namespace DatabaseSchemaBuilder
{
    public class TableBuilder
    {
        private readonly StringBuilder _str;

        private bool _firstColumn;

        public TableBuilder()
        {
            _str = new StringBuilder();

            _firstColumn = true;
        }

        public TableBuilder SetTableName(string tableName)
        {
            _str.Append($"CREATE TABLE {tableName}");

            return this;
        }

        public TableBuilder AddColumn(string column)
        {
            _str.Append(_firstColumn ? " (" : ", ");

            _str.Append(column);

            _firstColumn = false;
            return this;
        }
        
        public string Build()
        {
            _str.Append(");");
            return _str.ToString();
        }
    }
}