using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private string m_filePath;
    private InvoiceList m_invoiceList = new InvoiceList();
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
    }

    public void SaveNotes()
    {
        string json = JsonUtility.ToJson(m_invoiceList);
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
        m_invoiceList = JsonUtility.FromJson<InvoiceList>(json);

        m_invoicesByName.Clear();
        m_invoicesByLicensePlate.Clear();

        foreach (var invoice in m_invoiceList.invoices)
        {
            m_invoicesByName[invoice.Name] = invoice;
            m_invoicesByLicensePlate[invoice.LicensePlate] = invoice;
        }
    }

    public void AddNewNote(Invoice newInvoice)
    {
        m_invoiceList.invoices.Add(newInvoice);
        m_invoicesByName[newInvoice.Name] = newInvoice;
        m_invoicesByLicensePlate[newInvoice.LicensePlate] = newInvoice;
        SaveNotes();
    }

    public Invoice GetInvoiceByName(string name)
    {
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
