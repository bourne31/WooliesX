using System.Collections.Generic;

namespace WooliesX.Technical.Exercises.External.Contracts.Requests
{
    public class SpecialRequest
    {
        public decimal Total { get; set; }
        public IEnumerable<QuantityRequest> Quantities { get; set; }
    }
}
