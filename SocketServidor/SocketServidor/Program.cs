using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketServidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server();
        }
        public static void Server()
            
        {
            // Obtenemos información sobre la dirección IP del servidor local (localhost).
            IPHostEntry host = Dns.GetHostEntry("localhost");
            // Tomamos la primera dirección IP de la lista de direcciones obtenida.
            IPAddress ipAddress = host.AddressList[0];
            // Creamos un punto final que representa la dirección IP y un número de puerto.
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11200);

            try
            {
                // Creamos un socket, que es un objeto para la comunicación en red.
            // Especificamos el tipo de dirección IP, el tipo de socket (flujo) y el protocolo (TCP).
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // Asociamos el socket con el punto final local que hemos creado.
                listener.Bind(localEndPoint);
                // Ponemos el socket en modo de escucha para aceptar conexiones entrantes.
                listener.Listen(10);
                // Mostramos un mensaje en la consola para indicar que estamos esperando una conexión.
                Console.WriteLine("Esperando Conexion");
                // Esperamos hasta que un cliente se conecte y aceptamos esa conexión.
                Socket handler = listener.Accept();

                // Creamos variables para almacenar datos y bytes.
                string data = null;
                byte[] bytes = null;

                while (true) 
                {
                    // Creamos un búfer de bytes para recibir datos del cliente.
                    bytes = new byte[1024];
                    int byteRec = handler.Receive(bytes);
                    // Convertimos los bytes recibidos en una cadena de texto.
                    data = Encoding.ASCII.GetString(bytes, 0, byteRec);
                    // Si encontramos "<EOF>" en los datos recibidos, salimos del bucle.
                    if (data.IndexOf("<EOF>") > -1)
                        break;
                }
                // Mostramos en la consola el texto enviado por el cliente.
                Console.WriteLine("Texto del cliente: " + data);
            }
            catch (Exception e) 
            {
                // Si ocurre un error, mostramos el mensaje de error en la consola.
                Console.WriteLine(e.ToString());
            }
        }
    }
}
