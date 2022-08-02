using BookingService.Entity;

namespace BookingService.Entity
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext db;

        public BookingRepository(BookingDbContext db)
        {
            this.db = db;   
        }

        public Ticket AddTicket(Ticket model)
        {
            db.Bookings.Add(model);
            db.SaveChanges();
            return model;
        }

        public List<Ticket> GetBookings(string email)
        {
            return db.Bookings.Where(b => b.Email == email).ToList();
        }

        public int GetBookedTickets(Ticket model)
        {
            return db.Bookings.Count(ticket => 
            ticket.FlightNo == model.FlightNo && 
            ticket.FromPlace== model.FromPlace && 
            ticket.StartTime == model.StartTime && 
            ticket.Seattype == model.Seattype);
        }

        public Ticket SearchByPNR(string PNR)
        {
            return db.Bookings.Where(b => b.PNR == PNR && b.IsCancelled == 0).FirstOrDefault();
        }

        public Ticket CancelTicket(string PNR)
        {
            Ticket obj = db.Bookings.Where(f => f.PNR == PNR).FirstOrDefault();

            obj.IsCancelled = 1;
            db.Update(obj);
            db.SaveChanges();

            return obj;
        }

        public bool CancelTicketByFlightID(Ticket model)
        {
            List<Ticket> obj = db.Bookings.Where(f => f.FlightNo == model.FlightNo && f.FromPlace == model.FromPlace && f.StartTime == model.StartTime).ToList();

            foreach(var ticket in obj)
            {
                ticket.IsCancelled = 1;
            }

            //db.Update(obj);
            db.Bookings.UpdateRange(obj);
            db.SaveChanges();

            return obj.Count > 0 ? true : false;
        }
    }
}
