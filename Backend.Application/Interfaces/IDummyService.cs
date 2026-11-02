using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces
{
    public interface IDummyService
    {
        Task<DummyDbClass> GetDummyDataAsync();

        Task<IReadOnlyCollection<DummyDbClass>> GetDummyCollectionAsync();
    }
}
