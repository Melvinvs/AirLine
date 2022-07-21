using BookingService.Entity;

namespace BookingService.Entity
{
    public interface IBookingRepository
    {
        Ticket AddTicket(Ticket model);

        List<Ticket> GetBookings(string email);

        int GetBookedTickets(Ticket model);

        Ticket SearchByPNR(string PNR);

        Ticket CancelTicket(string PNR);
    }
}
