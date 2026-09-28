using UnityEngine;
using TMPro;

public class EquipmentStatUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerModel playerModel;

    [Header("Stat")]
    [SerializeField] private TMP_Text maxHPText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text defenceText;
    [SerializeField] private TMP_Text moveSpeedText;
    [SerializeField] private TMP_Text attackRangeText;
    [SerializeField] private TMP_Text attackCooldownText;

    private void OnEnable()
    {
        playerModel.OnStatChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        playerModel.OnStatChanged -= Refresh;
    }

    private void Refresh()
    {
        maxHPText.text = $"{playerModel.MaxHP:0.##}";
        attackText.text = $"{playerModel.Attack:0.##}";
        defenceText.text = $"{playerModel.Defence:0.##}";
        moveSpeedText.text = $"{playerModel.MoveSpeed:0.##}";
        attackRangeText.text = $"{playerModel.AttackRange:0.##}";
        attackCooldownText.text = $"{playerModel.AttackCooldown:0.##}";
    }

    private void OnDestroy()
    {
        if (playerModel != null)
            playerModel.OnStatChanged -= Refresh;
    }
}