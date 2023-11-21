using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    private Invoice m_currentlyDisplayedInvoice;

    // UI elements
    private VisualElement m_root;
    private VisualElement m_addInvoicePanel;
    private VisualElement m_searchInvoicePanel;
    private VisualElement m_blockViewPanel;

    private Button m_quitButton;
    private Button m_showAddInvoicePanelButton;
    private Button m_showSearchInvoicePanelButton;
    private Button m_addNewInvoiceButton;
    private Button m_searchInvoiceButton;

    private Button m_closeDetailsInvoicePanel;
    private Button m_editInvoiceButton;
    private Button m_saveChangedInvoiceButton;
    private Button m_deleteInvoiceButton;

    private TextField m_nameTextField;
    private TextField m_licensePlateTextField;
    private TextField m_serviceTextField;
    private TextField m_searchFilterTextField;

    private TextField m_nameToShowTextField;
    private TextField m_licensePlateToShowTextField;
    private TextField m_dateToShowTextField;
    private TextField m_serviceToShowTextField;

    private Label m_dateTimeLabel;

    private PopupField<string> m_popUpField;

    private ScrollView m_invoicesScrollView;

    // UI Elements names
    private const string k_addInvoicePanelName = "AddInvoicePanel";
    private const string k_searchInvoicePanelName = "SearchInvoicePanel";
    private const string k_blockViewPanelName = "BlockViewPanel";

    private const string k_AddInvoicePanelButtonName = "ShowSaveInvoicePanel";
    private const string k_searchInvoicePanelButtonName = "ShowSearchInvoicePanel";

    private const string k_quitButtonName = "QuitButton";
    private const string k_addNewInvoiceButtonName = "AddNewInvoiceButton";
    private const string k_searchInvoiceButtonName = "SearchInvoiceButton";

    private const string k_closeDetaisInvoicePanelButtonName = "CloseDetailsInvoicePanelButton";
    private const string k_editInvoiceButtonName = "EditInvoiceButton";
    private const string k_saveChangedInvoiceButtonName = "SaveChangedInvoiceButton";
    private const string k_deleteInvoiceButtonName = "DeleteInvoiceButton";

    private const string k_nameTextFieldName = "NameTextField";
    private const string k_licensePlateTextFieldName = "LicensePlateTextField";
    private const string k_serviceTextFieldName = "ServiceTextField";
    private const string k_searchFilterTextFieldName = "SearchFilterTextField";

    private const string k_nameToShowTextFieldName = "InvoiceToShowNameTextField";
    private const string k_licensePlateToShowTextFieldName = "InvoiceToShowPlateTextField";
    private const string k_dateToShowTextFieldName = "InvoiceToShowDateTextField";
    private const string k_serviceToShowTextFieldName = "InvoiceToShowServiceTextField";

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

        SignatureButtonClickEvents();
    }

    private void Start()
    {
        //ClearTextFields();
        m_saveChangedInvoiceButton.SetEnabled(false);
    }

    private void Update()
    {
        m_dateTimeLabel.text = System.DateTime.Now.ToString();
    }

    private void OnDisable()
    {
        UnsignatureButtonClickEvents();
    }

    private void GetVisualElementsReferences()
    {
        m_root = GetComponent<UIDocument>().rootVisualElement;
        m_addInvoicePanel = m_root.Q<VisualElement>(k_addInvoicePanelName);
        m_searchInvoicePanel = m_root.Q<VisualElement>(k_searchInvoicePanelName);
        m_blockViewPanel = m_root.Q<VisualElement>(k_blockViewPanelName);
    }

    private void GetButtonsReferences()
    {
        m_showAddInvoicePanelButton = m_root.Q<Button>(k_AddInvoicePanelButtonName);
        m_showSearchInvoicePanelButton = m_root.Q<Button>(k_searchInvoicePanelButtonName);
        m_quitButton = m_root.Q<Button>(k_quitButtonName);

        m_addNewInvoiceButton = m_root.Q<Button>(k_addNewInvoiceButtonName);
        m_searchInvoiceButton = m_root.Q<Button>(k_searchInvoiceButtonName);

        m_closeDetailsInvoicePanel = m_root.Q<Button>(k_closeDetaisInvoicePanelButtonName);
        m_editInvoiceButton = m_root.Q<Button>(k_editInvoiceButtonName);
        m_saveChangedInvoiceButton = m_root.Q<Button>(k_saveChangedInvoiceButtonName);
        m_deleteInvoiceButton = m_root.Q<Button>(k_deleteInvoiceButtonName);
    }

    private void GetTextFieldsReferences()
    {
        m_searchFilterTextField = m_root.Q<TextField>(k_searchFilterTextFieldName);

        m_nameTextField = m_root.Q<TextField>(k_nameTextFieldName);
        m_licensePlateTextField = m_root.Q<TextField>(k_licensePlateTextFieldName);
        m_serviceTextField = m_root.Q<TextField>(k_serviceTextFieldName);

        m_nameToShowTextField = m_root.Q<TextField>(k_nameToShowTextFieldName);
        m_licensePlateToShowTextField = m_root.Q<TextField>(k_licensePlateToShowTextFieldName);
        m_dateToShowTextField = m_root.Q<TextField>(k_dateToShowTextFieldName);
        m_serviceToShowTextField = m_root.Q<TextField>(k_serviceToShowTextFieldName);
    }

    private void SignatureButtonClickEvents()
    {
        m_showAddInvoicePanelButton.clicked += ShowAddInvoicePanel;
        m_showSearchInvoicePanelButton.clicked += ShowSearchInvoicePanel;
        m_quitButton.clicked += OnQuitButtonClicked;

        m_addNewInvoiceButton.clicked += OnSaveButtonClicked;
        m_searchInvoiceButton.clicked += OnSearchNoteButtonClicked;
        m_closeDetailsInvoicePanel.clicked += ToggleDetailsInvoicePanel;
        m_editInvoiceButton.clicked += EditInvoice;
        m_saveChangedInvoiceButton.clicked += SaveChangesOfInvoice;
        m_deleteInvoiceButton.clicked += DeleteInvoice;
    }

    private void UnsignatureButtonClickEvents()
    {
        m_quitButton.clicked -= OnQuitButtonClicked;
        m_showAddInvoicePanelButton.clicked -= ShowAddInvoicePanel;
        m_showSearchInvoicePanelButton.clicked -= ShowSearchInvoicePanel;
        m_addNewInvoiceButton.clicked -= OnSaveButtonClicked;
        m_searchInvoiceButton.clicked -= OnSearchNoteButtonClicked;
        m_closeDetailsInvoicePanel.clicked -= ToggleDetailsInvoicePanel;
        m_editInvoiceButton.clicked -= EditInvoice;
        m_saveChangedInvoiceButton.clicked += SaveChangesOfInvoice;
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
            ShowInvoicesInScrollView(consultedInvoices);
        }
        else
            ClearTextFields();
    }

    private void ShowInvoicesInScrollView(List<Invoice> consultedInvoices)
    {
        ClearInvoiceElements();

        foreach (var invoice in consultedInvoices)
        {
            var invoiceElement = new InvoiceVisualElement(invoice);
            m_invoicesScrollView.Add(invoiceElement);
            invoiceElement.RegisterCallback<ClickEvent>(evt => ShowInvoiceDetails(invoice));
        }
    }

    private void ClearInvoiceElements()
    {
        // Remove callback and clear elements
        foreach (var oldElement in m_invoicesScrollView.Children())
        {
            if (oldElement is InvoiceVisualElement invoiceElement)
            {
                invoiceElement.UnregisterCallback<ClickEvent>(evt => ShowInvoiceDetails(invoiceElement.Invoice));
            }
        }

        m_invoicesScrollView.Clear();
    }

    private void ShowInvoiceDetails(Invoice invoiceToShow)
    {
        ToggleDetailsInvoicePanel();

        m_currentlyDisplayedInvoice = invoiceToShow;
        
        m_nameToShowTextField.value = invoiceToShow.Name;
        m_licensePlateToShowTextField.value = invoiceToShow.LicensePlate;
        m_dateToShowTextField.value = invoiceToShow.Date;
        m_serviceToShowTextField.value = invoiceToShow.Service;

        SetTextFieldsReadOnly(true);
        EnableDisableButtons(true, true, true, false);
    }

    private void ToggleDetailsInvoicePanel()
    {
        if(m_blockViewPanel.style.display == DisplayStyle.None)
            m_blockViewPanel.style.display = DisplayStyle.Flex;
        else
            m_blockViewPanel.style.display = DisplayStyle.None;
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

    private void EditInvoice()
    {
        SetTextFieldsReadOnly(false);
        EnableDisableButtons(false, false, false, true);
    }

    private void SaveChangesOfInvoice()
    {
        if (m_currentlyDisplayedInvoice != null)
        {
            m_currentlyDisplayedInvoice.Name = m_nameToShowTextField.value;
            m_currentlyDisplayedInvoice.LicensePlate = m_licensePlateToShowTextField.value;
            m_currentlyDisplayedInvoice.Service = m_serviceToShowTextField.value;

            SaveSystem.Instance.SaveNotes();

            SetTextFieldsReadOnly(true);

            EnableDisableButtons(true, true, true, false);

            UpdateScrollView();
        }
    }

    private void SetTextFieldsReadOnly(bool isReadOnly)
    {
        m_nameToShowTextField.isReadOnly = isReadOnly;
        m_licensePlateToShowTextField.isReadOnly = isReadOnly;
        m_serviceToShowTextField.isReadOnly = isReadOnly;
    }

    private void EnableDisableButtons(bool showDetailsButton, bool editButton, bool deleteButton, bool saveChangesButton)
    {
        m_closeDetailsInvoicePanel.SetEnabled(showDetailsButton);
        m_editInvoiceButton.SetEnabled(editButton);
        m_deleteInvoiceButton.SetEnabled(deleteButton);
        m_saveChangedInvoiceButton.SetEnabled(saveChangesButton);
    }

    private void UpdateScrollView()
    {
        string searchText = m_searchFilterTextField.value;
        List<Invoice> consultedInvoices = SaveSystem.Instance.GetConsultedInvoices(m_popUpField, searchText);

        ClearInvoiceElements();
        ShowInvoicesInScrollView(consultedInvoices);
    }

    private void DeleteInvoice()
    {
        if (m_currentlyDisplayedInvoice != null)
        {
            SaveSystem.Instance.DeleteInvoice(m_currentlyDisplayedInvoice);

            ClearTextFields();

            m_blockViewPanel.style.display = DisplayStyle.None;
            UpdateScrollView();
        }
    }

    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
