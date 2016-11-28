using System.Collections.Generic;
using System.ServiceModel;
using CarReservation.Common.DataTransferObjects;

namespace CarReservation.Common.Interfaces
{
    [ServiceContract]
    public interface ICarReservationService
    {
        List<CarDto> Cars
        {
            [OperationContract]
            get;
        }

        List<CustomerDto> Customers
        {
            [OperationContract]
            get;
        }

        List<ReservationDto> Reservations
        {
            [OperationContract]
            get;
        }
        [OperationContract]
        CarDto GetCarById(int id);
        [OperationContract]
        CustomerDto GetCustomerById(int id);
        [OperationContract]
        ReservationDto GetReservationByNr(int reservationsNr);
        [OperationContract]
        CarDto InsertCar(CarDto car);
        [OperationContract]
        CustomerDto InsertCustomer(CustomerDto customer);
        [OperationContract]
        ReservationDto InsertReservation(ReservationDto reservation);
        [OperationContract]
        CarDto UpdateCar(CarDto car);
        [OperationContract]
        CustomerDto UpdateCustomer(CustomerDto customer);
        [OperationContract]
        ReservationDto UpdateReservation(ReservationDto reservation);
        [OperationContract]
        void DeleteCar(CarDto car);
        [OperationContract]
        void DeleteCustomer(CustomerDto customer);
        [OperationContract]
        void DeleteReservation(ReservationDto reservation);
    }
}
