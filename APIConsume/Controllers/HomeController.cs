using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public async Task<IActionResult> Index()
        {
            List<Reservation> reservationList = new List<Reservation>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/Reservation"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }

            return View(reservationList);
        }

        public ViewResult GetReservation() => View();

        [HttpPost]
        public async Task<IActionResult> GetReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5001/api/Reservation/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }

            return View(reservation);
        }

        public ViewResult AddReservation() => View();

        [HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), 
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"http://localhost:5001/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }

            return View(receivedReservation);
        }
    }
}