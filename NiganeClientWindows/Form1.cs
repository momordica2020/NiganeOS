using Grpc.Net.Client;
using NiganeClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grpc.Net.Client;
using System.Security.Permissions;

namespace NiganeClientWindows
{
    public partial class Form1 : Form
    {
        Greeter.GreeterClient client;
        public Form1()
        {
            InitializeComponent();


            var channel = GrpcChannel.ForAddress("https://localhost:7283");
            client = new Greeter.GreeterClient(channel);
            Print = _print;
        }

        public delegate void sendStringEvent(string s);
        sendStringEvent Print;
        private void _print(string str)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (textBox1.Text.Length > 10000)
                    {
                        textBox1.Text=textBox1.Text.Substring(5000);
                    }
                    textBox1.Text += $"[{DateTime.Now.ToString("HH:mm:ss")}]{str}{Environment.NewLine}";
                }));
            }
            catch(Exception ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private async void sayHello()
        {
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });
            Print($"Greeting: {reply.Message}");
        }

        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            sayHello();
            // The port number must match the port of the gRPC server.
            
            //Console.WriteLine("Greeting: " + reply.ToString());
        }
    }
}
