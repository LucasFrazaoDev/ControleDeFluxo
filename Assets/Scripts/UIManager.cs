using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    // UI elements
    private VisualElement m_root;
    private VisualElement m_addInvoicePanel;
    private VisualElement m_searchInvoicePanel;

    private Button m_quitButton;
    private Button m_showAddInvoicePanelButton;
    private Button m_showSearchInvoicePanelButton;
    private Button m_addNewInvoiceButton;
    private Button m_searchInvoiceButton;

    private TextField m_nameTextField;
    private TextField m_licensePlateTextField;
    private TextField m_serviceTextField;
    private TextField m_searchFilterTextField;

    private Label m_dateTimeLabel;

    private PopupField<string> m_popUpField;

    private ScrollView m_invoicesScrollView;

    // UI Elements names
    private const string k_addInvoicePanelName = "AddInvoicePanel";
    private const string k_searchInvoicePanelName = "SearchInvoicePanel";

    private const string k_AddInvoicePanelButtonName = "ShowSaveInvoicePanel";
    private const string k_searchInvoicePanelButtonName = "ShowSearchInvoicePanel";
    private const string k_quitButton = "QuitButton";
    private const string k_addNewInvoiceButtonName = "AddNewInvoiceButton";
    private const string k_searchInvoiceButtonName = "SearchInvoiceButton";

    private const string k_nameTextFieldName = "NameTextField";
    private const string k_licensePlateTextFieldName = "LicensePlateTextField";
    private const string k_serviceTextFieldName = "ServiceTextField";
    private const string k_searchFilterTextFieldName = "SearchFilterTextField";

    private const string k_dateLabelName = "DateLabel";
    private const string k_consultFilterDropdownFieldName = "ConsultFilterDropdownField";

    private const string k_invoicesScrollViewName = "InvoicesScrollView";

    private List<string> m_popupFieldOptions = new List<string> { "Nome", "Placa" };

    private void Awake()
    {
        GetVisualElementsReferences();
        GetButtonsReferences();
        GetTextFieldsReferences();

        m_dateTimeLabel = m_root.Q<Label>(k_dateLabelName);
        m_popUpField = m_root.Q<PopupField<string>>(k_consultFilterDropdownFieldName);
        m_invoicesScrollView = m_root.Q<ScrollView>(k_invoicesScrollViewName);
    }

    private void OnEnable()
    {
        m_popUpField.choices = m_popupFieldOptions;

        m_showAddInvoicePanelButton.clicked += ShowAddInvoicePanel;
        m_showSearchInvoicePanelButton.clicked += ShowSearchInvoicePanel;
        m_quitButton.clicked += OnQuitButtonClicked;

        m_addNewInvoiceButton.clicked += OnSaveButtonClicked;
        m_searchInvoiceButton.clicked += OnSearchNoteButtonClicked;
    }

    private void Start()
    {
        //ClearTextFields();
    }

    private void Update()
    {
        m_dateTimeLabel.text = System.DateTime.Now.ToString();
    }

    private void OnDisable()
    {
        m_quitButton.clicked -= OnQuitButtonClicked;
        m_showAddInvoicePanelButton.clicked -= ShowAddInvoicePanel;
        m_showSearchInvoicePanelButton.clicked -= ShowSearchInvoicePanel;
        m_addNewInvoiceButton.clicked -= OnSaveButtonClicked;
        m_searchInvoiceButton.clicked -= OnSearchNoteButtonClicked;
    }

    private void GetVisualElementsReferences()
    {
        m_root = GetComponent<UIDocument>().rootVisualElement;
        m_addInvoicePanel = m_root.Q<VisualElement>(k_addInvoicePanelName);
        m_searchInvoicePanel = m_root.Q<VisualElement>(k_searchInvoicePanelName);
    }

    private void GetButtonsReferences()
    {
        m_showAddInvoicePanelButton = m_root.Q<Button>(k_AddInvoicePanelButtonName);
        m_showSearchInvoicePanelButton = m_root.Q<Button>(k_searchInvoicePanelButtonName);
        m_quitButton = m_root.Q<Button>(k_quitButton);

        m_addNewInvoiceButton = m_root.Q<Button>(k_addNewInvoiceButtonName);
        m_searchInvoiceButton = m_root.Q<Button>(k_searchInvoiceButtonName);
    }

    private void GetTextFieldsReferences()
    {
        m_searchFilterTextField = m_root.Q<TextField>(k_searchFilterTextFieldName);

        m_nameTextField = m_root.Q<TextField>(k_nameTextFieldName);
        m_licensePlateTextField = m_root.Q<TextField>(k_licensePlateTextFieldName);
        m_serviceTextField = m_root.Q<TextField>(k_serviceTextFieldName);
    }

    private void ShowAddInvoicePanel()
    {
        m_searchInvoicePanel.style.display = DisplayStyle.None;
        m_addInvoicePanel.style.display = DisplayStyle.Flex;
    }

    private void ShowSearchInvoicePanel()
    {
        m_searchInvoicePanel.style.display = DisplayStyle.Flex;
        m_addInvoicePanel.style.display = DisplayStyle.None;
    }

    private void OnSearchNoteButtonClicked()
    {
        string searchText = m_searchFilterTextField.value;
        if (AreFieldEmpty(searchText)) return;

        List<Invoice> consultedInvoices = SaveSystem.Instance.GetConsultedInvoices(m_popUpField, searchText);

        if (consultedInvoices.Count > 0)
        {
            // Faça algo com a lista de notas consultadas, por exemplo, exibir na ScrollView
            ShowInvoicesInScrollView(consultedInvoices);
        }
        else
            ClearTextFields();
    }

    private void ShowInvoicesInScrollView(List<Invoice> consultedInvoices)
    {
        // Limpe os elementos existentes na ScrollView
        m_invoicesScrollView.Clear();
        Debug.Log(consultedInvoices.Count);
        // Para cada nota, crie um botão horizontal na ScrollView
        foreach (var invoice in consultedInvoices)
        {
            // Crie uma instância do seu elemento customizado (InvoiceVisualElement)
            var invoiceElement = new InvoiceVisualElement(invoice);

            // Adicione o elemento à ScrollView
            m_invoicesScrollView.Add(invoiceElement);
        }
    }

    private void OnSaveButtonClicked()
    {
        string name = m_nameTextField.value;
        string licensePlate = m_licensePlateTextField.value;
        string service = m_serviceTextField.value;
        string date = System.DateTime.Now.ToString();

        if (AreFieldsEmpty(name, licensePlate, service)) return;

        Invoice newInvoice = new Invoice(name, licensePlate, service, date);
        SaveSystem.Instance.AddNewNote(newInvoice);
        ClearTextFields();
    }

    private void ClearTextFields()
    {
        m_nameTextField.value = "";
        m_licensePlateTextField.value = "";
        m_serviceTextField.value = "";

        m_searchFilterTextField.value = "";
    }

    private bool AreFieldEmpty(string searchTextValue)
    {
        return string.IsNullOrEmpty(searchTextValue);
    }

    private bool AreFieldsEmpty(string nameValue, string licensePlateValue, string serviceValue)
    {
        return string.IsNullOrEmpty(nameValue)
            || string.IsNullOrEmpty(licensePlateValue)
            || string.IsNullOrEmpty(serviceValue);
    }



    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
