namespace BookingService.Models
{
    public class TicketModel
    {
        public int id { get; set; }


        #region FlightNo
        /// <summary>
        /// Flight Number
        /// </summary>
        public string flightNo { get; set; }
        #endregion

        public string name { get; set; }

        #region AirLineName
        /// <summary>
        /// AirLineName
        /// </summary>
        public string airLineName { get; set; }
        #endregion

        #region FromPlace
        /// <summary>
        /// FromPlace
        /// </summary>
        public string fromPlace { get; set; }
        #endregion

        #region ToPlace
        /// <summary>
        /// ToPlace
        /// </summary>
        public string toPlace { get; set; }
        #endregion

        #region StartTime
        /// <summary>
        /// StartTime
        /// </summary>
        public string startTime { get; set; }
        #endregion

        #region EndTime
        /// <summary>
        /// EndTime
        /// </summary>
        public string endTime { get; set; }
        #endregion

        #region TicketPrice
        /// <summary>
        /// TicketPrice
        /// </summary>
        public int ticketPrice { get; set; }
        #endregion

        #region MealType
        /// <summary>
        /// MealType
        /// </summary>
        public int mealType { get; set; }
        #endregion

        #region Discount
        /// <summary>
        /// Discount
        /// </summary>
        public int discount { get; set; }
        #endregion

        public string seatNo { get; set; }

        public string email { get; set; }

        public string pnr { get; set; }

        public int totalBSeats { get; set; }

        public int totalESeats { get; set; }

        public int seatType { get; set; }

        public int isCancelled { get; set; }

        public int age { get; set; }

        public string? gender { get; set; }

        public int isBooked { get; set; }

        public int createdBy { get; set; }

    }
}
