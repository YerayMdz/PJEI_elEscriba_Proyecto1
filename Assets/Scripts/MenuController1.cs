using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController1 : MonoBehaviour
{
    public GameObject menuOpciones; // Asignar en el Inspector

    public void ToggleMenu()
    {
        menuOpciones.SetActive(!menuOpciones.activeSelf);
    }
}
