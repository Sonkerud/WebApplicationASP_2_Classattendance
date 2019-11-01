
using Microsoft.AspNetCore.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplicationASP.Models;


namespace WebApplicationASP_2.Services
{
    public class JsonFileProductService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        Random random = new Random();

        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
           
        }

        
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products1.json"); }
        }
        

        public IEnumerable<Student> GetProducts()
        {

            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
               return JsonSerializer.Deserialize<Student[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }


        //public void AddRating(string productId, int rating)
        //{
        //    var products = GetProducts();

        //    if (products.First(x => x.Id == productId).Ratings == null)
        //    {
        //        products.First(x => x.Id == productId).Ratings = new int[] { rating };
        //    }
        //    else
        //    {
        //        var ratings = products.First(x => x.Id == productId).Ratings.ToList();
        //        ratings.Add(rating);
        //        products.First(x => x.Id == productId).Ratings = ratings.ToArray();
        //    }

        //    using (var outputStream = File.OpenWrite(JsonFileName))
        //    {
        //        JsonSerializer.Serialize<IEnumerable<Student>>(
        //            new Utf8JsonWriter(outputStream, new JsonWriterOptions
        //            {
        //                SkipValidation = true,
        //                Indented = true
        //            }),
        //            products
        //        );
        //    }
        //}

        public void ChangeAttendingStatus(string productId)
        {
            var products = GetProducts();

            var student = products.First(x => x.Id == productId);
            if (student.Attending == "true")
            {
                student.Attending = "false";
            } else if (student.Attending == "false")
            {
                student.Attending = "true";
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {


                JsonSerializer.Serialize<IEnumerable<Student>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }
        public int NumberOfGroups()
        {
            int nr = GetProducts().Where(x => x.Attending == "true").Count();
            int delat;
            if (nr % 3 == 0)
            {
                delat = nr / 3;
                return nr/delat;
            }
            else if (nr % 3 == 2)
            {
                delat = nr / 3;
                return nr/delat;
            }
            else if (nr % 3 == 1)
            {
                delat = nr / 2;
                return nr/delat;
            }
            return 0;
        }
     
        public Dictionary<int, List<Student>> GroupClass()
        {
            int chunk = NumberOfGroups();
            Dictionary<int, List<Student>> attendeesSortedIntoGroups = new Dictionary<int, List<Student>>();
            var list = GetProducts().Where(x => x.Attending == "true").Select(c=>c).ToList();
            var newRandomList = GetProducts().Where(x => x.Attending == "true").OrderBy(c => random.Next()).Select(c => c).ToList();

           
            for (int i = 0; i <= list.Count / chunk; i++)
            {
                if (newRandomList.Count >= chunk)
                {
                    attendeesSortedIntoGroups.Add(i, newRandomList.Take(chunk).ToList());

                    for (int x = 0; x < chunk; x++)
                    {
                        newRandomList.RemoveAt(0);
                    }
                }
                else if (newRandomList.Count != 0)
                {
                    attendeesSortedIntoGroups.Add(i, newRandomList.Take(newRandomList.Count).ToList());
                }
            }
            return attendeesSortedIntoGroups;
        }
        public int ChoseGroupForPresentation()
        {
            return random.Next(1, NumberOfGroups());
        }
    }
}
