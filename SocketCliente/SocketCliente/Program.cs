using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        Client();
    }

    public static void Client()
    {
        // Creamos un socket para el cliente.
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // Creamos un punto final que representa la dirección del servidor y el puerto al que nos conectaremos.
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11200);

            // Conectamos el socket del cliente al servidor.
            clientSocket.Connect(serverEndPoint);

            // Mensaje que queremos enviar al servidor.
            string message = "Hola, servidor. Este es un mensaje de ejemplo.<EOF>";

            // Convertimos el mensaje a un arreglo de bytes para enviarlo a través del socket.
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            // Enviamos los bytes al servidor.
            clientSocket.Send(messageBytes);

            Console.WriteLine("Mensaje enviado al servidor.");

            // Cerramos la conexión con el servidor.
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch (Exception e)
        {
            // En caso de error, mostramos el mensaje de error en la consola.
            Console.WriteLine(e.ToString());
        }
    }
}