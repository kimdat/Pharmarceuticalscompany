using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PharmaceuticalsCompany.Models.Career;
using PharmaceuticalsCompany.Data;
namespace PharmaceuticalsCompany.Services.Career
{
    public class CareerServiceImpl : ICareerService
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly SignInManager<ApplicationUser> _sm;
        private ApplicationDbContext context;
        public CareerServiceImpl(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm,ApplicationDbContext context)
        {
            _um = um;
            _sm = sm;
            this.context = context;

        }
        public async Task<CareerModel> Login(CareerModel candidate)
        {
                var result = await _sm.PasswordSignInAsync(candidate.Email, candidate.PassWord, false, false);
                if(result.Succeeded)
                {
                    return candidate;
                }
                else
                {
                    return null;
                }
        }

        public async Task<CareerModel> Logout()
        {
            await _sm.SignOutAsync();
            return null;
        }

        public  async Task<CareerModel> Register(CareerModel model,Education newEducation)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                DateOfBirth=model.DateOfBirth,
                Gender=model.Gender,
                Resume=model.Resume,
                Address=model.Address,
                Photo=model.Photo,
                PhoneNumber=model.Phone

                
            };
            
            var educationCarrer = new EducationCareer();
             var education = context.Educations.SingleOrDefault(e=>e.Name_school.ToLower().Equals(newEducation.Name_school.ToLower()));
            
             if (education==null)
             {
                 context.Educations.Add(newEducation);
                 context.SaveChanges();
                educationCarrer.Education_Id = newEducation.Id;
              }
             else
             {
                 educationCarrer.Education_Id = education.Id;
             }
           
            var result = await _um.CreateAsync(user, model.PassWord);
            educationCarrer.User_Id = user.Id;
            context.EducationCareers.Add(educationCarrer);
            context.SaveChanges();
            if (result.Succeeded)
               {

                   await _sm.SignInAsync(user, isPersistent: false);
                   return model;
               }
               else
               {
                   return null;
               }
             



        }
    }
}
