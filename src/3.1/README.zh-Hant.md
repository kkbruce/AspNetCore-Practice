# ASP.NET Core Practics

## 免責聲明

此為在對 ASP.NET Core 進行學習時的測試專案，註解都有簡易的說明。如果您對內容有疑惑時，那麼把專案執行起來，使用 <a href="https://www.postman.com" target="_blank">Postman</a> 或 <a href="https://www.telerik.com/fiddler" target="_blank">Fiddler</a> 瞭解測試的結果，將有助於學習。其中一些範例，使用瀏覽器會是比較好的選擇。

## 請給一顆星星！ :star:

如果您喜歡或使用此專案進行學習，請給它一顆星星，謝謝。

## 開發環境

* .NET Core 3.1.x SDK
* ASP.NET Core 3.1
* Visual Studio 2019 16.4.x
* Visual Studio Code

## 如果執行專案

1. 在專案目錄下點擊 **.sln** 啟動 Visual Studio 2019
2. 在 .csproj 所在目錄執行 `dotnet run`
3. 在 .csproj 所在目錄開啟 Visual Studio Code 並按 F5 執行偵錯

### ControllerBase 輔助方法 101 範例展示

* `AcceptedController` 有 17 範例.
* `BadRequestController` 有 5 範例.
* `ChallengeController` 有 0 範例.
* `ConflictController` 有 3 範例.
* `ContentController` 有 5 範例.
* `CreatedController` 有 11 範例.
* `FileController` 有 24 範例.
* `ForbidController` 有 0 範例.
* `NotFoundController` 有 2 範例.
* `OkController` 有 2 範例.
* `PhysicalFileController` 有 8 範例.
* `ProblemController` 有 4 範例.
* `RedirectController` 有 9 範例.
* `SignController` 有 0 範例.
* `StatusCodeController` 有 3 範例.
* `UnauthorizedController` 有 2 範例.
* `UnprocessableEntityController` 有 3 範例.
* `ValidationProblemController` 有 3 範例.

## AppsettingConfiguration 14 範例展示

* `GetKeyValue();`
* `GetConnectionString();`
* `GetHierarchicalData();`
* `BindToAClass();`
* `GetEnvModeConfig();`
* `GetOSEnvironment();`
* `GetMemoryConfig();`
* `GetCommandLineConfig();`
* `GetValueSample();`
* `GetSectionSample();`
* `GetChildrenSample();`
* `ExistsSample();`
* `GetIniSample();`
* `GetXMLSample();`

### ConfigurationBuilder 14 範例展示

* `GetKeyValue()`
* `GetConnectionString()`
* `GetHierarchicalData()`
* `BindToAClass()`
* `GetEnvModeConfig()`
* `GetOSEnvironment()`
* `GetMemoryConfig()`
* `GetCommandLineConfig()`
* `GetValueSample()`
* `GetSectionSample()`
* `GetChildrenSample()`
* `ExistsSample()`
* `GetIniSample()`
* `GetXMLSample()`

### FileUploadSample 8 範例展示

* `SingleFile()`
* `SingleFileForm()`
* `SingleFileSaveDisk()`
* `MultiFilesUseCollection()`
* `MultiFilesUseIEnum()`
* `MultiFilesUseList()`
* `MultiFilesSaveDisk()`
* `UploadWithModel()`

### IHttpClientFactory/QueryMaskSample
### IHttpClientFactory/QueryMaskSqliteSample

* 文章 1: [簡單四步驟：使用ASP.NET CORE提供口罩剩餘數量查詢API](https://blog.kkbruce.net/2020/02/aspnet-core-provider-mask-api.html)
* 文章 2: [簡單五步驟：以EF Core整合SQLite儲存口罩剩餘數量資訊](https://blog.kkbruce.net/2020/02/ef-core-sqlite.html)

### IHttpClientFactory/HttpClientSample 8 範例展示

* `HttpBasicController()` 有 3 個範例。
    * Basic
    * Named
    * Polly
* `RefitController()` 有 1 個範例。
* `RefitDIController()` 有 1 個範例。
* `SocketHttpController()` 有 3 個範例。

其中一部份重點在 `Startup.ConfigureServices()` 的組態上。

### IHttpClientFactory/HttpClientConsoleSample

如何使用 `IHttpClientFactory` 在主控台應用程式。

### Issue/RouteAsyncSuffix

文章: [小心ASP.NET CORE裡ASYNC結尾ACTION方法！](https://blog.kkbruce.net/2020/02/aspnetcore-async-action-name.html)