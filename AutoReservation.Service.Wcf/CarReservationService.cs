using System;
using System.Collections.Generic;
using System.Diagnostics;
using CarReservation.Common.DataTransferObjects;
using CarReservation.Common.Interfaces;

namespace CarReservation.Service.Wcf
{
    public class CarReservationService : ICarReservationService
    {
        public List<CarDto> Cars
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public List<CustomerDto> Customers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public List<ReservationDto> Reservations
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }

        public void DeleteCar(CarDto car)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public CarDto GetCarById(int id)
        {
            throw new NotImplementedException();
        }

        public CustomerDto GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public ReservationDto GetReservationByNr(int reservationsNr)
        {
            throw new NotImplementedException();
        }

        public CarDto InsertCar(CarDto car)
        {
            throw new NotImplementedException();
        }

        public CustomerDto InsertCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public CarDto UpdateCar(CarDto car)
        {
            throw new NotImplementedException();
        }

        public CustomerDto UpdateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }
    }
}