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
    public class FoodOrderController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44319/api");
        private readonly HttpClient _client;

        public FoodOrderController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<FoodOrderModel> FoodOrderModel = new List<FoodOrderModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/FoodOrder/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                FoodOrderModel = JsonConvert.DeserializeObject<List<FoodOrderModel>>(data);
            }
            return View(FoodOrderModel);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            FoodOrderModel foodOrder = new FoodOrderModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/FoodOrder/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                foodOrder = JsonConvert.DeserializeObject<FoodOrderModel>(data);
            }
            return View(foodOrder);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<UserModel> userList = new List<UserModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/").Result;

            string data = response.Content.ReadAsStringAsync().Result;
            userList = JsonConvert.DeserializeObject<List<UserModel>>(data);

            List<string> isClose = new List<string>();
            isClose.Add("N");
            isClose.Add("Y");

            ViewBag.User = new SelectList(userList, "UserId", "Name");
            ViewBag.IsClose = new SelectList(isClose);

            string orderId = "ABC";
            orderId = orderId + DateTime.Today.Date.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString() + "-000";

            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "/FoodOrder/").Result; 

            string data2 = response2.Content.ReadAsStringAsync().Result;
            List<FoodOrderModel> orderList = JsonConvert.DeserializeObject<List<FoodOrderModel>>(data2);
            int orderCount = orderList.Count + 1;

            orderId = orderId.Substring(0, orderId.Length - orderCount.ToString().Length) + orderCount.ToString();

            ViewData["OrderId"] = orderId;

            return View();
        }

        [HttpPost]
        public IActionResult Create(FoodOrderModel foodOrder)
        {
            try
            {
                string data = JsonConvert.SerializeObject(foodOrder);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/FoodOrder/", content).Result;

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
            List<UserModel> userList = new List<UserModel>();
            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "/User/").Result;

            string data2 = response2.Content.ReadAsStringAsync().Result;
            userList = JsonConvert.DeserializeObject<List<UserModel>>(data2);

            List<string> isClose = new List<string>();
            isClose.Add("N");
            isClose.Add("Y");

            ViewBag.User = new SelectList(userList, "UserId", "Name");
            ViewBag.IsClose = new SelectList(isClose);

            FoodOrderModel foodOrder = new FoodOrderModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/FoodOrder/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                foodOrder = JsonConvert.DeserializeObject<FoodOrderModel>(data);
            }
            return View(foodOrder);
        }
        [HttpPost]
        public IActionResult Edit(FoodOrderModel foodOrder)
        {
            try
            {
                string data = JsonConvert.SerializeObject(foodOrder);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/FoodOrder/" + foodOrder.OrderId, content).Result;

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
                FoodOrderModel foodOrder = new FoodOrderModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/FoodOrder/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    foodOrder = JsonConvert.DeserializeObject<FoodOrderModel>(data);
                }
                return View(foodOrder);

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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/FoodOrder/" + id).Result;
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
         
        public IActionResult Detail()
        {
            return PartialView("Detail", ViewData["OrderId"]); 
        }

        [HttpGet]
        public IActionResult Detail(string orderId)
        {
            List<OrderDetailModel> OrderDetail = new List<OrderDetailModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/OrderDetail/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                OrderDetail = JsonConvert.DeserializeObject<List<OrderDetailModel>>(data).Where(m => m.OrderId == orderId).ToList();
            }
            return PartialView(OrderDetail);
        }

        [HttpGet]
        public IActionResult CreateDetail()
        {
            List<FoodModel> FoodList = new List<FoodModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Food/").Result;

            string data = response.Content.ReadAsStringAsync().Result;
            FoodList = JsonConvert.DeserializeObject<List<FoodModel>>(data);

            ViewBag.Food = new SelectList(FoodList, "food_id", "food_name");  
             

            return View();
        }
    }
}
