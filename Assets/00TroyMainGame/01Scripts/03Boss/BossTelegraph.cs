using UnityEngine;

public class BossTelegraph : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer sr;

    public void Show(Vector2 position, Vector2 size)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        transform.localScale = size;

        gameObject.SetActive(true);
    }

    public void Show(Vector2 position, Vector2 size, float angle)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale = size;

        gameObject.SetActive(true);
    }

    public void SetColor(Color color)
    {
        sr.color = color;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}