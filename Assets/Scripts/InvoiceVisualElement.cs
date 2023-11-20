using UnityEngine;
using UnityEngine.UIElements;

public class InvoiceVisualElement : VisualElement
{
    private Label m_invoiceName;
    private Label m_invoiceLicensePlate;
    private Label m_invoiceDate;

    // Construtor que carrega o layout UXML
    public InvoiceVisualElement(Invoice invoice)
    {
        // Carregue o layout do arquivo UXML
        var visualTreeAsset = Resources.Load<VisualTreeAsset>("InvoiceElement");
        visualTreeAsset.CloneTree(this);

        m_invoiceName = this.Q<Label>("InvoiceNameLabel");
        m_invoiceLicensePlate = this.Q<Label>("InvoiceLicensePlateLabel");
        m_invoiceDate = this.Q<Label>("InvoiceDateLabel");

        SetNome(invoice.Name);
        SetPlaca(invoice.LicensePlate);
        SetData(invoice.Date);
    }

    // Métodos para configurar as informações da nota fiscal
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
