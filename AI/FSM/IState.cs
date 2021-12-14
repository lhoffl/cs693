using UnityEngine;
public interface IState {
    void Enter(StateMachine stateMachine);
    void Exit();
    void Tick();
    void HandleCollision(Collision2D other);
    void HandleInput();
    void HandleTrigger(Collider2D other);
}