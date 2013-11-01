using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;

namespace HelloClient
{
    public partial class Form1 : Form
    {
        // client
        TcpClient clientSocket = new TcpClient();
        string strIP = "127.0.0.1";
        int nPort = 7777;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect(strIP,nPort);
            label1.Text = "Client Socket Program - Server Connected ...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // get user input data 
            string strData = this.tb_data.Text;
            if (string.IsNullOrEmpty(strData))
            {
                return;
            }

            // send data to server
            NetworkStream networkStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(strData+"$");
            networkStream.Write(outStream, 0, outStream.Length);
            networkStream.Flush();

            // get data from server
            byte[] inStream = new byte[10025];
            networkStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = Encoding.ASCII.GetString(inStream);
            msg("Data from Server : " + returndata);
        }

        public void msg(string mesg)
        {
            richTextBox1.Text = richTextBox1.Text + Environment.NewLine + " >> " + mesg;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            clientSocket.Close();
        } 
    }
}
