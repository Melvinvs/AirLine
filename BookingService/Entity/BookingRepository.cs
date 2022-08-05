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
            return db.Bookings.Where(b => b.Email == email && b.IsCancelled == 0).ToList();
        }

        public int GetBookedTickets(Ticket model)
        {
            return db.Bookings.Count(ticket => 
            ticket.FlightNo == model.FlightNo && 
            ticket.FromPlace== model.FromPlace && 
            ticket.StartTime == model.StartTime && 
            ticket.Seattype == model.Seattype);
        }

        public List<Ticket> SearchByPNR(string PNR)
        {
            return db.Bookings.Where(b => b.PNR == PNR && b.IsCancelled == 0).ToList();
        }

        public Ticket CancelTicket(string PNR)
        {
            Ticket obj = db.Bookings.Where(f => f.PNR == PNR).FirstOrDefault();

            obj.IsCancelled = 1;
            db.Update(obj);
            db.SaveChanges();

            return obj;
        }

        public bool CancelTicketByAirline(string airlinename)
        {
            List<Ticket> obj = db.Bookings.Where(f => f.AirLineName== airlinename).ToList();

            foreach(var ticket in obj)
            {
                ticket.IsCancelled = 1;
            }


            if (obj.Count > 0)
            {
                if (obj.Count ==1)
                    db.Update(obj.First());
                else
                    db.Bookings.UpdateRange(obj);
            }
            
            db.SaveChanges();

            return obj.Count > 0 ? true : false;
        }

        public List<Ticket> GetAllTicketByUserID(int userId)
        {
            return db.Bookings.Where(b => b.CreatedBy == userId).ToList();
        }
    }
}
