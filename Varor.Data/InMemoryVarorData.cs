using System.Linq;
using System.Collections.Generic;
using VarorLibrary;

namespace OdeToFood.Data
{
    public class InMemoryVarorData : IVarorData
    {
        //readonly List<VarorModel> varor;

        public static List<VarorModel> varor;

        public InMemoryVarorData()
        {
            varor = new List<VarorModel>()
            {
                new VarorModel { Id = 1, Name = "Banan", Price = 12},
                new VarorModel { Id = 2, Name = "Blåbär", Price = 22},
                new VarorModel { Id = 3, Name = "Apelsin", Price = 31.50},
                new VarorModel { Id = 4, Name = "Peppar", Price = 3.50},
                new VarorModel { Id = 5, Name = "Krusbär", Price = 30}
            };
        }

        public VarorModel GetById(int id)
        {
            return varor.SingleOrDefault(r => r.Id == id);
        }

        public VarorModel Add(VarorModel newVara)
        {
            varor.Add(newVara);
            newVara.Id = varor.Max(r => r.Id) + 1;
            return newVara;
        }

        //Riktiga update
        public VarorModel Update(VarorModel updatedVara)
        {
            var vara = varor.SingleOrDefault(r => r.Id == updatedVara.Id);
            if (vara != null)
            {
                vara.Name = updatedVara.Name;
                vara.Price = updatedVara.Price;
            }
            return vara;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<VarorModel> GetVaraByName(string name = null)
        {
            return from r in varor
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public IEnumerable<VarorModel> GetVaraByName()
        {
            string name = "";
            return from r in varor
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public VarorModel Delete(int id)
        {
            var restaurant = varor.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                varor.Remove(restaurant);
            }
            return null;
        }

        public int GetCountofVaror()
        {
            return varor.Count();
        }
    }
}
