using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // Instancia global del inventario
    public int slotsPerSection = 20; // Número de ranuras por sección
    public int totalSections = 5; // Número de secciones
    private List<InventorySlot> slots = new List<InventorySlot>(); // Lista de todas las ranuras

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int totalSlots = slotsPerSection * totalSections;
        Debug.Log("Inicializando inventario con " + totalSlots + " ranuras.");

        for (int i = 0; i < totalSlots; i++)
        {
            slots.Add(new InventorySlot(null, 0)); // Crear ranuras vacías
        }
    }

    // Método para agregar un objeto al inventario
    public bool AddItem(Item item, int quantity)
    {
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();

        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item && slot.quantity < item.maxStack)
            {
                int amountToAdd = Mathf.Min(quantity, item.maxStack - slot.quantity);
                slot.AddAmount(amountToAdd);
                quantity -= amountToAdd;

                Debug.Log("Actualizando UI...");
                inventoryUI.UpdateInventoryUI(); // Actualizar la UI
                if (quantity <= 0)
                    return true;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                int amountToAdd = Mathf.Min(quantity, item.maxStack);
                slots[i] = new InventorySlot(item, amountToAdd);
                quantity -= amountToAdd;

                Debug.Log("Actualizando UI...");
                inventoryUI.UpdateInventoryUI(); // Actualizar la UI
                if (quantity <= 0)
                    return true;
            }
        }

        return false;
    }

    // Método para obtener la lista de ranuras del inventario
    public List<InventorySlot> GetSlots()
    {
        if (slots.Count == 0)
        {
            Debug.LogWarning("El inventario no estaba inicializado al llamar a GetSlots, creando ranuras...");
            int totalSlots = slotsPerSection * totalSections;
            for (int i = 0; i < totalSlots; i++)
            {
                slots.Add(new InventorySlot(null, 0));
            }
        }
        return slots;
    }
}
