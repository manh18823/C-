using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    // Xử lý thông điệp từ client
    static void ProcessMessage(object parm)
    {
        string data;
        int count;

        try
        {
            TcpClient client = parm as TcpClient;

            // Buffer for reading data
            Byte[] bytes = new Byte[256];

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            // Loop to receive all the data sent by the client
            while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to an ASCII string
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                Console.WriteLine($"Received: {data} at {DateTime.Now:t}");

                // Process the data sent by the client
                data = $"{data.ToUpper()}";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Send back a response
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine($"Sent: {data}");
            }

            // Shutdown and end connection
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Waiting message...");
        }
    }
    // end ProcessMessage


    // Khởi động server để lắng nghe các kết nối
    static void ExecuteServer(string host, int port)
    {
        int Count = 0;
        TcpListener server = null;

        try
        {
            Console.Title = "Server Application";

            // Parse the IP address and create a TcpListener
            IPAddress localAddr = IPAddress.Parse(host);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests
            server.Start();
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Waiting for a connection...");

            // Enter the listening loop
            while (true)
            {
                // Perform a blocking call to accept requests
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"Number of clients connected: {++Count}");
                Console.WriteLine(new string('*', 40));

                // Create a thread to handle communication with the client
                Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                thread.Start(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            // Stop the server
            server?.Stop();
            Console.WriteLine("Server stopped. Press any key to exit!");
            Console.Read();
        }
    }


    public static void Main()
    {
        string host = "127.0.0.1"; // Địa chỉ IP của server
        int port = 13000;          // Cổng để lắng nghe kết nối
        ExecuteServer(host, port); // Gọi hàm khởi động server
    }
}

