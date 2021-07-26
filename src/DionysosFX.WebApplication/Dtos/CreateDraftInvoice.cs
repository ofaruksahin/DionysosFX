using HttpMultipartParser;
using System.Collections.Generic;

namespace DionysosFX.WebApplication.Dtos
{
    public class CreateDraftInvoice
    {
        public List<int> InvoiceIds
        {
            get;
            set;
        }

        public List<FilePart> Files
        {
            get;
            set;
        }
    }
}
