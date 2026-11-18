@echo off
cd /d %~dp0SwitchForge
echo Building SwitchForge Windows...
dotnet publish SwitchForge.csproj -c Release -r win-x64 --self-contained false -p:PublishSelfContained=false -p:Platform=x64
echo Done!
explorer bin\x64\Release\net10.0-windows\win-x64\publish
pause