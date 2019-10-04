using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace APIControllers.Models
{
    public class Repository : IRepository
    {
        private Dictionary<int, Reservation> _items;

        public Repository()
        {
            _items = new Dictionary<int, Reservation>();
            new List<Reservation>
            {
                new Reservation {Id = 1, Name = "Ankit", StartLocation = "New York", EndLocation = "Beijing"},
                new Reservation {Id = 2, Name = "Bobby", StartLocation = "New Jersey", EndLocation = "Boston"},
                new Reservation {Id = 3, Name = "Jacky", StartLocation = "London", EndLocation = "Paris"}
            }.ForEach(r => AddReservation(r));
        }

        public Reservation this[int id] => _items.ContainsKey(id) ? _items[id] : null;

        public IEnumerable<Reservation> Reservations => _items.Values;

        public Reservation AddReservation(Reservation reservation)
        {
            if (reservation.Id == 0)
            {
                int key = _items.Count;
                while (_items.ContainsKey(key))
                {
                    key++;
                }

                reservation.Id = key;
            }

            _items[reservation.Id] = reservation;
            return reservation;
        }

        public void DeleteReservation(int id)
        {
            _items.Remove(id);
        }

        public Reservation UpdateReservation(Reservation reservation) => AddReservation(reservation);
    }
}