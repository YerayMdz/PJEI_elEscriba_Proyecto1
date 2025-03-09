using UnityEngine;

public class Vecino1 : MonoBehaviour
{
    private Animator animator;
    private float Speed = 0f; // Inicialmente en idle

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseDown() // Se activa al hacer clic en el personaje
    {
        // Alterna entre 0 (Idle) y 1 (Move)
        Speed = (Speed == 0f) ? 1f : 0f;
        animator.SetFloat("Speed", Speed); // Actualiza el parámetro en el Animator
    }
}
