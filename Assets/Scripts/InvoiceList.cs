using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InvoiceList
{
    public List<Invoice> invoices;

    public InvoiceList()
    { 
        invoices = new List<Invoice>();
    }
}
