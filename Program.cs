using System;
using System.Threading;
using Infineon.Yoda;

namespace YodaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //int i = 0;
            String yodaChannel = "RBG_T_TEST";
            String remotePath = @"\\mitconfig.rbg.infineon.com\yodaconfig\mc2uc";
            String hostType = "SERVER";
            String subject = "RBG_T.PCS.EM.TEST";

            Console.WriteLine("Yoda Channel: " + yodaChannel);
            Console.WriteLine("Remote Path: " + remotePath);
            Console.WriteLine("Host Type: " + hostType);
            Console.WriteLine("Subject: " + subject);

            IfxDoc message = new IfxDoc();
            message.Add("Application", "Infineon Cloud Platform");
            message.Add("Message", "Hello World");

            

                while (true)
                {
                try
                {
                    Publish(message, yodaChannel, remotePath, hostType, subject, 10);
                    Console.WriteLine("Message sent to: " + subject);
                    //i++;
                    //Console.WriteLine(i + ": Hello World!");
                    Thread.Sleep(2000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);

                }
                }
            
        }

        public static void Publish(IfxDoc message, String yodaChannel, String remotePath, String hostType, String subject, int timeout)
        {
            IfxDoc config = new IfxDoc();
            config.Add(IfxConst.IFX_CHANNEL, yodaChannel);
            config.Add(IfxConst.IFX_CONFIGURATION_LOCATION, remotePath);
            config.Add("CTX HOSTTYPE", hostType);

            IfxTransport EMTransport = new IfxTransport(config);
            EMTransport.FaultToleranceWeight = 1;

            message.SendSubject = subject;

            // Send message
            EMTransport.Publish(message, IfxConst.IfxPublishMode.Default, timeout);
        }
    }
}
