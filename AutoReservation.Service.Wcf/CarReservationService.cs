using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using CarReservation.BusinessLayer;
using CarReservation.Common.DataTransferObjects;
using CarReservation.Common.Interfaces;
using CarReservation.Dal;
using CarReservation.Dal.Entities;

namespace CarReservation.Service.Wcf
{
    public class CarReservationService : ICarReservationService
    {
        private readonly CarReservationBusinessComponent _businessComponent = new CarReservationBusinessComponent();
        public List<CarDto> Cars
        {
            get
            {
                WriteActualMethod();
                return _businessComponent.GetAll<Car>().ConvertToDtos();
            }
        }

        public List<CustomerDto> Customers
        {
            get
            {
                WriteActualMethod();
                return _businessComponent.GetAll<Customer>().ConvertToDtos();
            }
        } 

        public List<ReservationDto> Reservations
        {
            get
            {
                WriteActualMethod();
                return _businessComponent.GetAllReservation().ConvertToDtos();
            }
        }

        public void DeleteCar(CarDto car)
        {
            WriteActualMethod();
            _businessComponent.DeleteObject(car.ConvertToEntity());
        }

        public void DeleteCustomer(CustomerDto customer)
        {
            WriteActualMethod();
            _businessComponent.DeleteObject(customer.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            _businessComponent.DeleteObject(reservation.ConvertToEntity());
        }

        public CarDto GetCarById(int id)
        {
            WriteActualMethod();
            return _businessComponent.GetElement<Car>(c  => c.Id == id).ConvertToDto();
        }

        public CustomerDto GetCustomerById(int id)
        {
            WriteActualMethod();
            return _businessComponent.GetElement<Customer>(c => c.Id == id).ConvertToDto();
        }

        public ReservationDto GetReservationByNr(int reservationNo)
        {
            WriteActualMethod();
            return _businessComponent.GetReservationById(reservationNo).ConvertToDto();
        }

        public CarDto InsertCar(CarDto car)
        {
            WriteActualMethod();
            return _businessComponent.SaveObject(car.ConvertToEntity(),car.Id, true).ConvertToDto();
        }

        public CustomerDto InsertCustomer(CustomerDto customer)
        {
            WriteActualMethod();
            return _businessComponent.SaveObject(customer.ConvertToEntity(), customer.Id, true).ConvertToDto();
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return _businessComponent.SaveObject(reservation.ConvertToEntity(),reservation.ReservationNo, true).ConvertToDto();
        }

        public CarDto UpdateCar(CarDto car)
        {
            WriteActualMethod();
            try
            {
                return _businessComponent.SaveObject(car.ConvertToEntity(), car.Id, false).ConvertToDto();
            }

            catch (LocalOptimisticConcurrencyException<Car> ex)
            {
                throw new FaultException("Update Concurrency Error");
            }
        }

        public CustomerDto UpdateCustomer(CustomerDto customer)
        {
            WriteActualMethod();
            try
            {
                return _businessComponent.SaveObject(customer.ConvertToEntity(), customer.Id, false).ConvertToDto();
            }
            catch (LocalOptimisticConcurrencyException<Customer> ex)
            {
                throw new FaultException("Update Concurrency Error");
            }
        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                return _businessComponent.SaveObject(reservation.ConvertToEntity(), reservation.ReservationNo, false).ConvertToDto();
            }

            catch (LocalOptimisticConcurrencyException<Reservation> ex)
            {
                throw new FaultException("Update Concurrency Error");
            }
}

        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }
    }
}