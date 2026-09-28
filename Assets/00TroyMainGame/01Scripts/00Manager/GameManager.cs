using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        EventBus.OnToggleInventory += ToggleInventory;
    }

    private void OnDisable()
    {
        EventBus.OnToggleInventory -= ToggleInventory;
    }

    private void Start()
    {
        ChangeState(GameState.Playing);
    }

    private void ToggleInventory()
    {
        if (CurrentState == GameState.Playing)
        {
            ChangeState(GameState.Pausing);
            return;
        }

        if (CurrentState == GameState.Pausing)
        {
            ChangeState(GameState.Playing);
        }
    }

    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }
}
