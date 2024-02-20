using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Entities.DataTranferObjects
{
    public record class LinkParameters
    {
        public BookParameters BookParameters { get; init; }
        public HttpContext HttpContext { get; init; }

    }
}
