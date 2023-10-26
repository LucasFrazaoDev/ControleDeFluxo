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

    private TextField m_nameTextField;
    private TextField m_licensePlateTextField;
    private TextField m_serviceTextField;

    private void Awake()
    {
        m_root = GetComponent<UIDocument>().rootVisualElement;

        m_quitButton = m_root.Q<Button>("QuitButton");
        m_nameTextField = m_root.Q<TextField>("NameTextField");
        m_licensePlateTextField = m_root.Q<TextField>("LicensePlateTextField");
        m_serviceTextField = m_root.Q<TextField>("ServiceTextField");

        m_quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
