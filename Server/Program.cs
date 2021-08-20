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
            try
            {
                ServiceHost svc = new ServiceHost(typeof(Service1));
                svc.Open();
                Console.WriteLine("Apertura service1");

                ServiceHost svc2 = new ServiceHost(typeof(admin));
                svc2.Open();
                Console.WriteLine("Apertura admin");
                Console.ReadLine();
                svc.Close();
                svc2.Close();
                Console.WriteLine("Chiusura servizio wcf");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Apertura non riuscita dei wcf: " + ex.Message);
                Console.ReadLine();
            }
            

            //NEL CASO LA TABELLA PRODOTTI SIA VUOTA: DECOMMENTARE QUESTE RIGHE
            //Service1.popolateTable("Resident Evil Village", "Survival Horror", "Capcom", 20210418, 79.99, 20, "http://www.vestagames.it/defalco/src/Resident_Evil_Village.jpg");
            //Service1.popolateTable("It Takes Two", "Avventura", "Hazelight Studios", 20210326, 29.99, 20, "http://www.vestagames.it/defalco/src/It_Takes_Two_cover_art.jpg");
            
            
            
        }
    }
}
