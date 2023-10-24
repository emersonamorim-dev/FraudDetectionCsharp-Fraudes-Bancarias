using System.Net;

namespace FraudDetectionCsharp.Domain.entities
{
    public class Location
    {
        public string Country { get; set; }
        public string IPAddress { get; set; }

        public decimal Amount { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        // Propriedade para determinar se o pagamento é de um país estrangeiro
        public bool IsForeignTransaction { get; set; }

        // Propriedade para determinar se o pagamento foi aprovado
        public bool IsApproved { get; set; }


    }


}
