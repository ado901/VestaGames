using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(Service1));
            svc.Open();
            Console.WriteLine("Apertura servizio wcf");

            //NEL CASO LA TABELLA PRODOTTI SIA VUOTA: DECOMMENTARE QUESTE RIGHE
            //Service1.popolateTable("Resident Evil Village", "Survival Horror", "Capcom", 20210418, 79.99, 20, "http://www.vestagames.it/defalco/src/Resident_Evil_Village.jpg");
            //Service1.popolateTable("It Takes Two", "Avventura", "Hazelight Studios", 20210326, 29.99, 20, "http://www.vestagames.it/defalco/src/It_Takes_Two_cover_art.jpg");
            Console.ReadLine();
            svc.Close();
            Console.WriteLine("Chiusura servizio wcf");
        }
    }
}
