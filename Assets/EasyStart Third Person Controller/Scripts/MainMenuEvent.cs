using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private UIDocument document;
    private Button button;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        Debug.Log(document != null ? "UIDocument found" : "UIDocument not found");

        button = document.rootVisualElement.Q("StartGameButton") as Button;
        Debug.Log(button != null ? "Button found" : "Button not found");

        if (button != null)
        {
            /*button.RegisterCallback<ClickEvent>(OnPlayGameClick);*/
            button.clicked += OnPlayGameClick;
        }
    }

    private void OnDisable()
    {
        /*button.UnregisterCallback<ClickEvent>(OnPlayGameClick);*/
        button.clicked -= OnPlayGameClick;
    }

    private void OnPlayGameClick(/*ClickEvent evt*/)
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
        Debug.Log("StartGameButton clicked. UI is now hidden.");
    }
}