using System.Collections.Generic;

namespace WooliesX.Technical.Exercises.External.Contracts.Requests
{
    public class TrolleyRequest
    {
        public IEnumerable<ProductRequest> Products { get; set; }
        public IEnumerable<SpecialRequest> Specials { get; set; }
        public IEnumerable<QuantityRequest> Quantities { get; set; }
    }
}
