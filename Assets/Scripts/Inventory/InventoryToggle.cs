using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
  
    public GameObject inventoryFrame; // Marco decorativo del inventario

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = !inventoryFrame.activeSelf; // Invertir estado actual
            inventoryFrame.SetActive(isActive);
        }
    }
}
