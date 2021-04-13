
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Backend.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        
       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
