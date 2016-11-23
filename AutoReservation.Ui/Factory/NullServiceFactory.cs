using System.Collections.Generic;
using CarReservation.Common.DataTransferObjects;
using CarReservation.Common.Interfaces;

namespace CarReservation.Ui.Factory
{
    public class NullServiceFactory : IServiceFactory
    {
        public ICarReservationService GetService()
        {
            return new NullCarReservationService();
        }
    }

    public class NullCarReservationService : ICarReservationService
    {
        public List<CarDto> Cars => new List<CarDto>();
        public List<CustomerDto> Customers => new List<CustomerDto>();
        public List<ReservationDto> Reservations => new List<ReservationDto>();
        public CarDto GetCarById(int id) => null;
        public CustomerDto GetCustomerById(int id) => null;
        public ReservationDto GetReservationByNr(int reservationsNr) => null;
        public CarDto InsertCar(CarDto auto) => null;
        public CustomerDto InsertCustomer(CustomerDto customer) => null;
        public ReservationDto InsertReservation(ReservationDto reservation) => null;
        public CarDto UpdateCar(CarDto car) => null;
        public CustomerDto UpdateCustomer(CustomerDto customer) => null;
        public ReservationDto UpdateReservation(ReservationDto reservation) => null;
        public void DeleteCar(CarDto car) { }
        public void DeleteCustomer(CustomerDto customer) { }
        public void DeleteReservation(ReservationDto reservation) { }
    }
}
