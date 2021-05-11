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
            Console.ReadLine();
            svc.Close();
            Console.WriteLine("Chiusura servizio wcf");
        }
    }
}
