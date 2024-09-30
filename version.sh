cd /home/net5/cloudonesoftapps
chmod -R 777 ./siisoPublish
cd ./siisoPublish
cp appsettings.dev.json appsettings.json
cd ./Utils
echo "{\"VersionApp\":\"$(date '+%Y%m%d')\",\"ParcheApp\":\"$(date '+%H%M')\"}" > infoApp.json