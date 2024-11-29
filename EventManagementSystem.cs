using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    class User
    {
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }

        public void Register()
        {
            Console.WriteLine($"{Name} has registered successfully.");
        }

        public void ViewEvents(List<Event> events)
        {
            Console.WriteLine($"{Name} is viewing available events:");
            foreach (var ev in events)
            {
                Console.WriteLine($"- {ev.Title}");
            }
        }

        public void OrderEvent(Event ev)
        {
            Console.WriteLine($"{Name} has ordered the event: {ev.Title}");
        }

        public void CancelBooking(Event ev)
        {
            Console.WriteLine($"{Name} has canceled the booking for: {ev.Title}");
        }

        public void Contribute(Event ev, string contribution)
        {
            Console.WriteLine($"{Name} has contributed to {ev.Title}: {contribution}");
        }
    }

    class Admin
    {
        public string Name { get; set; }

        public Admin(string name)
        {
            Name = name;
        }

        public void ViewAllBookings(List<Event> events)
        {
            Console.WriteLine($"{Name} is viewing all bookings:");
            foreach (var ev in events)
            {
                Console.WriteLine($"Event: {ev.Title}, Booked by: {string.Join(", ", ev.BookedUsers)}");
            }
        }

        public void ManageBooking(Event ev, string action, User user = null)
        {
            switch (action.ToLower())
            {
                case "add":
                    ev.BookedUsers.Add(user.Name);
                    Console.WriteLine($"Admin added {user.Name} to the event: {ev.Title}");
                    break;
                case "remove":
                    ev.BookedUsers.Remove(user.Name);
                    Console.WriteLine($"Admin removed {user.Name} from the event: {ev.Title}");
                    break;
                case "edit":
                    Console.WriteLine($"Admin edited details for the event: {ev.Title}");
                    break;
                default:
                    Console.WriteLine("Invalid action.");
                    break;
            }
        }
    }

    class Event
    {
        public string Title { get; set; }
        public List<string> BookedUsers { get; set; } = new List<string>();

        public Event(string title)
        {
            Title = title;
        }

        public void BookEvent(User user)
        {
            BookedUsers.Add(user.Name);
            Console.WriteLine($"{user.Name} booked the event: {Title}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var event1 = new Event("Music Concert");
            var event2 = new Event("Tech Conference");

            var events = new List<Event> { event1, event2 };

            var guest = new User("John");
            guest.Register();
            guest.ViewEvents(events);
            guest.OrderEvent(event1);
            guest.CancelBooking(event1);
            guest.Contribute(event2, "Sponsorship");

            var admin = new Admin("Admin1");
            admin.ViewAllBookings(events);
            admin.ManageBooking(event1, "add", guest);
            admin.ManageBooking(event1, "remove", guest);
        }
    }
}