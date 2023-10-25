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

    private Button m_newNoteButton;
    private Button m_consultNoteButton;
    private Button m_quitButton;

    private void Awake()
    {
        m_root = GetComponent<UIDocument>().rootVisualElement;

        m_newNoteButton = m_root.Q<Button>("NewNoteButton");
        m_consultNoteButton = m_root.Q<Button>("ConsultNoteButton");
        m_quitButton = m_root.Q<Button>("QuitButton");

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
