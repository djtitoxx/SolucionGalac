# SolucionGalac
Instrucciones de Instalacion en archivo de Texto

Modificar la conexion en el archivo appsettings.json

"ConnectionStrings": {
        "MyConnection": "Data Source =DESKTOP-868MOLJ\\SQLEXPRESS;Database=DBCONTROLINVENTARIO;User ID=sa;Password=1234;Trusted_Connection=false;MultipleActiveResultSets=true"
        },

---------------------------------------------------


Ejecutar los siguientes comandos en el Package Manager Console, para realizar
la migración de la Base de Datos

Add-Migration InitialMigration

update-database


--------------------------------------------------
Nota: para utilizar los metodos Post, por favor omitir el ID
