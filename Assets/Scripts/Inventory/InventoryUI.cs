using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // Prefab de una ranura de inventario
    public Transform inventoryPanel; // Panel donde se mostrarán las ranuras
    private List<GameObject> slotObjects = new List<GameObject>(); // Lista de slots en la UI

    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            CreateInventorySlots();
            UpdateInventoryUI();
        }
    }

    // Crea las ranuras del inventario en la UI
    void CreateInventorySlots()
    {
        List<InventorySlot> slots = inventory.GetSlots();
        Debug.Log("Número de slots a crear: " + slots.Count);

        foreach (var slot in slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);
            slotObjects.Add(newSlot);
            Debug.Log("Slot creado en la UI");
        }
    }


    // Actualiza la UI con los objetos en el inventario
    public void UpdateInventoryUI()
    {
        List<InventorySlot> slots = inventory.GetSlots();

        for (int i = 0; i < slotObjects.Count; i++)
        {
            Transform slotTransform = slotObjects[i].transform;
            Image icon = slotTransform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI quantityText = slotTransform.Find("Quantity").GetComponent<TextMeshProUGUI>();

            if (slots[i].item != null)
            {
                Debug.Log("Mostrando en UI: " + slots[i].item.itemName);
                icon.sprite = slots[i].item.icon; // ASIGNAR EL ICONO
                icon.enabled = true; // Activar imagen
                quantityText.text = slots[i].quantity > 1 ? slots[i].quantity.ToString() : "";
            }
            else
            {
                icon.sprite = null;
                icon.enabled = false; // Ocultar imagen si no hay objeto
                quantityText.text = "";
            }
        }
    }

    // Mostrar y ocultar el inventario con la tecla "I"
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
        }
    }
}
