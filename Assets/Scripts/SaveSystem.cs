using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UIElements;
using System.Linq;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private string m_filePath;
    private InvoiceList m_invoiceList = new InvoiceList();

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
            return;
        }

        string json = File.ReadAllText(m_filePath);
        m_invoiceList = JsonUtility.FromJson<InvoiceList>(json);
    }

    public void AddNewNote(Invoice newInvoice)
    {
        m_invoiceList.invoices.Add(newInvoice);
        SaveNotes();
    }

    public List<Invoice> GetConsultedInvoices(PopupField<string> popupField, string searchText)
    {
        List<Invoice> consultedInvoices = new List<Invoice>();

        switch (popupField.index)
        {
            // Search in the list for Name
            case 0:
                consultedInvoices = m_invoiceList.invoices
                    .Where(invoice => invoice.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(invoice => invoice.Date)
                    .ToList();
                break;

            // Search in the list for License Plate
            case 1:
                consultedInvoices = m_invoiceList.invoices
                    .Where(invoice => invoice.LicensePlate.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(invoice => invoice.Date)
                    .ToList();
                break;
        }

        return consultedInvoices;
    }

    public void DeleteInvoice(Invoice invoiceToDelete)
    {
        if (m_invoiceList == null)
            LoadNotes();

        if (m_invoiceList.invoices.Contains(invoiceToDelete))
        {
            m_invoiceList.invoices.Remove(invoiceToDelete);

            //m_invoicesByName.Remove(invoiceToDelete.Name);
            //m_invoicesByLicensePlate.Remove(invoiceToDelete.LicensePlate);

            SaveNotes();
        }
    }

}
