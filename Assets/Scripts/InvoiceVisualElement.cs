using UnityEngine;
using UnityEngine.UIElements;

public class InvoiceVisualElement : VisualElement
{
    private Invoice m_invoice;
    private Label m_invoiceName;
    private Label m_invoiceLicensePlate;
    private Label m_invoiceDate;

    private const string k_invoiceElementName = "InvoiceElement";
    private const string k_invoiceNameLabelName = "InvoiceNameLabel";
    private const string k_invoiceLicensePlateLabelName = "InvoiceLicensePlateLabel";
    private const string k_invoiceDateLabelName = "InvoiceDateLabel";


    public Invoice Invoice { get => m_invoice; set => m_invoice = value; }

    public InvoiceVisualElement(Invoice invoice)
    {
        // Store invoice to remove callback method in UI Manager
        Invoice = invoice;

        // Load UXML file
        var visualTreeAsset = Resources.Load<VisualTreeAsset>(k_invoiceElementName);
        visualTreeAsset.CloneTree(this);

        m_invoiceName = this.Q<Label>(k_invoiceNameLabelName);
        m_invoiceLicensePlate = this.Q<Label>(k_invoiceLicensePlateLabelName);
        m_invoiceDate = this.Q<Label>(k_invoiceDateLabelName);

        SetNome(invoice.Name);
        SetPlaca(invoice.LicensePlate);
        SetData(invoice.Date);
    }

    private void SetNome(string nome)
    {
        m_invoiceName.text = nome;
    }

    private void SetPlaca(string placa)
    {
        m_invoiceLicensePlate.text = placa;
    }

    private void SetData(string data)
    {
        m_invoiceDate.text = data;
    }
}
