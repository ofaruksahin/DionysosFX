using DionysosFX.Module.WebApi;
using DionysosFX.Swan.Routing;
using DionysosFX.WebApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DionysosFX.WebApplication.Controllers
{
    [Route("draftinvoice")]
    public class InvoiceController : BaseController
    {
        [Route(HttpVerb.POST,"createdraft")]
        public async Task<bool> CreateDraftInvoiceEndpoint([FormData]CreateDraftInvoice dto)
        {
            return true;
        }
    }
}
