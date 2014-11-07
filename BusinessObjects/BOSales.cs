using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class BOSales
    {
        public string InvoiceNo
        {
            get;
            set;
        }

        public string InvoiceDate
        {
            get;
            set;
        }

        public string SubTotal
        {
            get;
            set;
        }


        public string VATPercent
        {
            get;
            set;
        }

        public string VATAmount
        {
            get;
            set;
        }

        public string GrandTotal
        {
            get;
            set;
        }

        public string TotalPayment
        {
            get;
            set;
        }

        public string PaymentDue
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }
    }
}
