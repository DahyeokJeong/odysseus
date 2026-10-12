using UnityEngine;

public class HydraHeadView : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer sr;

    [Header("Idle Sprite")]
    [SerializeField] private Sprite idleSprite01;
    [SerializeField] private Sprite idleSprite02;

    [Header("Animation")]
    [SerializeField] private float frameDuration = 0.4f;

    private float frameTimer;
    private bool isSecondFrame;
    private bool isPlaying;

    private void Update()
    {
        if (!isPlaying)
            return;

        frameTimer += Time.deltaTime;

        if (frameTimer >= frameDuration)
        {
            frameTimer = 0f;
            isSecondFrame = !isSecondFrame;

            sr.sprite = isSecondFrame? idleSprite02 : idleSprite01;
        }
    }

    public void PlayIdle()
    {
        frameTimer = Random.Range(0f, frameDuration);
        isSecondFrame = false;
        isPlaying = true;

        sr.sprite = idleSprite01;
    }

    public void StopIdle()
    {
        isPlaying = false;
    }

    public void SetSeveredSprite(Sprite severedSprite)
    {
        StopIdle();
        sr.sprite = severedSprite;
    }
}