using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    // Materiales que puedes asignar desde el Inspector
    public Material defaultMaterial;    // Material original del jugador
    public Material damageMaterial;     // Material cuando el jugador recibe daño
    public Material dodgeMaterial;      // Material cuando el jugador esquiva

    private SkinnedMeshRenderer skinnedMeshRenderer;  // Referencia al SkinnedMeshRenderer

    void Start()
    {
        // Intentar obtener el SkinnedMeshRenderer del objeto o de sus hijos
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        // Asegurarse de que haya un SkinnedMeshRenderer
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("No se encontró SkinnedMeshRenderer en el objeto o en sus hijos: " + gameObject.name);
        }

        // Establecer el material original
        if (skinnedMeshRenderer != null && defaultMaterial != null)
        {
            skinnedMeshRenderer.material = defaultMaterial;
        }
    }

    // Cambiar al material de daño
    public void ApplyDamageMaterial()
    {
        if (skinnedMeshRenderer != null && damageMaterial != null)
        {
            skinnedMeshRenderer.material = damageMaterial;
        }
    }

    // Cambiar al material de esquive
    public void ApplyDodgeMaterial()
    {
        if (skinnedMeshRenderer != null && dodgeMaterial != null)
        {
            skinnedMeshRenderer.material = dodgeMaterial;
        }
    }

    // Restaurar el material original
    public void RestoreDefaultMaterial()
    {
        if (skinnedMeshRenderer != null && defaultMaterial != null)
        {
            skinnedMeshRenderer.material = defaultMaterial;
        }
    }
}
