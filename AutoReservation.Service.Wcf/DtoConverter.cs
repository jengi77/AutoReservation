using CarReservation.Common.DataTransferObjects;
using CarReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarReservation.Service.Wcf
{
    public static class DtoConverter
    {
        #region Car
        private static Car GetCarInstance(CarDto dto)
        {
            if (dto.CarClass == CarClass.Standard) { return new StandardCar(); }
            if (dto.CarClass == CarClass.MidRange) { return new MidRangeCar(); }
            if (dto.CarClass == CarClass.Luxury) { return new LuxuryCar(); }
            throw new ArgumentException("Unknown CarDto implementation.", nameof(dto));
        }
        public static Car ConvertToEntity(this CarDto dto)
        {
            if (dto == null) { return null; }

            Car car = GetCarInstance(dto);
            car.Id = dto.Id;
            car.Brand = dto.Brand;
            car.DailyRate = dto.DailyRate;
            car.RowVersion = dto.RowVersion;

            if (car is LuxuryCar)
            {
                ((LuxuryCar)car).BaseRate = dto.BaseRate;
            }
            return car;
        }
        public static CarDto ConvertToDto(this Car entity)
        {
            if (entity == null) { return null; }

            CarDto dto = new CarDto
            {
                Id = entity.Id,
                Brand = entity.Brand,
                DailyRate = entity.DailyRate,
                RowVersion = entity.RowVersion
            };

            if (entity is StandardCar) { dto.CarClass = CarClass.Standard; }
            if (entity is MidRangeCar) { dto.CarClass = CarClass.MidRange; }
            if (entity is LuxuryCar)
            {
                dto.CarClass = CarClass.Luxury;
                dto.BaseRate = ((LuxuryCar)entity).BaseRate;
            }


            return dto;
        }
        public static List<Car> ConvertToEntities(this IEnumerable<CarDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<CarDto> ConvertToDtos(this IEnumerable<Car> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
        #endregion
        #region Customer
        public static Customer ConvertToEntity(this CustomerDto dto)
        {
            if (dto == null) { return null; }

            return new Customer()
            {
                Id = dto.Id,
                Lastname = dto.Lastname,
                Firstname = dto.Firstname,
                Birthday = dto.Birthday,
                RowVersion = dto.RowVersion
            };
        }
        public static CustomerDto ConvertToDto(this Customer entity)
        {
            if (entity == null) { return null; }

            return new CustomerDto
            {
                Id = entity.Id,
                Lastname = entity.Lastname,
                Firstname = entity.Firstname,
                Birthday = entity.Birthday,
                RowVersion = entity.RowVersion
            };
        }
        public static List<Customer> ConvertToEntities(this IEnumerable<CustomerDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<CustomerDto> ConvertToDtos(this IEnumerable<Customer> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
        #endregion
        #region Reservation
        public static Reservation ConvertToEntity(this ReservationDto dto)
        {
            if (dto == null) { return null; }

            Reservation reservation = new Reservation
            {
                ReservationNo = dto.ReservationNo,
                From = dto.From,
                To = dto.To,
                CarId = dto.Car.Id,
                CustomerId = dto.Customer.Id,
                RowVersion = dto.RowVersion
            };

            return reservation;
        }
        public static ReservationDto ConvertToDto(this Reservation entity)
        {
            if (entity == null) { return null; }

            return new ReservationDto
            {
                ReservationNo = entity.ReservationNo,
                From = entity.From,
                To = entity.To,
                RowVersion = entity.RowVersion,
                Car = ConvertToDto(entity.Car),
                Customer = ConvertToDto(entity.Customer)
            };
        }
        public static List<Reservation> ConvertToEntities(this IEnumerable<ReservationDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<ReservationDto> ConvertToDtos(this IEnumerable<Reservation> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
        #endregion

        private static List<TTarget> ConvertGenericList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(converter).ToList();
        }
    }

}
