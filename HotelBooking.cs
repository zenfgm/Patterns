using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{
    interface IBookingState
    {
        void SelectRoom();
        void ConfirmBooking();
        void Pay();
        void CancelBooking();
    }

    class BookingContext
    {
        public IBookingState State { get; set; }

        public BookingContext() => State = new IdleState(this);
    }

    class IdleState : IBookingState
    {
        private readonly BookingContext _context;

        public IdleState(BookingContext context) => _context = context;

        public void SelectRoom()
        {
            Console.WriteLine("Room selected. Moving to RoomSelected state.");
            _context.State = new RoomSelectedState(_context);
        }

        public void ConfirmBooking() => Console.WriteLine("Cannot confirm. No room selected.");
        public void Pay() => Console.WriteLine("Cannot pay. No booking confirmed.");
        public void CancelBooking() => Console.WriteLine("Nothing to cancel.");
    }

    class RoomSelectedState : IBookingState
    {
        private readonly BookingContext _context;

        public RoomSelectedState(BookingContext context) => _context = context;

        public void SelectRoom() => Console.WriteLine("Room already selected.");
        public void ConfirmBooking()
        {
            Console.WriteLine("Booking confirmed. Moving to BookingConfirmed state.");
            _context.State = new BookingConfirmedState(_context);
        }

        public void Pay() => Console.WriteLine("Cannot pay. Booking not confirmed.");
        public void CancelBooking()
        {
            Console.WriteLine("Booking cancelled. Returning to Idle state.");
            _context.State = new IdleState(_context);
        }
    }
    class BookingConfirmedState : IBookingState
    {
        private readonly BookingContext _context;

        public BookingConfirmedState(BookingContext context) => _context = context;

        public void SelectRoom() => Console.WriteLine("Cannot select room. Booking already confirmed.");
        public void ConfirmBooking() => Console.WriteLine("Booking already confirmed.");
        public void Pay()
        {
            Console.WriteLine("Booking paid. Moving to Paid state.");
            _context.State = new PaidState(_context);
        }

        public void CancelBooking()
        {
            Console.WriteLine("Booking cancelled. Returning to Idle state.");
            _context.State = new IdleState(_context);
        }
    }
    class PaidState : IBookingState
    {
        private readonly BookingContext _context;

        public PaidState(BookingContext context) => _context = context;

        public void SelectRoom() => Console.WriteLine("Cannot select room. Booking already paid.");
        public void ConfirmBooking() => Console.WriteLine("Booking already confirmed.");
        public void Pay() => Console.WriteLine("Booking already paid.");
        public void CancelBooking() => Console.WriteLine("Cannot cancel. Booking already paid.");
    }

    class Program
    {
        static void Main()
        {
            var booking = new BookingContext();

            booking.State.SelectRoom();
            booking.State.ConfirmBooking();
            booking.State.Pay();
            booking.State.CancelBooking();
        }
    }
}
