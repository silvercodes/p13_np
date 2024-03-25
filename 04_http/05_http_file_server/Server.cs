using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _05_http_file_server;

internal class Server
{
    private HttpListener listener = null!;
    private string host = "127.0.0.1";
    private int port;
    private Thread serverThread;
    private string[] indexFiles = 
    { 
        "index.html",
    };


    public required string RootDirectory { get; set; }

    public Server(int port)
    {
        this.port = port;

        InitServer();

        serverThread = new Thread(Start);
        serverThread.IsBackground = false;
        serverThread.Start();
    }

    private void InitServer()
    {
        listener = new HttpListener();
        listener.Prefixes.Add($@"http://{host}:{port}/");           // LAST SLASH !!!
    }

    private void Start()
    {
        listener.Start();
        Console.WriteLine($"Server started at {host}:{port}");

        while(true)
        {
            try
            {
                HttpListenerContext ctx = listener.GetContext();     // Blocking

                _ = HandleRequest(ctx);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection error: {ex.Message}");
            }
        }
    }

    private async Task HandleRequest(HttpListenerContext ctx)
    {
        string? path = ctx.Request.Url?.AbsolutePath;

        await Console.Out.WriteLineAsync($"Path requested: {path}");

        path = path.Trim('/');

        
        if (string.IsNullOrEmpty( path ))
        {
            foreach(string indexFile in indexFiles)
            {
                if (File.Exists(Path.Combine(RootDirectory, indexFile)))
                {
                    path = indexFile;
                    break;
                }
            }
        }

        string filePath = Path.Combine(Path.Combine(RootDirectory, path));

        if (File.Exists(filePath))
        {


        }


    }


}
