using System;
using System.Collections.Generic;

namespace ClickHouseApi.Utilities
{
    public static class DataGeneratorUtility
    {
        // 生成随机 ID
        public static int GenerateId()
        {
            var random = new Random();
            return random.Next(1000, 9999);
        }

        // 生成随机 Name
        public static string GenerateName(int index)
        {
            return "test" + index;
        }

        // 生成 Session ID
        public static string GenerateSessionId()
        {
            return Guid.NewGuid().ToString();
        }

        // 生成数据条目
        public static List<DataEntry> GenerateData(int numberOfRecords)
        {
            var data = new List<DataEntry>();

            for (int i = 0; i < numberOfRecords; i++)
            {
                var entry = new DataEntry
                {
                    Id = GenerateId(),
                    Name = GenerateName(i),
                    SessionId = GenerateSessionId()
                };
                data.Add(entry);
            }

            return data;
        }
    }

    // 数据条目模型
    public class DataEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SessionId { get; set; }
    }
}
