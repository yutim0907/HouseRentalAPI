# HouseRentalAPI

HouseRentalAPI是一個練習用專案，使用dot NET 6建立Restful API，提供租屋貼文的 CRUD 功能。

## 功能
* 可以查詢所有租屋貼文列表
* 可以根據租屋貼文的id查詢單一租屋貼文
* 可以根據租屋貼文的城市查詢租屋貼文列表
* 可以根據租屋貼文的價格查詢租屋貼文列表
* 可以新增租屋貼文
* 可以更新租屋貼文
* 可以刪除租屋貼文

## 架構
* 專案使用.NET 6進行開發
* 資料庫使用Entity Framework Core，採用Db First方式建立，使用MSSQL作為儲存資料的介面。
* 單元測試使用了NUnit
