using System;
using ClickHouse.Client.ADO;
using System.Text;
using System.Text.Json;
using System.Reflection;

namespace ClickHouseApi.Helpers
{
    public class ClickHouseHelper
    {   
        public static string GetSetting(string connectionString, string settingName)
    {
        using (var connection = new ClickHouseConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SHOW SETTINGS LIKE '{settingName}';";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var name = reader["name"].ToString();
                            var value = reader["value"].ToString();
                            return $"Setting: {name}, Value: {value}";
                        }
                        else
                        {
                            return $"Setting {settingName} not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
    }
        public static void SetAllowExpr(string connectionString)
        {
            using (var connection = new ClickHouseConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SET allow_experimental_object_type = 1;";
                        command.ExecuteNonQuery();
                        Console.WriteLine("Experimental object type enabled.");
                    }
                    string settingName = "allow_experimental_object_type";
        
                    string result = ClickHouseHelper.GetSetting(connectionString, settingName);
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while enabling experimental object type: {ex.Message}");
                }
            }
        }
         public static string GenerateSql(Type type, string tableName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE IF NOT EXISTS {tableName} (");

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                string columnName = property.Name;
                string columnType = GetSqlType(property.PropertyType);
                sb.AppendLine($"    {columnName} {columnType},");
            }
        
            // Remove the last comma
            sb.Length--; // Remove last comma
            sb.AppendLine(")ENGINE = MergeTree() ORDER BY (event_id, action_time);");

            return sb.ToString();
        }

        private static string GetSqlType(Type type)
        {
            if (type == typeof(int) || type == typeof(uint))
                return "INT";
            if (type == typeof(float) || type == typeof(double))
                return "FLOAT";
            if (type == typeof(string))
                return "String";
            if (type == typeof(DateTime))
                return "DATETIME";
            if (type == typeof(Guid))
                return "UUID";
            if (type == typeof(JsonDocument))
                return "JSON";
            // Add other type mappings as needed

            throw new NotSupportedException($"Type {type} is not supported");
        }

        public static void CreateTable(string connectionString, string createTableSql)
        {
            using (var connection = new ClickHouseConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SetAllowExpr(connectionString); 
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = createTableSql;
                        var affectedRows = command.ExecuteNonQuery();
                        Console.WriteLine($"SQL executed. Affected rows: {affectedRows}");
                        Console.WriteLine("Create Table Succeed!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Create Table Failed!");
                }
            }
        }

        public static void InsertTable(string connectionString, string tableName, IEnumerable<string> columns, IEnumerable<object> values)
        {
            using (var connection = new ClickHouseConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string columnNames = string.Join(", ", columns);
                    string valuePlaceholders = string.Join(", ", values.Select(v => FormatValue(v)));
                    
                    var insertCommand = $"INSERT INTO {tableName} ({columnNames}) VALUES ({valuePlaceholders})";
                    
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = insertCommand;

                        var rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");
                        Console.WriteLine("Insert Data Succeed!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Insert Data Failed!");
                }
            }
        }

        public static void ReadTable(string connectionString, string tableName)
        {
            using (var connection = new ClickHouseConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection opened successfully.");

                    var selectCommand = $"SELECT * FROM {tableName}";

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = selectCommand;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write(reader.GetValue(i) + "\t");
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        public static void DeleteRow(string connectionString, string tableName, Dictionary<string, object> conditions)
        {
            using (var connection = new ClickHouseConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var whereClause = new StringBuilder("WHERE ");
                    foreach (var condition in conditions)
                    {
                        string columnName = condition.Key;
                        string formattedValue = FormatValue(condition.Value);

                        whereClause.Append($"{columnName} = {formattedValue} AND ");
                    }
                    
                    // 移除最后的 "AND " 
                    if (conditions.Count > 0)
                    {
                        whereClause.Length -= 5; 
                    }
                    else
                    {
                        // 如果没有条件，删除操作是无效的
                        throw new ArgumentException("At least one condition is required.");
                    }

                    // 构建完整的 DELETE 语句
                    var deleteCommand = $"DELETE FROM {tableName} {whereClause.ToString()}";

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = deleteCommand;
                        var rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");
                        Console.WriteLine("Delete Row Succeed!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Delete Row Failed!");
                }
            }
        }

        public static void DeleteTable(string connectionString,string tableName)
        {
            using (var connection = new ClickHouseConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var deleteCommand = $"DROP TABLE {tableName}";

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = deleteCommand;
                        var rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");
                        Console.WriteLine("Delete Table Succeed!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Delete Table Failed!");
                }

            }
        }


        private static string FormatValue(object value)
        {
            if (value is string || value is Guid)
            {
                return $"'{value}'";
            }
            if (value is DateTime dateTime)
            {
                // Convert DateTime to ClickHouse compatible format
                return $"'{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}'";
            }
            if (value is bool boolean)
            {
                return boolean ? "1" : "0";
            }
            if (value is null)
            {
                return "NULL";
            }
            return value.ToString();
        }
        
    }
}
