﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace UDPSensorBroadcaster
{
    class Program
    {
        public static void Main(string[] args)
        {
            int number = 0;
            double co = 0.30; double nox = 70.0; string level = "Medium";
            Random rnCo = new Random();
            Random rnNox = new Random();

            string sensorLocation = "Pollution sensor v.1.0. \r\n" + "Location: Jernbanegade 3 1\r\n";

            UdpClient udpServer = new UdpClient(0);
            udpServer.EnableBroadcast = true;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 1111);

            Console.WriteLine("Broadcast ready. Get started Press Enter");
            Console.ReadLine();
            DateTime currentTime1 = DateTime.Now;
            string timeText1 = "Time: " + currentTime1.ToString() + " \r\n";
            Console.WriteLine(timeText1);
            Console.ReadLine();

            while (true)
            {
                co = 0.20 + rnCo.Next(0, 100) / 99.9;
                nox = 70.0 + rnNox.Next(0, 150);

                level = "High";
                if (nox < 100) { level = "Low"; }
                else
                    if (nox < 150) { level = "Medium"; }

                DateTime currentTime = DateTime.Now;
                string timeTxt = "Time: " + currentTime + " \r\n";
                string data = "CO: " + co + " \r\n" + "NOx: " + nox + " \r\n" + "Particle level: " + level + " \r\n \r\n" + "Number: " + number;
                string sensorData = sensorLocation + timeTxt + data;

                Byte[] sendBytes = Encoding.ASCII.GetBytes(sensorData);

                try
                {
                    udpServer.Send(sendBytes, sendBytes.Length, endPoint); //, endPoint
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                number++;
                Console.WriteLine(" " + number);
                Console.WriteLine(sensorData);
                Thread.Sleep(2000);
            }

        }
    }
}


