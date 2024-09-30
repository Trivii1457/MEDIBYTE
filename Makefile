gitPull:
	git pull --progress -v --no-rebase "origin" && \
	echo ******* REPOSITORIO SIISO ACTUALIZADO  *******
	
publicar:
	cd ./WebApp && \
	dotnet restore --force -s https://api.nuget.org/v3/index.json -s https://nexus.dbusiness.app/repository/nuget-group/ -s https://nuget.devexpress.com/nYVMfL2DHjdhvBeIVCLpdfJqwAjciBDPXo836DK6lFsCbB5gdz/api && \
	echo ******* PROYECTO SIISO RESTAURADO  ******* && \
	dotnet build && \
	echo ******* PROYECTO SIISO COMPILADO  ******* && \
	dotnet publish -r linux-x64 -c Release --self-contained true -p:PublishSingleFile=false -o ../publish && \
	echo ******* PROYECTO SIISO PUBLICADO PARA LINUX X64  *******

setVersion:
	./version.sh

build: gitPull publicar setVersion

up: 
	chmod -R 777 publish && systemctl restart siisotest.service

siiso: build up
