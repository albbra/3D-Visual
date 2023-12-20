using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    // Assign the button in the inspector
    public Button exitButton;

    void Start()
    {
        // Add a listener to the button's click event
        exitButton.onClick.AddListener(ExitGame);
    }

    // Function to be called when the button is clicked
    void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}