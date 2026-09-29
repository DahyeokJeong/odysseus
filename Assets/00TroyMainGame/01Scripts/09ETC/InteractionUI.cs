using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TMP_Text interactionText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show(string text)
    {
        interactionText.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}