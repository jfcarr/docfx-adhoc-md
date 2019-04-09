
default:
	@echo 'Targets:'
	@echo '  build-win10   Build Windows 10 executable'
	@echo '  build         Build portable executable (dll)'
	@echo '  clean'

build-win10:
	dotnet build -r win10-x64

build:
	dotnet build

clean:
	-rm -rf bin
	-rm -rf obj
