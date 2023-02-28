using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTh.Application.Features.Languages.Queries.GetLanguageList
{
    public class GetLanguageListQuery 
        : IRequest<LanguageListVm>
    {
    }
}
