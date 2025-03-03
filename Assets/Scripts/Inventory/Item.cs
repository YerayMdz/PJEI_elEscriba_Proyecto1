using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;       // Nombre del objeto
    public Sprite icon;           // Icono del objeto para la UI
    public int maxStack = 20;     // Máximo de objetos que pueden apilarse (20)
}

