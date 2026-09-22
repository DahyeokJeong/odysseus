using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void LateUpdate()
    {
        if (player == null)
            return;

        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z);
    }
}