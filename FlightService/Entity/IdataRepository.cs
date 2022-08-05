using FlightService.Entity;
using FlightService.Models;

namespace FlightService.Entity
{
    public interface IdataRepository
    {
        Flight AddFlight(Flight model);

        List<Flight> GetFlights(string from, string to, DateTime date);

        AirLineModel BlockAirline(int id);

        bool adddiscount(string name, int value);

        bool AddAirLine(AirLineModel model);

        List<AirLineModel> GetAllAirline();
    }
}
