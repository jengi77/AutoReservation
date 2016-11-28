using System;
using System.ServiceModel;

namespace CarReservation.Service.Wcf.Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CarReservationService starting.");
            
            // Instantiate new ServiceHost 
            ServiceHost host = new ServiceHost(typeof(CarReservationService));

            // Open ServiceHost
            host.Open();

            Console.WriteLine("CarReservationService started.");
            Console.WriteLine();
            Console.WriteLine("Press Return to stop the Service.");

            Console.ReadLine();

            // Stop ServiceHost
            if (host.State != CommunicationState.Closed)
            {
                host.Close();
            }
        }
    }
}
