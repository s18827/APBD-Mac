using System;
using System.Collections.Generic;
using ExTest2.Entities;

namespace ExTest2.DTOs.Requests
{
    public class AddOrderRequest
    {
        /* REQUEST BODY:
        {
            "DateAccepted": "2020-06-01",
            "Notes": "Please, prepare for next friday",
            "Confectionery": [
                {
                    "Quantity": "1",
                    "Name": "Cake for birthday",
                    "Notes": "Write Happy birthday on the cake"
                }
            ]
        }
        */

        public DateTime DateAccepted { get; set; }

        public string Notes { get; set; }

        // collection of confectionery products:
        public IEnumerable<ConfectioneryProduct> Confectionery { get; set; }
    }
}