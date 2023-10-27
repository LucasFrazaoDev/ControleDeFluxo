using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    private VisualElement m_root;

    private Button m_quitButton;
    private Button m_saveNoteButton;
    private Button m_searchNoteButton;

    private TextField m_nameTextField;
    private TextField m_licensePlateTextField;
    private TextField m_serviceTextField;

    private Label m_dateTimeLabel;

    private PopupField<string> m_popUpField;

    private TextField m_searchFilterTextField;
    private TextField m_consultedNameTextField;
    private TextField m_consultedLicensePlateTextField;
    private TextField m_consultedDateTextField;
    private TextField m_consultedServiceTextField;

    private List<string> m_popupFieldOptions = new List<string> { "Placa", "Nome" };

    private void Awake()
    {
        m_root = GetComponent<UIDocument>().rootVisualElement;

        m_quitButton = m_root.Q<Button>("QuitButton");
        m_saveNoteButton = m_root.Q<Button>("AddNewNote");
        m_searchNoteButton = m_root.Q<Button>("SearchNoteButton");
        m_dateTimeLabel = m_root.Q<Label>("TimeLabel");

        m_nameTextField = m_root.Q<TextField>("NameTextField");
        m_licensePlateTextField = m_root.Q<TextField>("LicensePlateTextField");
        m_serviceTextField = m_root.Q<TextField>("ServiceTextField");

        m_searchFilterTextField = m_root.Q<TextField>("SearchFilterTextField");
        m_popUpField = m_root.Q<PopupField<string>>("ConsultFilterDropdownField");
        m_consultedNameTextField = m_root.Q<TextField>("ConsultedNameTextField");
        m_consultedLicensePlateTextField = m_root.Q<TextField>("ConsultedLicensePlateTextField");
        m_consultedDateTextField = m_root.Q<TextField>("ConsultedDateTextField");
        m_consultedServiceTextField = m_root.Q<TextField>("ConsultedServiceTextField");
        
    }

    private void OnEnable()
    {
        m_popUpField.choices = m_popupFieldOptions;
        m_popUpField.RegisterValueChangedCallback(OnPopupFieldValueChanged);

        m_quitButton.clicked += OnQuitButtonClicked;
        m_saveNoteButton.clicked += OnSaveButtonClicked;
        m_searchNoteButton.clicked += OnSearchNoteButtonClicked;
    }

    private void Start()
    {
        ClearTextFields();
    }

    private void OnSearchNoteButtonClicked()
    {
        Invoice consultedInvoice = SaveSystem.Instance.GetInvoiceByName("LUCAS");
        Debug.Log(consultedInvoice);
        if (consultedInvoice != null)
        {
            m_consultedNameTextField.value = consultedInvoice.Name;
            m_consultedLicensePlateTextField.value = consultedInvoice.LicensePlate;
            Debug.Log("Achei a sua nota!!");
        }
        Debug.Log("Busca encerrada!");
    }

    private void OnSaveButtonClicked()
    {
        string name = m_nameTextField.value;
        string licensePlate = m_licensePlateTextField.value;
        string service = m_serviceTextField.value;
        string date = System.DateTime.Now.ToString();

        Invoice newInvoice = new Invoice(name, licensePlate, service, date);
        SaveSystem.Instance.AddNewNote(newInvoice);
        ClearTextFields();
        Debug.Log("Nota Salva!!");
    }

    private void ClearTextFields()
    {
        m_nameTextField.value = "";
        m_licensePlateTextField.value = "";
        m_serviceTextField.value = "";

        m_searchFilterTextField.value = "";
        m_consultedNameTextField.value = "";
        m_consultedLicensePlateTextField.value = "";
        m_consultedDateTextField.value = "";
        m_consultedServiceTextField.value = "";
    }

    private void OnPopupFieldValueChanged(ChangeEvent<string> e)
    {
        Debug.Log("Selected value: " + e.newValue);
    }

    private void Update()
    {
        m_dateTimeLabel.text = System.DateTime.Now.ToString();
    }

    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
