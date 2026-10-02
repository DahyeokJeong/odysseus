using UnityEngine;

public class YSorting : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    [Header("Sorting")]
    [SerializeField] private Transform sortingPoint;

    private void Awake()
    {
        if (spriteRenderers == null ||
            spriteRenderers.Length == 0)
        {
            spriteRenderers =
                GetComponentsInChildren<SpriteRenderer>();
        }
    }

    private void LateUpdate()
    {
        int sortingOrder =
            Mathf.RoundToInt(
                -sortingPoint.position.y * 100f
            );

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.sortingOrder = sortingOrder;
        }
    }
}