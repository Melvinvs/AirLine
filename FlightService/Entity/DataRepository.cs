using FlightService.Entity;
using FlightService.Models;

namespace FlightService.Entity
{
    public class DataRepository : IdataRepository
    {
        private readonly FlightDbContext db;

        public DataRepository(FlightDbContext db)
        {
            this.db = db;   
        }

        public Flight AddFlight(Flight model)
        {
            db.Flight.Add(model);
            db.SaveChanges();
            return model;
        }

        public List<Flight> GetFlights(string from, string to, DateTime date)
        {
            List<Flight> flights = db.Flight.Where(d => d.FromPlace.Contains(from) && d.ToPlace.Contains(to) && d.IsBlocked==0 && d.StartTime.Date == date.Date).ToList();
            return flights;
        }

        public AirLineModel BlockAirline(int id)
        {
            AirLineModel obj = db.AirLine.Where(f => f.ID == id).FirstOrDefault();

            obj.IsBlocked = true;
            db.Update(obj);
            db.SaveChanges();

            return obj;
        }

        public bool adddiscount(string name, int value)
        {
            List<Flight> obj = db.Flight.Where(f => f.AirLineName == name).ToList();

            foreach (var flight in obj)
            {
                flight.Discount = value;
            }

            //db.Update(obj);
            db.Flight.UpdateRange(obj);
            db.SaveChanges();
            
            return true;
        }

        public bool AddAirLine(AirLineModel model)
        {
            db.AirLine.Add(model);
            db.SaveChanges();

            return true;
        }

        public List<AirLineModel> GetAllAirline()
        {
            return db.AirLine.ToList();
        }
    }
}
