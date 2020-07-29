
using CovidWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CovidWebApp.Services
{
    public interface ICityService
    {
        public List<City> GetCities();
        public City GetCity(int id);
        public void AddCity(City c);
        public void SaveChanges();
        public void ImportCityData(IFormFile file, DateTime date);
        public bool IsEmpty();

/*        public void GetCityCaseData(City c);*/


    }

    public class CityService : ICityService
    {
        private readonly AppDbContext _db;
        public CityService(AppDbContext db)
        {
            _db = db;
        }

        public List<City> GetCities()
        {
            return _db.Cities.ToList();
        }

        public City GetCity(int id)
        {
            return _db.Cities.Find(id);
        }
        public void AddCity(City c)
        {
            _db.Cities.Add(c);
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void ImportCityData(IFormFile file, DateTime date)
        {

            DateTime uploadDate = Convert.ToDateTime(date);

            List<City> cities = _db.Cities.ToList();
            using var reader = new StreamReader(file.OpenReadStream());
            string line;


            int lineNum = -1;
            while ((line = reader.ReadLine()) != null)
            {
                lineNum++;
                if (lineNum == 0) continue;

                string[] dataArr = line.Split(",");
                string cityName = dataArr[1].Replace("\"", "");
                CaseData currCase = new CaseData { Date = uploadDate, Cases = int.Parse(dataArr[2]), Deaths = int.Parse(dataArr[5]), Tested = int.Parse(dataArr[8]) };

                //reutrns City or default(null in this case)
                City result = cities.FirstOrDefault(x => x.CityName == cityName);

                if (result == null)
                {
                    City newCity = new City { CityName = cityName, Data = new List<CaseData> { currCase }, Population = int.Parse(dataArr[11]) };
                    AddCity(newCity);
                }
                else
                {
                    var updateCity = GetCity(result.Id);
                    //check to see if file has already been imported
                    //if it is just override data
                    CaseData caseExist = updateCity.Data.FirstOrDefault(x => x.Date == currCase.Date);

                    if (caseExist == null) updateCity.AddData(currCase);

                    else
                    {
                        updateCity.Data.Remove(caseExist);
                        updateCity.AddData(currCase);
                    }

                }
                SaveChanges();
            }



        }

        public bool IsEmpty()
        {
            if (_db.Cities.Count() == 0) return true;
            return false;
        }

    }
}
