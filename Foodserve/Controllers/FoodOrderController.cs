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
            orderId = orderId + DateTime.Today.Date.Day.ToString("00") + DateTime.Today.Month.ToString("00") + DateTime.Today.Year.ToString() + "-000";

            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "/FoodOrder/").Result; 

            string data2 = response2.Content.ReadAsStringAsync().Result;
            List<FoodOrderModel> orderList = JsonConvert.DeserializeObject<List<FoodOrderModel>>(data2);
            var todayOrder = orderList.Where(order => order.OrderDate.Date == DateTime.Today.Date).ToList();

            int orderCount = todayOrder.Count + 1;

            orderId = orderId.Substring(0, orderId.Length - orderCount.ToString().Length) + orderCount.ToString();

            ViewData["OrderId"] = orderId;
            TempData["OrderId"] = orderId;

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

            OrderDetailModel orderDetail = new OrderDetailModel();
            HttpResponseMessage response3 = _client.GetAsync(_client.BaseAddress + "/OrderDetail/").Result;
            List<OrderDetailModel> orderDetailList = new List<OrderDetailModel>();

            if (response3.IsSuccessStatusCode)
            {
                string data3 = response3.Content.ReadAsStringAsync().Result;
                orderDetailList = JsonConvert.DeserializeObject<List<OrderDetailModel>>(data3).Where(m => m.OrderId == id).ToList();

            }

            List<FoodModel> Food = new List<FoodModel>();
            HttpResponseMessage response4 = _client.GetAsync(_client.BaseAddress + "/Food/").Result;
            if (response4.IsSuccessStatusCode)
            {
                string FoodData = response4.Content.ReadAsStringAsync().Result;
                Food = JsonConvert.DeserializeObject<List<FoodModel>>(FoodData).ToList();
            }

            List<OrderDetailModel> join = (from detail in orderDetailList
                             join food in Food on detail.FoodId equals food.FoodId
                             select new OrderDetailModel
                             {
                                 OrderId = detail.OrderId,
                                 DetailId = detail.DetailId,
                                 Qty = detail.Qty,
                                 FoodId = food.FoodId,
                                 FoodName = food.FoodName
                             }).ToList(); 

            ViewData["OrderDetails"] = join;
            TempData["OrderId"] = id;

            OrderDetailModel OrderDetail = new OrderDetailModel();
            HttpResponseMessage responses = _client.GetAsync(_client.BaseAddress + "/OrderDetail/total-qty/" + id).Result;

            if (responses.IsSuccessStatusCode)
            {
                string data = responses.Content.ReadAsStringAsync().Result;
                OrderDetail = JsonConvert.DeserializeObject<OrderDetailModel>(data);
                
                ViewBag.Qty = Convert.ToInt32(OrderDetail.totalQty);
            }

            ViewBag.Price = hitungHarga(id);

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

            ViewBag.OrderID = TempData["OrderId"];

            ViewBag.Food = new SelectList(FoodList, "FoodId", "FoodName");  
             

            return View();
        }
        [HttpPost]
        public IActionResult CreateDetail(OrderDetailModel orderDetail)
        {
            try
            {
                FoodOrderModel foodOrder = new FoodOrderModel();
                HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "/FoodOrder/" + orderDetail.OrderId).Result;

                if (response2.IsSuccessStatusCode)
                {
                    string data2 = response2.Content.ReadAsStringAsync().Result;
                    foodOrder = JsonConvert.DeserializeObject<FoodOrderModel>(data2);
                }

                FoodModel food = new FoodModel();
                HttpResponseMessage response3 = _client.GetAsync(_client.BaseAddress + "/Food/" + orderDetail.FoodId).Result;
                if(response3.IsSuccessStatusCode)
                {
                    string data3 = response3.Content.ReadAsStringAsync().Result;
                    food = JsonConvert.DeserializeObject<FoodModel>(data3);
                }

                orderDetail.FoodModel = food;
                orderDetail.OrderModel = foodOrder;

                string data = JsonConvert.SerializeObject(orderDetail);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/OrderDetail/", content).Result;
                 
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Create Success";
                    return RedirectToAction("Edit", new { id = orderDetail.OrderId });
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return RedirectToAction("Edit", new { id = orderDetail.OrderId });
        }

        public int hitungHarga(string orderId)
        {
            int result = 0;
            int temp = 0;
            
            List<OrderDetailModel> OrderDetail = new List<OrderDetailModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/OrderDetail/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                OrderDetail = JsonConvert.DeserializeObject<List<OrderDetailModel>>(data).Where(order => order.OrderId == orderId).ToList();
            }

            foreach (OrderDetailModel item in OrderDetail)
            {
                FoodModel food = new FoodModel();
                HttpResponseMessage responses = _client.GetAsync(_client.BaseAddress + "/Food/" + item.FoodId).Result;
                string data = responses.Content.ReadAsStringAsync().Result;
                food = JsonConvert.DeserializeObject<FoodModel>(data);

                temp =  Convert.ToInt32(food.FoodPrice *item.Qty);

                result += temp;
            } 

            return result;
        }
     }
}
