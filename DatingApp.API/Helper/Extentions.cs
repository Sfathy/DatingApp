using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helper
{
    public static class Extentions
    {
        public static void AddApplicationError(this HttpResponse respose,string message){
            respose.Headers.Add("Application-Error",message);
            respose.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            respose.Headers.Add("Access-Control-Allow-Origin","*");
        }
        public static int CalcAge(this DateTime theDateTime){
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age)>DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}