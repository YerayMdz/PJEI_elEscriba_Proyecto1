using UnityEngine;

public class DesactivarCredits : MonoBehaviour
{
    public Canvas canvasMenu; // Asigna el Canvas en el Inspector

    public void DesactivarCanvas()
    {
        canvasMenu.gameObject.SetActive(false);
    }
}
