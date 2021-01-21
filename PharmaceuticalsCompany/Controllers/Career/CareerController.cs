using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalsCompany.Services.Career;
using PharmaceuticalsCompany.Models.Career;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PharmaceuticalsCompany.Controllers.Candidate
{
    public class CareerController : Controller
    {
        private  ICareerService services;
        public CareerController(ICareerService services)
        {
            this.services = services;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Authentication()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CareerModel candidate)
        {

                var model = await services.Login(candidate);
                if (model != null)
                {

                    ViewBag.Msg = "success";
                    return RedirectToAction("index", "Home");


                }
                else
                {
                    ViewBag.Msg = "invalid";
                }
               
            
           
            return View();

        }
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CareerModel candidate, IFormFile file,Education newEducation,IFormFile fileUser)
        {
          
                try
            {
                if(fileUser.Length>0)
                {
                    var filePath = Path.Combine("wwwroot/images", fileUser.FileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    candidate.Photo = "/images/" + fileUser.FileName;
                }
                if (file.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/file", file.FileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    candidate.Resume = "/file/" + file.FileName;
                }
                var result = await services.Register(candidate, newEducation);
                    if (result != null)
                    {
                       
                        return RedirectToAction("index", "Career");
                    }
                    else
                    {
                   
                    }

                }
                catch (Exception e) 
                {

                    ModelState.AddModelError(string.Empty, e.Message);
                }
          
            return View();
        }
    }
}