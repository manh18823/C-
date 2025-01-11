using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void ConnectServer(string server, int port)
    {
        string message, responseData;
        int bytes;

        try
        {
            // Tạo TcpClient để kết nối đến server
            TcpClient client = new TcpClient(server, port);
            Console.Title = "Client Application";
            NetworkStream stream = null;

            while (true)
            {
                Console.Write("Input message <press Enter to exit>: ");
                message = Console.ReadLine();
                if (message == string.Empty)
                {
                    break; // Thoát khỏi vòng lặp nếu người dùng không nhập
                }

                // Chuyển thông điệp thành byte array
                Byte[] data = System.Text.Encoding.ASCII.GetBytes($"{message}");

                // Nhận stream để đọc và ghi
                stream = client.GetStream();

                // Gửi thông điệp đến server
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Sent: {message}");

                // Nhận phản hồi từ server
                data = new Byte[256];
                bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine($"Received: {responseData}");
            }

            // Đóng kết nối
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }
    }

    public static void Main(string[] args)
    {
        string server = "127.0.0.1"; // Địa chỉ server
        int port = 13000;           // Cổng server
        ConnectServer(server, port);
    }
}
