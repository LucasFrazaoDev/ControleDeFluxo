using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Invoice
{
    public string Name;
    public string LicensePlate;
    public string Service;
    public string Date;

    public Invoice(string name, string licensePlate, string service, string date)
    {
        Name = name;
        LicensePlate = licensePlate;
        Service = service;
        Date = date;
    }
}

