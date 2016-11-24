using System.Collections.Generic;
using CarReservation.Common.DataTransferObjects;

namespace CarReservation.Common.Interfaces
{
    public interface ICarReservationService
    {
        List<CarDto> Cars { get;}
        List<CustomerDto> Customers { get; }
        List<ReservationDto> Reservations { get; }
        CarDto GetCarById(int id) ;
        CustomerDto GetCustomerById(int id);
        ReservationDto GetReservationByNr(int reservationsNr);
        CarDto InsertCar(CarDto car);
        CustomerDto InsertCustomer(CustomerDto customer);
        ReservationDto InsertReservation(ReservationDto reservation);
        CarDto UpdateCar(CarDto car);
        CustomerDto UpdateCustomer(CustomerDto customer);
        ReservationDto UpdateReservation(ReservationDto reservation);
        void DeleteCar(CarDto car);
        void DeleteCustomer(CustomerDto customer);
        void DeleteReservation(ReservationDto reservation);
    }
}
