# ASP.NET Core Practics

## 免責聲明

此為在對 ASP.NET Core 進行學習時的測試專案，註解都有簡易的說明。如果您對內容有疑惑時，那麼把專案執行起來，使用 Postman 或 Fiddler 瞭解測試的結果，將有助於學習。其中一些範例，使用瀏覽器會是比較好的選擇。

## 請給一顆星星！ :star:

如果您喜歡或使用此專案進行學習，請給它一顆星星，謝謝。

## 開發環境

* .NET Core 3.1 SDK
* ASP.NET Core 3.1
* Visual Studio 2019 16.4.x
* Visual Studio Code

## 運行 IActionResultSample

展示 ASP.NET Core `ControllerBase` `IActionResult` 輔助方法

> 預設路由 `[Route("ControllerName/[action]")]`。

1. 點擊 IActionResultSample.sln 啟動 Visual Studio 2019
2. 在 .csproj 所在目錄執行 `dotnet run`
3. 在 .csproj 所在目錄以 F5 啟動 Visual Studio Code 偵錯

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

### QueryMaskSample

Detail: [簡單四步驟：使用ASP.NET CORE提供口罩剩餘數量查詢API](https://blog.kkbruce.net/2020/02/aspnet-core-provider-mask-api.html)

### RouteAsyncSuffix

Detail: [小心ASP.NET CORE裡ASYNC結尾ACTION方法！](https://blog.kkbruce.net/2020/02/aspnetcore-async-action-name.html)