using System;

namespace ExTest2.DTOs.Responses
{
    public class GetOrderResponse
    {
        public int IdOrder { get; set; }

        public DateTime DateAccepted { get; set; }

        public DateTime DateFinished { get; set; }

        public string Notes { get; set; }

        public int IdCustomer { get; set; }

        public int IdEmployee { get; set; }

    }
}