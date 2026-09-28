using System;

public static class EventBus
{
    public static event Action OnToggleInventory;

    public static void ToggleInventory() { OnToggleInventory?.Invoke(); }
}