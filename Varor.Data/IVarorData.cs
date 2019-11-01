using VarorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IVarorData
    {
        IEnumerable<VarorModel> GetVaraByName(string name);
        IEnumerable<VarorModel> GetVaraByName();
        VarorModel GetById(int id);
        VarorModel Update(VarorModel updatedVara);
        VarorModel Add(VarorModel newVara);
        int Commit();
        VarorModel Delete(int id);
        int GetCountofVaror();
    }

}
