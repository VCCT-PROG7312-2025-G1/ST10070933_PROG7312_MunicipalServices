using Microsoft.AspNetCore.Mvc;
using ST10070933_PROG7312_MunicipalServices.Models;
using ST10070933_PROG7312_MunicipalServices.Services;
using System;
using System.Linq;

namespace ST10070933_PROG7312_MunicipalServices.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly IDataService _dataService;

        public ServiceRequestsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // ServiceRequests
        public IActionResult Index(string sortBy = "default")
        {
            List<ServiceRequest> requests;

            switch (sortBy)
            {
                case "priority":
                    requests = _dataService.RequestManager.GetAllRequestsSortedByPriority();
                    ViewBag.SortMethod = "Displaying requests sorted by priority level";
                    break;
                case "id":
                    requests = _dataService.RequestManager.GetAllRequestsSortedById();
                    ViewBag.SortMethod = "Displaying requests sorted by ID number";
                    break;
                case "urgent":
                    requests = _dataService.RequestManager.GetUrgentRequests();
                    ViewBag.SortMethod = "Displaying high priority requests only";
                    break;
                default:
                    requests = _dataService.GetAllRequests();
                    ViewBag.SortMethod = null; 
                    break;
            }

            return View(requests);
        }

        //ServiceRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // ServiceRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.RequestId = Guid.NewGuid().ToString();
            request.NumericId = new Random().Next(1000, 9999);
            request.Status = "Submitted";
            request.Created = DateTime.Now;

            _dataService.AddServiceRequest(request);
            TempData["SuccessMessage"] = "Service request submitted successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ServiceRequests/Details/{id}
        public IActionResult Details(string id)
        {
            var request = _dataService.GetAllRequests()
                .FirstOrDefault(r => r.RequestId == id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        //ServiceRequests/Search
        public IActionResult Search(int? numericId)
        {
            if (!numericId.HasValue)
            {
                return View();
            }

            var request = _dataService.RequestManager.SearchById(numericId.Value);

            if (request == null)
            {
                ViewBag.Message = $"No request found with ID: {numericId}";
                return View();
            }

            ViewBag.SearchMethod = "Found using AVL Tree Search";
            return View("Details", request);
        }

        // ServiceRequests/Routing
        public IActionResult Routing()
        {
            var distances = _dataService.RequestManager.GetShortestPathFromCentralOffice("Central Office");
            var mst = _dataService.RequestManager.GetOptimalRoutingNetwork();
            var graph = _dataService.RequestManager.GetLocationGraph();

            ViewBag.Distances = distances;
            ViewBag.MST = mst;
            ViewBag.Graph = graph.GetGraph();

            return View();
        }
    }
}