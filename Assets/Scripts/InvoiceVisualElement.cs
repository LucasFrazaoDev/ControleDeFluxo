using UnityEngine;
using UnityEngine.UIElements;

public class InvoiceVisualElement : VisualElement
{
    private Invoice m_invoice;
    private Label m_invoiceName;
    private Label m_invoiceLicensePlate;
    private Label m_invoiceDate;

    public Invoice Invoice { get => m_invoice; set => m_invoice = value; }

    public InvoiceVisualElement(Invoice invoice)
    {
        // Store invoice to remove callback method in UI Manager
        Invoice = invoice;

        // Load UXML file
        var visualTreeAsset = Resources.Load<VisualTreeAsset>("InvoiceElement");
        visualTreeAsset.CloneTree(this);

        m_invoiceName = this.Q<Label>("InvoiceNameLabel");
        m_invoiceLicensePlate = this.Q<Label>("InvoiceLicensePlateLabel");
        m_invoiceDate = this.Q<Label>("InvoiceDateLabel");

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
