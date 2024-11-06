using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class HotelFacade
    {
        private RoomBookingSystem _roomBookingSystem;
        private CleaningService _cleaningService;
        private RestarauntSystem _restarauntSystem;
        private Notification _notification;
        public HotelFacade()
        {
            _roomBookingSystem = new RoomBookingSystem();
            _cleaningService = new CleaningService();
            _restarauntSystem = new RestarauntSystem();
            _notification = new Notification();
        }

        public void BookRoomWithServices(DateTime startDay, DateTime endDay, int people, string typeBooking)
        {
            _roomBookingSystem.book(startDay, endDay, people, typeBooking);
            _cleaningService.notificationCleaning(people, "Уборка запланирована");
            _restarauntSystem.notificationKitchen(people, 101);
            _notification.sendNotification("Бронирование номера с услугами выполнено.");
        }

        public void OrganizeEventWithRoomBooking(DateTime eventDate, int participants, string equipment)
        {
            _roomBookingSystem.book(eventDate, eventDate.AddDays(1), participants, "Мероприятие");
            Console.WriteLine($"Организация мероприятия с заказом оборудования: {equipment}.");
            _notification.sendNotification("Мероприятие организовано.");
        }

        public void BookRestaurantTableWithTaxi(int people, DateTime time)
        {
            _restarauntSystem.bookTable(people, time);
            Console.WriteLine("Такси заказано для поездки в ресторан.");
            _notification.sendNotification("Забронирован стол в ресторане с вызовом такси.");
        }
    }
}
