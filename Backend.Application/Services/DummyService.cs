using Backend.Application.Interfaces;
using Backend.Models;
using System.Collections.Generic;
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

        public Task<IReadOnlyCollection<DummyDbClass>> GetDummyCollectionAsync()
        {
            IReadOnlyCollection<DummyDbClass> items = new[]
            {
                new DummyDbClass { PocoID = 1, POCODescription = "First sample item" },
                new DummyDbClass { PocoID = 2, POCODescription = "Second sample item" },
                new DummyDbClass { PocoID = 3, POCODescription = "Third sample item" }
            };

            return Task.FromResult(items);
        }
    }
}
