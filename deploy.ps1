dotnet publish --configuration Release
ftp -s:ftp.dat

#hemos generado un key-ge para acceder con ssh sin password
#desde powershell ( si no es admin se cuelga )
#genera la clave
 #ssh-keygen -t rsa -b 2048 <ENTER> <ENTER> <ENTER>
#copia la clave en servidor remoto
 #cat ~/.ssh/id_rsa.pub | ssh carlozzer@busmon.westeurope.cloudapp.azure.com "cat >> ~/.ssh/authorized_keys"

ssh carlozzer@busmon.westeurope.cloudapp.azure.com "sudo cp bus/* /var/www/busmon/"
ssh carlozzer@busmon.westeurope.cloudapp.azure.com "sudo service nginx restart"
ssh carlozzer@busmon.westeurope.cloudapp.azure.com 




