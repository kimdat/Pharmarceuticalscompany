using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PharmaceuticalsCompany.Models.Career;

namespace PharmaceuticalsCompany.Services.Career
{
    public class CareerServiceImpl : ICareerService
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly SignInManager<ApplicationUser> _sm;
        public CareerServiceImpl(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm)
        {
            _um = um;
            _sm = sm;

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

        public  async Task<CareerModel> Register(CareerModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                DateOfBirth=model.DateOfBirth,
                Gender=model.Gender,
                Resume=model.Resume,
                Address=model.Address
            };
         
            var result = await _um.CreateAsync(user, model.PassWord);
            if(result.Succeeded)
            {

                await _sm.SignInAsync(user, isPersistent: false);
                return model;
            }
              
            
                return null;
        }
    }
}
