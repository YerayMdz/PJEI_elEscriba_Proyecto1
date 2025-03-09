using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public static Vector3 savedPosition = Vector3.zero;
    public static Quaternion savedRotation = Quaternion.identity;

    public static void SavePosition(Transform playerTransform)
    {
        Debug.Log("PlayerPositionManager está activo");
        savedPosition = playerTransform.position;
        savedRotation = playerTransform.rotation;
    }

    public static void RestorePosition(GameObject player)
    {
        player.transform.position = savedPosition;
        player.transform.rotation = savedRotation;
    }


}
