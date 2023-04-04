using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductosAPI
{
    public class Program
    {//esto es comentario de develop
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //Aqui voy a crear comentario en una rama creada desde develop
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        //Agregando Tercer Comentario PRueba
    }
}



/*
Errores:

200: Todo fue correcto y la petición se hizo sin problemas.

201: Informa que un elemento fue creado o modificado en el Servidor!

400: Hubo algun error en la petocion y algo salio mal

401: Desautorizado, el cliente no tiene permisos de entrar a nuestro server o de hacer peticiones a nuestro server.

403: que nuestro server ha entendido nuestra peticion, pero se niega a utilizarla.

405: Metodo no permitido, es decir, que el server tiene bloqueado algun metodo http (GET, POST, DELETE, PUT, PATCH, etc)

500: Que algo anda mal con nuestro server a nivel interno, sea algun error en el codigo o alguna otra cosa ajena a nuestra logica.

 
 
 */