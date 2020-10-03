# TaxRateScheduler
Use the postman for better request response . 
Open the solution in visual studio 2019 -- core 3.1 must wait for package installation. 
build the solution - solved the error or contact me for it. 
for db -- entity framework core configure your local sql server config -add in appsettings.json 
use = add-migration taxrate1 
update-database 
run the solution - it will open in swagger follow the instruction given by swagger. 
use postman. 
upload the file -attached. 
add any tax rate with name and schedule type. 
input format -- name, taxrate,schedule type, year., startdate, enddate. 
now use get method for get the taxrate - input name , date dateformat -- local storage.
File format:

--------- Municipality Tax Rate Yearly --------
Mumbai|1.0|2020
Delhi|2.0|2020
Gujrat|1.2|2020
