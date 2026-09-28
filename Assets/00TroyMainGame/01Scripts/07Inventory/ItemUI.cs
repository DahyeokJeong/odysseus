using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemUI : MonoBehaviour,
                      IPointerEnterHandler,
                      IPointerExitHandler,
                      IBeginDragHandler,
                      IEndDragHandler,
                      IDragHandler
{
    [Header("Component")]
    [SerializeField] private Image icon;
    [SerializeField] private GameObject enforce;
    [SerializeField] private TMP_Text enforceText;
    [SerializeField] private CanvasGroup canvasGroup;

    private EquipmentSlotUI equipmentSlot;
    private InventoryItem item;
    private ItemTooltipUI tooltip;
    private Transform originalParent;
    private Canvas canvas;

    public InventoryItem Item => item;
    public EquipmentSlotUI EquipmentSlot => equipmentSlot;

    private void Awake()
    {
        icon.enabled = false;

        tooltip = FindAnyObjectByType<ItemTooltipUI>(FindObjectsInactive.Include);

        canvas = GetComponentInParent<Canvas>();
    }

    public void SetItem(InventoryItem item)
    {
        this.item = item;
        
        icon.sprite = item.Data.ItemIcon;
        icon.enabled = true;

        UpdateEnforce();
    }

    public void SetEquipmentSlot(EquipmentSlotUI equipmentSlot)
    {
        this.equipmentSlot = equipmentSlot;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;

        tooltip.Show(item);
        tooltip.SetPosition(transform as RectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null)
            return;

        originalParent = transform.parent;

        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;

        tooltip.Hide();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
    }
}