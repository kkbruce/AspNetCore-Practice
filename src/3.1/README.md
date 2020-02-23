# ASP.NET Core Practics

## Disclaimer

This is a test project when learning of ASP.NET Core. The XML comments have simple description. If you have any questions about the content, then implementing the project and using Postman or Fiddler to understand the test results will help you learn. A few examples make the browser a better choice.

## Give a Star! :star:

If you like or are using this project to learn, please give it a star. Thanks!

## Environment

* .NET Core 3.1 SDK
* ASP.NET Core 3.1
* Visual Studio 2019 16.4.x
* Visual Studio Code

## Run IActionResultSample

Demo ASP.NET Core `ControllerBase` `IActionResult` Helper

> Default route `[Route("ControllerName/[action]")]`.<br>

1. Click IActionResultSample.sln with Visual Studio 2019.
2. In .csproj folder run `dotnet run` 
3. In .csproj folder click F5 start Visual Studio Code debug

### ControllerBase Helper 101 Demo

* `AcceptedController` have 17 demo.
* `BadRequestController` have 5 demo.
* `ChallengeController` have 0 demo.
* `ConflictController` have 3 demo.
* `ContentController` have 5 demo.
* `CreatedController` have 11 demo.
* `FileController` have 24 demo.
* `ForbidController` have 0 demo.
* `NotFoundController` have 2 demo.
* `OkController` have 2 demo.
* `PhysicalFileController` have 8 demo.
* `ProblemController` have 4 demo.
* `RedirectController` have 9 demo.
* `SignController` have 0 demo.
* `StatusCodeController` have 3 demo.
* `UnauthorizedController` have 2 demo.
* `UnprocessableEntityController` have 3 demo.
* `ValidationProblemController` have 3 demo.

## AppsettingConfiguration 14 Demo

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

### ConfigurationBuilder 14 Demo

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

### FileUploadSample 8 Demo

* `SingleFile()`
* `SingleFileForm()`
* `SingleFileSaveDisk()`
* `MultiFilesUseCollection()`
* `MultiFilesUseIEnum()`
* `MultiFilesUseList()`
* `MultiFilesSaveDisk()`
* `UploadWithModel()`

### IHttpClientFactory/QueryMaskSample

Detail: [簡單四步驟：使用ASP.NET CORE提供口罩剩餘數量查詢API](https://blog.kkbruce.net/2020/02/aspnet-core-provider-mask-api.html)

### IHttpClientFactory/HttpClientSample 8 Demo

* `HttpBasicController()` have 3 demo.
* `RefitController()` have 1 demo.
* `RefitDIController()` have 1 demo.
* `SocketHttpController()` have 3 demo.

Part of it focuses on the configuration of `Startup.ConfigureServices()`.

### IHttpClientFactory/HttpClientConsoleSample

How to use `IHttpClientFactory` in Console application.

### Issue/RouteAsyncSuffix

Detail: [小心ASP.NET CORE裡ASYNC結尾ACTION方法！](https://blog.kkbruce.net/2020/02/aspnetcore-async-action-name.html)
