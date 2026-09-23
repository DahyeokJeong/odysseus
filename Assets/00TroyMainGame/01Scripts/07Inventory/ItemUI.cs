using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Image icon;
    [SerializeField] private GameObject enforce;
    [SerializeField] private TMP_Text enforceText;

    private InventoryItem item;

    public InventoryItem Item => item;

    public void SetItem(InventoryItem item)
    {
        this.item = item;

        icon.sprite = item.Data.ItemIcon;

        UpdateEnforce();
    }

    private void UpdateEnforce()
    {
        if (item.Enforce <= 0)
        {
            enforce.SetActive(false);
            return;
        }

        enforce.SetActive(true);
        enforceText.text = $"+{item.Enforce}";
    }
}