# Appharbor RequestTracer

This module traces incoming requests and their outgoing responses using the event log. The module should work on any platform but is specifically designed to work with AppHarbor.

## Configuration

To set up the module properly add the following module to your web.config:

```xml
<configuration>
	<appSettings>
		<!-- set this flag to 1 to enable the request tracer-->
		<add key="appharbor.requesttracer.enabled" value="0" />
	</appSettings>
	<system.webServer>
		<modules>
			<remove name="AppHarborRequestTracer" />
			<add name="AppHarborRequestTracer" type="AppHarbor.RequestTracer.RequestLoggingModule, AppHarbor.RequestTracer" />
		</modules>
	</system.webServer>
</configuration>
```

Please note that this might already be done automagically when you installed the library from NuGet.

Request logging is disabled by default, to enable set application setting `appharbor.requesttracer.enabled` to `1`.

## Contributors

All help is welcome!

## Copyright

Copyright © 2012 Premotion Software Solutions and contributors.

## License

Appharbor RequestTracer is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to license.txt for more information.

## Download
You can download the compiled binary from NuGet: [https://nuget.org/packages/AppHarbor.RequestTracer](https://nuget.org/packages/AppHarbor.RequestTracer)