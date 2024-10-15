using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.MVCWebApp.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiDeliveryOrderingSystem.MVCWebApp.Controllers
{
  public class PricingPolicyController : Controller
  {
    // GET: PricingPolicy
    public async Task<IActionResult> Index()
    {
      try
      {
        using (HttpClient httpClient = new HttpClient())
        {
          using (HttpResponseMessage response = await httpClient.GetAsync(Const.APIEndPoint + "PricingPolicy"))
          {
            if (response.IsSuccessStatusCode)
            {
              string content = await response.Content.ReadAsStringAsync();
              BusinessResult? result = JsonConvert.DeserializeObject<BusinessResult>(content);
              if (result != null && result.Data != null)
              {
                List<Models.PricingPolicy>? data = JsonConvert.DeserializeObject<List<Models.PricingPolicy>>(result.Data.ToString());
                return View(data);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Fetching data error: {ex.Message}");
      }
      return View(new List<Models.PricingPolicy>());
    }

    // GET: PricingPolicy/Details/:id
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      using (HttpClient httpClient = new HttpClient())
      {
        using (HttpResponseMessage response = await httpClient.GetAsync(Const.APIEndPoint + $"PricingPolicy/{id}"))
        {
          if (response.IsSuccessStatusCode)
          {
            string content = await response.Content.ReadAsStringAsync();
            BusinessResult? result = JsonConvert.DeserializeObject<BusinessResult>(content);
            if (result != null && result.Data != null)
            {
              Models.PricingPolicy? data = JsonConvert.DeserializeObject<Models.PricingPolicy>(result.Data.ToString());
              return View(data);
            }
          }
        }
      }
      return View(new Models.PricingPolicy());
    }

    // GET: PricingPolicyController/Create
    public async Task<ActionResult> CreateAsync()
    {
      List<Models.PricingPolicy>? pricingPolicies = new List<Models.PricingPolicy>();
      using (HttpClient httpClient = new HttpClient())
      {
        using (HttpResponseMessage response = await httpClient.GetAsync(Const.APIEndPoint + "PricingPolicy"))
        {
          if (response.IsSuccessStatusCode)
          {
            string content = await response.Content.ReadAsStringAsync();
            BusinessResult? result = JsonConvert.DeserializeObject<BusinessResult>(content);
            if (result != null && result.Data != null)
            {
              pricingPolicies = JsonConvert.DeserializeObject<List<Models.PricingPolicy>>(result.Data.ToString());
            }
          }
        }
      }
      ViewData["PricingId"] = new SelectList(pricingPolicies, "PricingId", "PricingId");
      return View();
    }

    // POST: PricingPolicyController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateConfirmed([Bind("PricingId,WeightRange,ShippingMethod,BasePrice,AdditionalServicePrice,InsuranceFee,CustomsFee,Currency,EffectiveDate,ExpiryDate")] Models.PricingPolicy pricingPolicy)
    {
      bool saveStatus = false;
      if (ModelState.IsValid)
      {
        using (HttpClient httpClient = new HttpClient())
        {
          using (HttpResponseMessage response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "PricingPolicy/", pricingPolicy))
          {
            if (response.IsSuccessStatusCode)
            {
              string content = await response.Content.ReadAsStringAsync();
              BusinessResult result = JsonConvert.DeserializeObject<BusinessResult>(content);
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
        return View(pricingPolicy);

      }
    }

    // GET: PricingPolicyController/Edit/:id
    public async Task<ActionResult> EditAsync(int id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Models.PricingPolicy pricingPolicy = null;
      using (HttpClient httpClient = new HttpClient())
      {
        using (HttpResponseMessage response = await httpClient.GetAsync(Const.APIEndPoint + $"PricingPolicy/{id}"))
        {
          if (response.IsSuccessStatusCode)
          {
            string content = await response.Content.ReadAsStringAsync();
            BusinessResult? result = JsonConvert.DeserializeObject<BusinessResult>(content);
            if (result != null && result.Data != null)
            {
              pricingPolicy = JsonConvert.DeserializeObject<Models.PricingPolicy>(result.Data.ToString());
            }
          }
        }
      }

      if (pricingPolicy == null)
      {
        return NotFound();
      }

      return View(pricingPolicy);
    }

    // POST: PricingPolicyController/Edit/:id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditConfirmed(int id, [Bind("PricingId,WeightRange,ShippingMethod,BasePrice,AdditionalServicePrice,InsuranceFee,CustomsFee,Currency,EffectiveDate,ExpiryDate")] Models.PricingPolicy pricingPolicy)
    {
      if (id != pricingPolicy.PricingId)
      {
        return NotFound();
      }

      bool saveStatus = false;
      if (ModelState.IsValid)
      {
        using (HttpClient httpClient = new HttpClient())
        {
          using (HttpResponseMessage response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + $"PricingPolicy/{id}", pricingPolicy))
          {
            if (response.IsSuccessStatusCode)
            {
              string content = await response.Content.ReadAsStringAsync();
              BusinessResult? result = JsonConvert.DeserializeObject<BusinessResult>(content);
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
        return View(pricingPolicy);
      }
    }

    // GET: PricingPolicy/Delete/:id
    public async Task<IActionResult> DeleteAsync(int id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Models.PricingPolicy pricingPolicy = null;
      using (HttpClient httpClient = new HttpClient())
      {
        using (HttpResponseMessage response = await httpClient.GetAsync(Const.APIEndPoint + $"PricingPolicy/{id}"))
        {
          if (response.IsSuccessStatusCode)
          {
            string content = await response.Content.ReadAsStringAsync();
            BusinessResult? result = JsonConvert.DeserializeObject<BusinessResult>(content);
            if (result != null && result.Data != null)
            {
              pricingPolicy = JsonConvert.DeserializeObject<Models.PricingPolicy>(result.Data.ToString());
            }
          }
        }
      }

      if (pricingPolicy == null)
      {
        return NotFound();
      }

      return View(pricingPolicy);
    }

    // POST: PricingPolicyController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      bool saveStatus = false;
      if (ModelState.IsValid)
      {
        using (HttpClient httpClient = new HttpClient())
        {
          using (HttpResponseMessage response = await httpClient.DeleteAsync(Const.APIEndPoint + $"PricingPolicy/{id}"))
          {
            if (response.IsSuccessStatusCode)
            {
              string content = await response.Content.ReadAsStringAsync();
              BusinessResult? result = JsonConvert.DeserializeObject<BusinessResult>(content);
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
        return View();
      }
    }
  }
}
