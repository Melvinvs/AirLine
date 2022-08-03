using FlightService.Entity;
using FlightService.Models;

namespace FlightService.Entity
{
    public interface IdataRepository
    {
        Flight AddFlight(Flight model);

        List<Flight> GetFlights(string from, string to);

        Flight BlockFlight(int id);

        Flight adddiscount(int id, int value);

        bool AddAirLine(AirLineModel model);

        List<AirLineModel> GetAllAirline();
    }
}
