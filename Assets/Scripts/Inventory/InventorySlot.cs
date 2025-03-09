using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public Item item;  // Objeto almacenado en la ranura
    public int quantity; // Cantidad del objeto en la ranura

    public InventorySlot(Item newItem, int newQuantity)
    {
        item = newItem;
        quantity = newQuantity;
    }

    // Agregar más cantidad a la ranura
    public bool AddAmount(int amount)
    {
        if (quantity + amount <= item.maxStack)
        {
            quantity += amount;
            return true;
        }
        return false;
    }
}