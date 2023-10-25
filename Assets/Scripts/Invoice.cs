using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Invoice
{
    private string m_name;
    private string m_licensePlate;
    private string m_service;
    private string m_date;

    public string Name { get => m_name; set => m_name = value; }
    public string LicensePlate { get => m_licensePlate; set => m_licensePlate = value; }
    public string Service { get => m_service; set => m_service = value; }
    public string Date { get => m_date; set => m_date = value; }

    public Invoice(string name, string licensePlate, string service, string date)
    {
        Name = name;
        LicensePlate = licensePlate;
        Service = service;
        Date = date;
    }
}
