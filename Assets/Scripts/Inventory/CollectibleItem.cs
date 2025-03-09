using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public Item item;
    public int quantity = 1;
    public AudioClip pickupSound; // Sonido al recoger
    private AudioSource audioSource;

    private void Start()
    {
        // Intentar obtener un AudioSource en este objeto o en hijos
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

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

                if (pickupSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }

                // Destruir después de que suene (con un pequeño delay si lo necesitas)
                Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
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
