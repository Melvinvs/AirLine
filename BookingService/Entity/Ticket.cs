using System.ComponentModel.DataAnnotations;

namespace BookingService.Entity
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }


        #region FlightNo
        /// <summary>
        /// Flight Number
        /// </summary>
        public string FlightNo { get; set; }
        #endregion

        public string Name { get; set; }
        
        #region AirLineName
        /// <summary>
        /// AirLineName
        /// </summary>
        public string AirLineName { get; set; }
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

        #region TicketPrice
        /// <summary>
        /// TicketPrice
        /// </summary>
        public int TicketPrice { get; set; }
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

        public string seatNo { get; set; }

        public string Email { get; set; }

        public string PNR { get; set; }

        public int Seattype { get; set; }

        public int IsCancelled { get; set; }

        public int Age { get; set; }

        public string? Gender { get; set; }
        public int CreatedBy { get; set; }
    }
}
