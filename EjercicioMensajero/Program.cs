using EjercicioMensajero.Comunicacion;
using MensajeroModel;
using MensajeroModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjercicioMensajero
{
    class Program
    {
        private static IMensajesDAL mensajesDAL = ILecturaDAL.GetInstancia();
        
        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Que desea hacer?");
            Console.WriteLine("1. Ingresar \n2. Mostrar \n0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1": Ingresar(); 
                    break;
                case "2": Mostrar();
                    break;
                case "0": continuar = false;
                    break;
                default: Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }

        static void Main(string[] args)
        {
            
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            
            while (Menu()) ;
        }

        static void Ingresar()
        {
            DateTime fecha = DateTime.Now;
            Console.WriteLine("Ingrese medidor: ");
            string medidor = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese lectura: ");
            string lectura = Console.ReadLine().Trim();
            Mensaje mensaje = new Mensaje()
            {
                Medidor = medidor,
                Fecha = fecha,
                Lectura = lectura
            };
           
            lock (mensajesDAL)
            { 
                mensajesDAL.IngresarLectura(mensaje);
            }
        }

        static void Mostrar()
        {
            List<Mensaje> mensajes = null;
            lock (mensajesDAL)
            {
                mensajes = mensajesDAL.ObtenerLecturas();
            }
            foreach (Mensaje mensaje in mensajes)
            {
                Console.WriteLine(mensaje);
            }
        }
    }
}
