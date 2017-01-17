# How to use C Libraries in ASP.NET Core

This project demonstrates how to call 3rd-party C API libraries from .NET Core.  The resulting Web API service can be deployed to Windows and Linux computers.  The example project uses <a href="https://www.leadtools.com">LEADTOOLS</a> as the 3rd party SDK because they have C libraries that can be downloaded from their website for Windows and Linux. The service resulting from this tutorial will convert any supported document or image to PNG. However, this method of wrapping native binaries can be applied to any 3rd party library.


## ASPNET-Core-Image-Conversion-Service

This example uses LEADTOOLS in ASP.NET Core to convert almost any image or document file to a PNG.  The resulting service can be hosted on Windows and Linux.

