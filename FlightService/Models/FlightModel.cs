namespace FlightService.Models
{
    public class FlightModel
    {
        #region FlightNo
        /// <summary>
        /// Flight Number
        /// </summary>
        public string FlightNo { get; set; }
        #endregion

        #region AirLineName
        /// <summary>
        /// AirLineName
        /// </summary>
        public string AirLineName { get; set; }
        #endregion

        #region ContactNo
        /// <summary>
        /// Contact Number
        /// </summary>
        public string ContactNo { get; set; }
        #endregion

        #region ContactAddress
        /// <summary>
        /// Contact Address
        /// </summary>
        public string ContactAddress { get; set; }
        #endregion

        #region FromPlace
        /// <summary>
        /// FromPlace
        /// </summary>
        public string FromPlace { get; set; }
        #endregion

        #region ToPlace
        /// <summary>
        /// ToPlace
        /// </summary>
        public string ToPlace { get; set; }
        #endregion

        #region StartTime
        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime StartTime { get; set; }
        #endregion

        #region EndTime
        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime EndTime { get; set; }
        #endregion

        #region ScheduledType
        /// <summary>
        /// ScheduledType
        /// </summary>
        public int ScheduledType { get; set; }
        #endregion

        #region PlaneNo
        /// <summary>
        /// PlaneNo
        /// </summary>
        public string PlaneNo { get; set; }
        #endregion

        #region BussinesSeatNo
        /// <summary>
        /// BussinesSeatNo
        /// </summary>
        public int BussinesSeatNo { get; set; }
        #endregion

        #region EconomySeatNo
        /// <summary>
        /// EconomySeatNo
        /// </summary>
        public int EconomySeatNo { get; set; }
        #endregion

        #region TicketPrice
        /// <summary>
        /// TicketPrice
        /// </summary>
        public int TicketPrice { get; set; }
        #endregion

        #region ToralRows
        /// <summary>
        /// ToralRows
        /// </summary>
        public int ToralRows { get; set; }
        #endregion

        #region MealType
        /// <summary>
        /// MealType
        /// </summary>
        public int MealType { get; set; }
        #endregion

        #region Discount
        /// <summary>
        /// Discount
        /// </summary>
        public int Discount { get; set; }
        #endregion
    }
}
