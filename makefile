all:
	mcs -pkg:dotnet35 -reference:HtmlAgilityPack.dll -reference:websocket-sharp.dll -reference:DSharpPlus.dll pigDiscord.cs
run: pigDiscord.exe
	sh runDiscord
clean:
	rm pigDiscord.exe
