# 專案簡介
1. 目前功能涵蓋ClickHouse連線與CRUD操作
2. 總共設計五個功能操作資料庫, 功能函式整合在Helper/ClickHouseHelper.cs
3. Program.cs設置每個功能的簡易測試情境
4. Models/ 歸納目前設置的兩個model (EventLogAbstract.cs, ItemLogAbstract.cs)
5. Configs/ 歸納所有設置config檔,未來的config檔也將收入於此


# 路徑目錄
ClickHouseApi/
│
├── ClickHouseApi.csproj          
├── Program.cs                   
│
├── Controllers/                 
│   └── ClickHouseController.cs
│
├── Helpers/                      
│   └── ClickHouseHelper.cs
│
├── Models/                      
│   └── EventLogAbstract.cs
│   └── ItemLogAbstract.cs
│
├── Configs/                      
│   └── appsettings.Development.json          
│   └── appsettings.json         
│
└── README.md     


# Program.cs測試說明
所有功能設置都有建立可以直接測試的測試參數
所有參數都可以作手動調整

1. var tableName = ""; 可設置要創建的table名稱

2. var entityType = typeof(); 可設置要使用的model (EventLogColumn, ItemLogColumn)

3. var insertColumns = new List<string> {};
   var insertValues = new List<object> {};
   insertColumns可以設定要寫入資料的欄位, insertValues可設置對應的值

4. var deleteConditions = new Dictionary<string, object>
    {
        {},
        {}
    };
    可以設定要指定刪除的欄位以及與之對應到的值
           

# ClickHouseHelper.cs函式名稱與參數解釋

1. 創建表格 CreateTable 
(string connectionString, string tableName, Type entityType)
(連接資訊, 表單名稱, 類別)

2. 插入資料 InsertTable 
(string connectionString, string tableName, IEnumerable<string> columns, IEnumerable<object> values)
(連接資訊, 表單名稱, 目標欄位, 欄位值)

3. 讀取資料 ReadTable 
#(string connectionString, string tableName)
#(連接資訊, 表單名稱)

4. 刪除單筆資料 DeleteRow 
(string connectionString, string tableName, Dictionary<string, object> conditions)
(連接資訊, 表單名稱, 刪除條件(字典))

5. 刪除整張表格 DeleteTable 
(string connectionString,string tableName)
(連接資訊, 表單名稱)


# ClickHouse連線設置
Configs/appsettings.json 檔案包含資料庫連接字串及其他配置

