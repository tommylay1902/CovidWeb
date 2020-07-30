
using CovidWebApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CovidWebApp.Services
{
    public interface ICityService
    {
        public List<City> GetCities();
        public City GetCity(int id);
        public void AddCity(City c);
        public void AddUserInputCity(City c);
        public void SaveChanges();
        public void ImportCityData(IFormFile file, DateTime date);
        public bool IsEmpty();
        public List<DateTime> GetCaseDataDates(int id);
        public List<CaseData> GetCityCaseData(int id);
        public List<int> GetCaseDataCases(int id);
        public List<int> GetCaseDataDeaths(int id);
        public List<int> GetCaseDataTested(int id);

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
        public List<CaseData> GetCityCaseData(int id)
        {
            var caseDatas = _db.CaseDatas.ToList();
            return caseDatas.Where(x => x.City.Id == id).OrderBy(x => x.Date).ToList();
        }
        public void ImportCityData(IFormFile file, DateTime date)
        {

            DateTime uploadDate = Convert.ToDateTime(date);
            /*var caseDatas = _db.CaseDatas.ToList();*/
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
                    City newCity = new City { CityName = cityName,  Population = int.Parse(dataArr[11]) };
                    cities.Add(newCity);
                    currCase.City = newCity;
                    _db.CaseDatas.Add(currCase);
                }
                else
                {
                    var updateCity = GetCity(result.Id);
                    
                    //check to see if file has already been imported
                    //if it is just override data
                    CaseData caseExist = GetCityCaseData(updateCity.Id).FirstOrDefault(x => x.Date == currCase.Date);
                    currCase.City = updateCity;
                    if (caseExist == null)
                    {
                        _db.CaseDatas.Add(currCase);
                        /*caseDatas.Add(currCase);*/
                    }

                    else
                    {
                    /*    GetCityCaseData(updateCity.Id).Remove(caseExist);*/
                        _db.CaseDatas.Remove(caseExist);
                        _db.CaseDatas.Add(currCase);
/*
                        caseDatas.Remove(caseExist);
                        caseDatas.Add(currCase);*/
                        GetCityCaseData(updateCity.Id).Add(currCase);
                    }

                }
                
            }
            SaveChanges();

        }
        
        public bool IsEmpty()
        {
            if (_db.Cities.Count() == 0) return true;
            return false;
        }

        public List<DateTime> GetCaseDataDates(int id)
        {
            return _db.CaseDatas.Where(x => x.City.Id == id).Select(x => x.Date).OrderBy(x => x.Date).ToList();
        }

        public List<int> GetCaseDataCases(int id)
        {
            return _db.CaseDatas.Where(x => x.City.Id == id).Select(x => x.Cases).ToList();
        }

        public List<int> GetCaseDataDeaths(int id)
        {
            return _db.CaseDatas.Where(x => x.City.Id == id).Select(x => x.Deaths).ToList();
        }

        public List<int> GetCaseDataTested(int id)
        {
            return _db.CaseDatas.Where(x => x.City.Id == id).Select(x => x.Tested).ToList();
        }

        public void AddUserInputCity(City c)
        {
            _db.Cities.Add(c);
        }
    }
}
