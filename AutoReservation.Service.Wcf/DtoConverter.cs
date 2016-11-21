using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using CarReservation.Dal.Entities;

namespace CarReservation.Service.Wcf
{
    public static class DtoConverter
    {
        #region Car
        private static Car GetCarInstance(CarDto dto)
        {
            if (dto.CarKlasse == CarClass.Standard) { return new StandardCar(); }
            if (dto.CarKlasse == CarClass.Mittelklasse) { return new MittelklasseCar(); }
            if (dto.CarKlasse == CarClass.Luxusklasse) { return new LuxusklasseCar(); }
            throw new ArgumentException("Unknown CarDto implementation.", nameof(dto));
        }
        public static Car ConvertToEntity(this CarDto dto)
        {
            if (dto == null) { return null; }

            Car Car = GetCarInstance(dto);
            Car.Id = dto.Id;
            Car.Marke = dto.Marke;
            Car.Tagestarif = dto.Tagestarif;
            Car.RowVersion = dto.RowVersion;

            if (Car is LuxusklasseCar)
            {
                ((LuxusklasseCar)Car).Basistarif = dto.Basistarif;
            }
            return Car;
        }
        public static CarDto ConvertToDto(this Car entity)
        {
            if (entity == null) { return null; }

            CarDto dto = new CarDto
            {
                Id = entity.Id,
                Marke = entity.Marke,
                Tagestarif = entity.Tagestarif,
                RowVersion = entity.RowVersion
            };

            if (entity is StandardCar) { dto.CarKlasse = CarKlasse.Standard; }
            if (entity is MittelklasseCar) { dto.CarKlasse = CarKlasse.Mittelklasse; }
            if (entity is LuxusklasseCar)
            {
                dto.CarKlasse = CarKlasse.Luxusklasse;
                dto.Basistarif = ((LuxusklasseCar)entity).Basistarif;
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
        #region Kunde
        public static Kunde ConvertToEntity(this KundeDto dto)
        {
            if (dto == null) { return null; }

            return new Kunde
            {
                Id = dto.Id,
                Nachname = dto.Nachname,
                Vorname = dto.Vorname,
                Geburtsdatum = dto.Geburtsdatum,
                RowVersion = dto.RowVersion
            };
        }
        public static KundeDto ConvertToDto(this Kunde entity)
        {
            if (entity == null) { return null; }

            return new KundeDto
            {
                Id = entity.Id,
                Nachname = entity.Nachname,
                Vorname = entity.Vorname,
                Geburtsdatum = entity.Geburtsdatum,
                RowVersion = entity.RowVersion
            };
        }
        public static List<Kunde> ConvertToEntities(this IEnumerable<KundeDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<KundeDto> ConvertToDtos(this IEnumerable<Kunde> entities)
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
                ReservationsNr = dto.ReservationsNr,
                Von = dto.Von,
                Bis = dto.Bis,
                CarId = dto.Car.Id,
                KundeId = dto.Kunde.Id,
                RowVersion = dto.RowVersion
            };

            return reservation;
        }
        public static ReservationDto ConvertToDto(this Reservation entity)
        {
            if (entity == null) { return null; }

            return new ReservationDto
            {
                ReservationsNr = entity.ReservationsNr,
                Von = entity.Von,
                Bis = entity.Bis,
                RowVersion = entity.RowVersion,
                Car = ConvertToDto(entity.Car),
                Kunde = ConvertToDto(entity.Kunde)
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
