using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ItemTooltipUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text enforceText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text statText;

    [Header("Position")]
    [SerializeField] private Vector2 offset = new Vector2(20f, 0f);

    public void Show(InventoryItem item)
    {
        ItemData data = item.Data;

        itemIcon.sprite = data.ItemIcon;
        nameText.text = data.ItemName;
        gradeText.text = data.ItemGrade.ToString();
        typeText.text = data.ItemType.ToString();

        if (item.Enforce > 0)
            enforceText.text = $"+{item.Enforce}";
        else
            enforceText.text = "";

        statText.text = GetStatText(data);

        gameObject.SetActive(true);
    }

    public void SetPosition(RectTransform itemRect)
    {
        RectTransform tooltipRect = transform as RectTransform;

        Vector3[] corners = new Vector3[4];
        itemRect.GetWorldCorners(corners);

        Vector3 leftCenter = (corners[0] + corners[1]) * 0.5f;
        Vector3 rightCenter = (corners[2] + corners[3]) * 0.5f;

        float tooltipWidth = tooltipRect.rect.width;

        if(rightCenter.x + tooltipWidth + offset.x <= Screen.width)
        {
            tooltipRect.pivot = new Vector2(0f, 0.5f);
            tooltipRect.position = rightCenter + new Vector3(offset.x, offset.y);
        }
        else
        {
            tooltipRect.pivot = new Vector2(1f, 0.5f);
            tooltipRect.position = leftCenter + new Vector3(-offset.x, offset.y);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private string GetStatText(ItemData data)
    {
        StringBuilder sb = new StringBuilder();

        if (data.Attack != 0)
            sb.AppendLine($"Attack     {GetValue(data.Attack)}");

        if (data.MaxHP != 0)
            sb.AppendLine($"Max HP     {GetValue(data.MaxHP)}");

        if (data.Defence != 0)
            sb.AppendLine($"Defence    {GetValue(data.Defence)}");

        if (data.MoveSpeed != 0)
            sb.AppendLine($"Move Speed     {GetValue(data.MoveSpeed)}");

        if (data.AttackRange != 0)
            sb.AppendLine($"Attack Range     {GetValue(data.AttackRange)}");

        if (data.AttackCooldown != 0)
            sb.AppendLine($"Attack Cooldown     {GetValue(data.AttackCooldown)}");

        return sb.ToString();
    }

    private string GetValue(float value)
    {
        if (value > 0)
            return $"+{value:0.##}";

        return $"{value:0.##}";
    }
}
