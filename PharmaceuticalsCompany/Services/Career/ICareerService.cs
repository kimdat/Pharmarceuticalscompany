using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmaceuticalsCompany.Models.Career;
namespace PharmaceuticalsCompany.Services.Career
{
    public interface ICareerService
    {
         Task<CareerModel> Login(CareerModel candidate);
        Task<CareerModel> Register(CareerModel model, Education newEducation);
        Task<CareerModel> Logout();
    }
}
