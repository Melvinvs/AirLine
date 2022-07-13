using AirLine.Interface;
using AirLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLine.Repository
{
    public class FlightService : IFlightService
    {
        private List<FlightModel> lst;

        public FlightService()
        {
            lst = new List<FlightModel>
            {
                new FlightModel{FlightNo=1,AirLineName="Indigo"},
                new FlightModel{FlightNo=2,AirLineName="Deccan"}
            };
        }

        public FlightModel GetFlightDetails(int id)
        {
            return lst.Where(f => f.FlightNo == id).FirstOrDefault();
        }
    }
}
