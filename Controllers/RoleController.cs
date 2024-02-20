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
    public class RoleController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44319/api");
        private readonly HttpClient _client;

        public RoleController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RoleModel> roleList = new List<RoleModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                roleList = JsonConvert.DeserializeObject<List<RoleModel>>(data);
            }
            return View(roleList);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            RoleModel role = new RoleModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                role = JsonConvert.DeserializeObject<RoleModel>(data);
            }
            return View(role);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoleModel role)
        {
            try { 
                string data = JsonConvert.SerializeObject(role);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Role/", content).Result;

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
            RoleModel role = new RoleModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/" + id).Result;

            if (response.IsSuccessStatusCode) {
                string data = response.Content.ReadAsStringAsync().Result;
                role = JsonConvert.DeserializeObject<RoleModel>(data); 
                }
                return View(role);
        }
        [HttpPost]
        public IActionResult Edit(RoleModel role)
        {
            try
            {
                string data = JsonConvert.SerializeObject(role);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Role/" + role.RoleId, content).Result;

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
                RoleModel role = new RoleModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    role = JsonConvert.DeserializeObject<RoleModel>(data);
                }
                return View(role);

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
             HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Role/" + id).Result;
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
