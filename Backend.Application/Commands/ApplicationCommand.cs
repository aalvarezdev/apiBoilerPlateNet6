using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Application.Commands
{
    public class ApplicationCommand : IRequest<bool>
    {
    }

    public class ApplicationCommandHandler : IRequestHandler<ApplicationCommand, bool>
    {
        public Task<bool> Handle(ApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
