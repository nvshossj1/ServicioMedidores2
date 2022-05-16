using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroModel.DAL
{
    public class IMedidorDAL : IMensajesDAL
    {

        
        private IMedidorDAL()
        {

        }
        
        private static IMedidorDAL instancia;
        
        public static IMensajesDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new IMedidorDAL();
            }
            return instancia;
        }

        private static string url = Directory.GetCurrentDirectory();

        private static string archivo = url + "/Lecturas.txt";
        public void IngresarLectura(Mensaje mensaje)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(mensaje.Medidor + "|" + mensaje.Fecha + "|" + mensaje.Lectura);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<Mensaje> ObtenerLecturas()
        {
            List<Mensaje> lista = new List<Mensaje>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            string idtxt = arr[0];
                            DateTime fecha = Convert.ToDateTime(arr[1]);
                            Mensaje mensaje = new Mensaje()
                            {
                                Medidor = arr[0],
                                Fecha = fecha,
                                Lectura = arr[2]
                            };
                            lista.Add(mensaje);
                        }
                    } while (texto != null);
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
