using Backend.Application.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Services
{
    public class DummyService : IDummyService
    {
        public async Task<DummyDbClass> GetDummyDataAsync()
        {
            // Simulate async data fetching
            return await Task.FromResult(new DummyDbClass { PocoID = 1, POCODescription = "Dummy Data" });
        }
    }
}
