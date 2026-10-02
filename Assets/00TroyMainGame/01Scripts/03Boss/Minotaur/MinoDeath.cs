using UnityEngine;

public class MinoDeath : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private MinoView view;

    public void Dead()
    {
        view.SetDeadSprite();

        // 이후 미노타우로스 아이템 드랍
    }
}