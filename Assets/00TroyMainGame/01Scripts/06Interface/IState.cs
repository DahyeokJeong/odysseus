using UnityEngine;

public interface IState
{
    void Enter();
    void FixedTick();
    void Tick();
    void Exit();
}
