using UnityEngine;
using UnityEngine.UI;

public class DeactivateObjectOnClick : MonoBehaviour
{
    public GameObject objectToDeactivate;

    private void Start()
    {
        // Füge den Button-Click-Handler hinzu
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(DeactivateObject);
        }
    }

    void DeactivateObject()
    {
        // Deaktiviere das angegebene GameObject
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
    }
}
