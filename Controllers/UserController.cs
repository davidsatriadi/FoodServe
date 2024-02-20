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
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44319/api");
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserModel> userList = new List<UserModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<List<UserModel>>(data);
            }
            return View(userList);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            UserModel user = new UserModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data);
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<RoleModel> userList = new List<RoleModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/").Result;

            string data = response.Content.ReadAsStringAsync().Result;
            userList = JsonConvert.DeserializeObject<List<RoleModel>>(data);

            List<string> isActive = new List<string>();
            isActive.Add("Y");
            isActive.Add("N");

            ViewBag.Role = new SelectList(userList, "RoleId", "RoleName");
            ViewBag.isActive = new SelectList(isActive);

            return View();
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/User/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Create Success";
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

        [HttpGet]
        public IActionResult Edit(string id)
        {


            List<RoleModel> userList = new List<RoleModel>();
            HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Role/").Result;

            string data1 = response1.Content.ReadAsStringAsync().Result;
            userList = JsonConvert.DeserializeObject<List<RoleModel>>(data1);

            List<string> isActive = new List<string>();
            isActive.Add("Y");
            isActive.Add("N");

            ViewBag.Role = new SelectList(userList, "RoleId", "RoleName");
            ViewBag.isActive = new SelectList(isActive);

            UserModel user = new UserModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data);
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/User/" + user.UserId, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Edit Success";
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

        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                UserModel user = new UserModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UserModel>(data);
                }
                return View(user);

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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/User/" + id).Result;
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