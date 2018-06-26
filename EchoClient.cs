using System;
using System.Text; 
using System.IO;
using System.Net.Sockets;

namespace EchoClient
{
    class Program
    {
        private const String host = "localhost";
        private const int port = 5005;
        
        static void Main(string[] args) 
        {
            TcpClient client = null;
            NetworkStream networkStream = null;

            try 
            {
                client = new TcpClient(host, port);
                Console.WriteLine("Connected to server...");
                networkStream = client.GetStream();

                byte[] buffer = new byte[1024];
                
                // Read welcome message from server
                networkStream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("Server said:" + System.Text.Encoding.ASCII.GetString(buffer));
                
                // Write message to server
                buffer = Encoding.ASCII.GetBytes("Hello, the password is 's3cr3t007'.\n");
                networkStream.Write(buffer, 0, buffer.Length);
                
                // Read response from server
                networkStream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("Server said:" + Encoding.ASCII.GetString(buffer));
                
                // Quit
                Console.WriteLine("Okay, quitting now...");
                buffer = Encoding.ASCII.GetBytes("quit\n");
                networkStream.Write(buffer, 0, buffer.Length);
            } 
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            } 
            finally 
            {
                if(networkStream != null) 
		        {
			        networkStream.Close();
		        }
                if(client != null)
                {
                    client.Close(); // Always close your sockets and clean up after yourself!
                }
            }
        }
    }
}
