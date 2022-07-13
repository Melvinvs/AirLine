#region Included Namespaces
using FlightService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace FlightService.Controllers
{
    #region Flight
    /// <summary>
    /// all flight related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Flight : ControllerBase
    {
        #region AddUpdateFlight
        /// <summary>
        /// add and pdate  flight details
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool AddUpdateFlight(FlightModel Model)
        {
            var status = false;

            return status;
        }
        #endregion

        #region scheduleFlight
        /// <summary>
        /// schedule flight 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool scheduleFlight(FlightModel Model)
        {
            var status = false;

            return status;
        }
        #endregion

        #region AddDiscount
        /// <summary>
        /// AddDiscount
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool AddDiscount(FlightModel Model)
        {
            var status = false;

            return status;
        }
        #endregion
    }
    #endregion
}
