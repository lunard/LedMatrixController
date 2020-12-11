# LED Matrix Controller
The project implements a simple Web API that allows an external application to setup a WS2812B LED matrix.
For the project I use a 8x32 RGB led matrix, like https://www.amazon.it/gp/product/B07KT1H481 and a Raspberry Pi 3 A+ with Raspian 32 bit.
The base setup I followed this useful page: https://jamesnaylor.dev/Posts/Read?id=pi-ws2812-with-net-core

## Deploy to the raspberry
To deploy the application on the raspberry is enough to compile it (I prefer a single file application) and run

 ```csharp
dotnet publish -r linux-arm  -p:PublishSingleFile=true
scp -r .\LedMatrixController\bin\Debug\netcoreapp3.1\linux-arm\publish\* pi@<rasp IP>:\home\pi\<your destination folder>
 ```

 Once started on the raspberry (./LedMatrixController) you can use one of the defined API actions, eg:
 
 ```
  wget --spider http://localhost:5000/api/test/led/1/2/blue
 ```

 ## Problems
 Build a single file doesn't embed native libreries (eg Serilog.Skins.SEQ.dll)
 To do that, we have to use the flag **IncludeNativeLibrariesForSelfExtract**, but it seems not to work !
