using UnityEngine;

public class BossTelegraph : MonoBehaviour
{
    public void Show(Vector2 position, Vector2 size)
    {
        transform.position = position;
        transform.localScale = size;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}