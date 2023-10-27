using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private string m_filePath;
    private List<Invoice> m_invoices = new List<Invoice>();
    private Dictionary<string, Invoice> m_invoicesByName = new Dictionary<string, Invoice>();
    private Dictionary<string, Invoice> m_invoicesByLicensePlate = new Dictionary<string, Invoice>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        m_filePath = Path.Combine(Application.persistentDataPath, "invoices.json");
    }

    private void Start()
    {
        LoadNotes();
        Debug.Log(m_filePath);
    }

    public void SaveNotes()
    {
        string json = JsonUtility.ToJson(m_invoices);

        File.WriteAllText(m_filePath, json);
    }

    public void LoadNotes()
    {
        if (!File.Exists(m_filePath))
        {
            Debug.Log("Não há nenhum arquivo salvo!");
            return;
        }

        string json = File.ReadAllText(m_filePath);
        m_invoices = JsonUtility.FromJson<List<Invoice>>(json);

        m_invoicesByName.Clear();
        m_invoicesByLicensePlate.Clear();

        foreach (var invoice in m_invoices)
        {
            m_invoicesByName[invoice.Name] = invoice;
            m_invoicesByLicensePlate[invoice.LicensePlate] = invoice;
        }
    }


    public void AddNewNote(Invoice newInvoice)
    {
        m_invoices.Add(newInvoice);
        m_invoicesByName[newInvoice.Name] = newInvoice;
        m_invoicesByLicensePlate[newInvoice.LicensePlate] = newInvoice;
        SaveNotes();
    }

    public Invoice GetInvoiceByName(string name)
    {
        Debug.Log(m_invoices.Count);

        if (m_invoicesByName.TryGetValue(name, out Invoice invoice))
        {
            return invoice;
        }
        return null;
    }

    public Invoice GetInvoiceByLicensePlate(string licensePlate)
    {
        if (m_invoicesByLicensePlate.TryGetValue(licensePlate, out Invoice invoice))
        {
            return invoice;
        }
        return null;
    }
}
