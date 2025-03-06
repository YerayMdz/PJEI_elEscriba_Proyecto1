using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuOpciones; // Asignar en el Inspector

    public void ToggleMenu()
    {
        menuOpciones.SetActive(!menuOpciones.activeSelf);
    }
}
