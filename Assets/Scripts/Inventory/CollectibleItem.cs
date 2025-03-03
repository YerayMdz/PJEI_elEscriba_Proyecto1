using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public Item item;
    public int quantity = 1;

    private void OnMouseDown()
    {
        Debug.Log("Clic en: " + gameObject.name); // Ver si el objeto detecta el clic

        if (Inventory.instance != null)
        {
            Debug.Log("Inventario encontrado");

            bool added = Inventory.instance.AddItem(item, quantity);
            if (added)
            {
                Debug.Log("Objeto agregado al inventario: " + item.itemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventario lleno, no se pudo agregar");
            }
        }
        else
        {
            Debug.Log("Inventario NO encontrado en la escena");
        }
    }
}