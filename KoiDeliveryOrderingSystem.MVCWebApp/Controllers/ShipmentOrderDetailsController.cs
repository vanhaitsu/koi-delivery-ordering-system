using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiDeliveryOrderingSystem.Common;
using Newtonsoft.Json;
using KoiDeliveryOrderingSystem.MVCWebApp.Base;
using AnimalType = KoiDeliveryOrderingSystem.MVCWebApp.Models.AnimalType;
//using ShipmentOrderDetail = KoiDeliveryOrderingSystem.MVCWebApp.Models.ShipmentOrderDetail;
using ShipmentOrder = KoiDeliveryOrderingSystem.MVCWebApp.Models.ShipmentOrder;
using KoiDeliveryOrderingSystem.MVCWebApp.Models;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
    public class ShipmentOrderDetailsController : Controller
    {
        // GET: ShipmentOrderDetails
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetail"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<ShipmentOrderDetail>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<ShipmentOrderDetail>());
        }

        // GET: ShipmentOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetail/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ShipmentOrderDetail>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ShipmentOrderDetail());
        }

        // GET: ShipmentOrderDetails/Create
        public async Task<IActionResult> CreateAsync()
        {
            var orders = new List<ShipmentOrder>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            orders = JsonConvert.DeserializeObject<List<ShipmentOrder>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["OrderId"] = new SelectList(orders, "OrderId", "OrderId");

            var animalTypes = new List<AnimalType>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Animaltypes"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            animalTypes = JsonConvert.DeserializeObject<List<AnimalType>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["AnimalTypeId"] = new SelectList(animalTypes, "AnimalTypeId", "Description");
            return View();
        }

        // POST: ShipmentOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipmentOrderDetailId,ShipmentOrderId,AnimalTypeId,AdditionalServices,Fee,Status,Weight,Length,DateOfEntry,Gender,Color,HealthStatus,Age,Achievement,Origin,ImageUrl,Description")] ShipmentOrderDetail shipmentOrderDetail)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "ShipmentOrderDetail/", shipmentOrderDetail))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var animalTypes = new List<AnimalType>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Animaltypes"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                animalTypes = JsonConvert.DeserializeObject<List<AnimalType>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["AnimalTypeId"] = new SelectList(animalTypes, "AnimalTypeId", "Description", shipmentOrderDetail.AnimalTypeId);
                return View(shipmentOrderDetail);
            }
        }

        //GET: ShipmentOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShipmentOrderDetail shipmentOrderDetail = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetail/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            shipmentOrderDetail = JsonConvert.DeserializeObject<ShipmentOrderDetail>(result.Data.ToString());
                        }
                    }
                }
            }

            if (shipmentOrderDetail == null)
            {
                return NotFound();
            }


            // Get dropdown data ------------------------------------------------------------
            var orders = new List<ShipmentOrder>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            orders = JsonConvert.DeserializeObject<List<ShipmentOrder>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["OrderId"] = new SelectList(orders, "OrderId", "OrderId");

            var animalTypes = new List<AnimalType>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Animaltypes"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            animalTypes = JsonConvert.DeserializeObject<List<AnimalType>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["AnimalTypeId"] = new SelectList(animalTypes, "AnimalTypeId", "Description");
            return View(shipmentOrderDetail);
        }

        // POST: ShipmentOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipmentOrderDetailId,ShipmentOrderId,AnimalTypeId,AdditionalServices,Fee,Status,Weight,Length,DateOfEntry,Gender,Color,HealthStatus,Age,Achievement,Origin,ImageUrl,Description")] ShipmentOrderDetail shipmentOrderDetail)
        {
            if (id != shipmentOrderDetail.ShipmentOrderDetailId)
            {
                return NotFound();
            }

            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "ShipmentOrderDetail/" + id, shipmentOrderDetail))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var orders = new List<ShipmentOrder>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrders"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                orders = JsonConvert.DeserializeObject<List<ShipmentOrder>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["OrderId"] = new SelectList(orders, "OrderId", "OrderId");

                var animalTypes = new List<AnimalType>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Animaltypes"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                animalTypes = JsonConvert.DeserializeObject<List<AnimalType>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["AnimalTypeId"] = new SelectList(animalTypes, "AnimalTypeId", "Description");
                return View(shipmentOrderDetail);
            }
        }

        // GET: ShipmentOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShipmentOrderDetail shipmentOrderDetail = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "ShipmentOrderDetail/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            shipmentOrderDetail = JsonConvert.DeserializeObject<ShipmentOrderDetail>(result.Data.ToString());
                        }
                    }
                }
            }

            if (shipmentOrderDetail == null)
            {
                return NotFound();
            }

            return View(shipmentOrderDetail);
        }

        // POST: ShipmentOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "ShipmentOrderDetail/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var animalTypes = new List<AnimalType>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Animaltypes"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                animalTypes = JsonConvert.DeserializeObject<List<AnimalType>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["AnimalTypeId"] = new SelectList(animalTypes, "AnimalTypeId", "Description", id);
                return View();
            }
        }
    }
}
