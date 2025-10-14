using Microsoft.AspNetCore.Mvc;
using ST10070933_PROG7312_MunicipalServices.Models;
using ST10070933_PROG7312_MunicipalServices.Services;

namespace ST10070933_PROG7312_MunicipalServices.Controllers
{
 
        public class IssuesController : Controller
        {
            private readonly IDataService _data;
            private readonly IWebHostEnvironment _env;


            public IssuesController(IDataService data, IWebHostEnvironment env)
            {
                _data = data;
                _env = env;
            }


            public IActionResult Index()
            {
                // show all reported issues
                var issues = _data.Issues.OrderByDescending(i => i.ReportedAt).ToList();
                return View(issues);
            }


            public IActionResult Create()
            {
                return View();
            }


            [HttpPost]
            public async Task<IActionResult> Create(Issue model, IFormFile? attachment)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                // handle attachment if present
                if (attachment != null && attachment.Length > 0)
                {
                    var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);


                    var filename = $"{Guid.NewGuid()}_{Path.GetFileName(attachment.FileName)}";
                    var filePath = Path.Combine(uploadsPath, filename);
                    await using var stream = System.IO.File.Create(filePath);
                    await attachment.CopyToAsync(stream);
                    model.Attachments.Add($"/uploads/{filename}");
                }


                model.ReportedAt = DateTime.UtcNow;
                _data.AddIssue(model);


                TempData["SuccessMessage"] = "Issue reported successfully!";
                return RedirectToAction("Index");
            }
        }
    }

