using ConsoleSocketServer;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Tutorial.SqlConn;

public class Server
{
    private ILogSaver logSaver;
    private TcpListener listener;
    public Server(string ip, int port)
    {
        logSaver = new FileLogSaver("log.txt");

        listener = new TcpListener(IPAddress.Parse(ip), port);
        listener.Start();

        Listen();
    }
    public void Listen()
    {
        while (listener.Server.IsBound)
        {
            //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Ожидание подключений...");
            var tcpClient = listener.AcceptTcpClient();

            //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Полученно входящее соединение!");
            ThreadPool.QueueUserWorkItem(state => HandleDeivce(tcpClient));
        }
    }
    public void HandleDeivce(TcpClient tcpClient)
    {
        try
        {
            var streamReader = new StreamReader(tcpClient.GetStream());
        var streamWriter = new StreamWriter(tcpClient.GetStream());
        
        var message = streamReader.ReadLine();
        Console.WriteLine(message);
        Console.WriteLine("Received: ", message + "\n");
        //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "{1}: Received: {0}", message, Thread.CurrentThread.ManagedThreadId);
        logSaver.Save(message);
        //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Getting Connection ...");
        //MySqlConnection conn = DBUtils.GetDBConnection();

        
            //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Openning Connection ...");

            //conn.Open();

            //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Connection successful!");
        

        string response = "done";
        streamWriter.WriteLine(response);
        streamWriter.Flush();
        //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "{1}: Sent: {0}", response, Thread.CurrentThread.ManagedThreadId);
        }
        catch (Exception e)
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Error: " + e.Message);
        }
        /*string imei = String.Empty;
        string data = null;
        Byte[] bytes = new Byte[256];
        int i;
        try
        {
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                string hex = BitConverter.ToString(bytes);
                data = Encoding.Unicode.GetString(bytes, 0, i);
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);
                logSaver.save(data);
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Getting Connection ...");
                MySqlConnection conn = DBUtils.GetDBConnection();

                try
                {
                    Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Openning Connection ...");

                    conn.Open();

                    Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Connection successful!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Error: " + e.Message);
                }
                string str = "done";
                Byte[] reply = System.Text.Encoding.Unicode.GetBytes(str);
                stream.Write(reply, 0, reply.Length);
                Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "{1}: Sent: {0}", str, Thread.CurrentThread.ManagedThreadId);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Exception: {0}", e.ToString());
            client.Close();
        }
        */
    }
}