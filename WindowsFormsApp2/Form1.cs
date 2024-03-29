﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket T;   //通訊物件
        string User;    //使用者

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string IP = IPBox.Text;
            int Port = int.Parse(PortBox.Text);
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);

            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            User = userbox.Text;

            try
            {
                T.Connect(EP);
                Send("0" + User);

            }
            catch
            {
                MessageBox.Show("無法連上伺服器!!");
                return;
            }
            button1.Enabled = false;
        }

        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);
            T.Send(B, 0, B.Length, SocketFlags.None);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(button1.Enabled == false)
            {
                Send("9" + User);
                T.Close();
            }
        }
    }
}
