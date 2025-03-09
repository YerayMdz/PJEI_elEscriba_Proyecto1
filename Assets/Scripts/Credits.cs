using UnityEngine;

public class Credits : MonoBehaviour
{
    public Canvas canvasMenu; // Asigna el Canvas en el Inspector

    public void ToggleCanvas()
    {
        canvasMenu.gameObject.SetActive(!canvasMenu.gameObject.activeSelf);
    }
}
