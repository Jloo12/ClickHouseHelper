using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using ClickHouseApi.Utilities;
using ClickHouseApi.Helpers;

namespace ClickHouseApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                // .SetBasePath(Directory.GetCurrentDirectory())
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "configs"))
                .AddJsonFile("appsettings.json")
                .Build();
            
            var connectionString = configuration.GetConnectionString("ClickHouseConnection");

            // 手動調整要測試的項目, event or item
            var tableName = "event_log"; //event_log, item_log
            var entityType = typeof(EventLogAbstract); //EventLogAbstract, ItemLogAbstract

            // 插入資料列的測試值
            var insertColumns = new List<string> { "event_id", "user_id", "event_type", "action_time" };
            var insertValues = new List<object> { Guid.NewGuid(), "user123", "click", DateTime.Now };
           
            // 刪除資料列的測試值
            var deleteConditions = new Dictionary<string, object>
            {
                { "user_id", "user123" },
                { "event_type", "click" }
            };

            // Create Table
            CreateTableTest(connectionString, tableName, entityType);

            // // Insert Table
            // ClickHouseHelper.InsertTable(connectionString,tableName, insertColumns, insertValues);

            // // Read Table
            // ClickHouseHelper.ReadTable(connectionString, tableName);

            // // Delete Row
            // ClickHouseHelper.DeleteRow(connectionString, tableName, deleteConditions);

            // //Delete Table
            // ClickHouseHelper.DeleteTable(connectionString, tableName);
        }
        
        static void CreateTableTest(string connectionString, string tableName, Type entityType)
        {
            string sql = ClickHouseHelper.GenerateSql(entityType, tableName);
            Console.WriteLine(sql);
            ClickHouseHelper.CreateTable(connectionString, sql);
        }

    }
}