.nuget\NuGet.exe update -self

%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe AppHarbor.RequestTracer.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger
.nuget\NuGet.exe pack AppHarbor.RequestTracer\AppHarbor.RequestTracer.nuspec