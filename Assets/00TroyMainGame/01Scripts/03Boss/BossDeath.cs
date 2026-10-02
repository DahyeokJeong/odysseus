using UnityEngine;

public class BossDeath : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody2D rb;

    public void Dead()
    {
        rb.linearVelocity = Vector2.zero;
    }
}