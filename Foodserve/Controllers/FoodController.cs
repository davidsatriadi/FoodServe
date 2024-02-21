using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodServeAPI.Models;
using System.Net.Http;
using FoodServe.Models;
using Newtonsoft.Json;
using System.Text;

namespace FoodServe.Controllers
{
    public class FoodController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44319/api");
        private readonly HttpClient _client;

        public FoodController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<FoodModel> foodList = new List<FoodModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Food/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                foodList = JsonConvert.DeserializeObject<List<FoodModel>>(data);
            }
            return View(foodList);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            FoodModel food = new FoodModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Food/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                food = JsonConvert.DeserializeObject<FoodModel>(data);
            }
            return View(food);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FoodModel food)
        {
            try { 
                string data = JsonConvert.SerializeObject(food);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Food/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Create Success";
                    return RedirectToAction("Index");
                }
            } 
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            FoodModel food = new FoodModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Food/" + id).Result;

            if (response.IsSuccessStatusCode) {
                string data = response.Content.ReadAsStringAsync().Result;
                food = JsonConvert.DeserializeObject<FoodModel>(data); 
                }
                return View(food);
        }
        [HttpPost]
        public IActionResult Edit(FoodModel food)
        {
            try
            {
                string data = JsonConvert.SerializeObject(food);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Food/" + food.FoodId, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Edit Success";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                FoodModel food = new FoodModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Food/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    food = JsonConvert.DeserializeObject<FoodModel>(data);
                }
                return View(food);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult deleteConfirmed(string id)
        {
            try
            {
             HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Food/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Delete Success";
                return RedirectToAction("Index");
            }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}
