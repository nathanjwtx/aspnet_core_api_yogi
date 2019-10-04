using System.Collections.Generic;
using APIControllers.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIControllers.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository _repository;
        //Constructor
        public ReservationController(IRepository repo) => _repository = repo;

        [HttpGet]
        public IEnumerable<Reservation> Get() => _repository.Reservations;

        [HttpGet("{id}")]
        public Reservation Get(int id) => _repository[id];

        [HttpPost]
        public Reservation Post([FromBody] Reservation reservation)
        {
            Reservation res = new Reservation
            {
                Name = reservation.Name,
                StartLocation = reservation.StartLocation,
                EndLocation = reservation.EndLocation
            };
            _repository.AddReservation(res);
            return res;
        }

        [HttpPut]
        public Reservation Put([FromBody] Reservation reservation) => _repository.UpdateReservation(reservation);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Reservation> patch)
        {
            Reservation reservation = Get(id);
            if (reservation != null)
            {
                patch.ApplyTo(reservation);
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _repository.DeleteReservation(id);
    }
}