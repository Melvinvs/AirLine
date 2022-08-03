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

        public List<Flight> GetFlights(string from, string to)
        {
            List<Flight> flights = db.Flight.Where(d => d.FromPlace.Contains(from) && d.ToPlace.Contains(to) && d.IsBlocked==0).ToList();
            return flights;
        }

        public Flight BlockFlight(int id)
        {
            Flight obj = db.Flight.Where(f => f.Id == id).FirstOrDefault();

            obj.IsBlocked = 1;
            db.Update(obj);
            db.SaveChanges();

            return obj;
        }

        public Flight adddiscount(int id, int value)
        {
            Flight obj = db.Flight.Where(f => f.Id == id).FirstOrDefault();

            obj.Discount = value;
            db.Update(obj);
            db.SaveChanges();

            return obj;
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
