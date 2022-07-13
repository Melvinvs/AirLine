using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLine.Interface
{
    public interface IFlightService
    {
        Models.FlightModel GetFlightDetails(int id);
    }
}
