
using UnityEngine;

public class YSorting : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    [Header("Sorting")]
    [SerializeField] private Transform sortingPoint;
    [SerializeField] private bool isDynamic = false;

    private const float SORTING_SCALE = 10f;

    private void Awake()
    {
        if (spriteRenderers == null ||
            spriteRenderers.Length == 0)
        {
            spriteRenderers =
                GetComponentsInChildren<SpriteRenderer>();
        }
    }

    private void Start()
    {
        UpdateSortingOrder();
    }

    private void LateUpdate()
    {
        if (!isDynamic)
            return;

        UpdateSortingOrder();
    }

    private void UpdateSortingOrder()
    {
        int sortingOrder =
            Mathf.RoundToInt(
                -sortingPoint.position.y * SORTING_SCALE
            );

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.sortingOrder = sortingOrder;
        }
    }
}
