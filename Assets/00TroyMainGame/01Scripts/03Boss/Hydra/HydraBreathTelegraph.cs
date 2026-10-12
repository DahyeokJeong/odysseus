using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HydraBreathTelegraph : MonoBehaviour
{
    [Header("Range")]
    [SerializeField] private float attackRange = 15f;
    [SerializeField, Range(0f, 360f)] private float attackAngle = 150f;
    [SerializeField] private Vector2 attackDirection = Vector2.down;

    [Header("Visual")]
    [SerializeField] private int textureSize = 512;
    [SerializeField] private Color warningColor = new Color(0f, 1f, 0f, 0.35f);

    private SpriteRenderer sr;
    private Texture2D texture;
    private Sprite sprite;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        CreateSector();
    }

    private void CreateSector()
    {
        int size = Mathf.Max(32, textureSize);

        texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;

        Color[] pixels = new Color[size * size];

        float halfAngle = attackAngle * 0.5f;
        float baseAngle = Mathf.Atan2(
            attackDirection.y,
            attackDirection.x
        ) * Mathf.Rad2Deg;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 point = new Vector2(
                    (x + 0.5f) / size * 2f - 1f,
                    (y + 0.5f) / size * 2f - 1f
                );

                if (point.sqrMagnitude > 1f)
                    continue;

                float angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
                float difference = Mathf.DeltaAngle(baseAngle, angle);

                if (Mathf.Abs(difference) <= halfAngle)
                    pixels[y * size + x] = warningColor;
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        sprite = Sprite.Create(
            texture,
            new Rect(0, 0, size, size),
            new Vector2(0.5f, 0.5f),
            size / (attackRange * 2f)
        );

        sr.sprite = sprite;
        sr.sortingOrder = 500;
    }

    private void OnDestroy()
    {
        if (sprite != null)
            Destroy(sprite);

        if (texture != null)
            Destroy(texture);
    }
}