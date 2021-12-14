using UnityEngine;
class DeadState : IState {
    public void Enter(StateMachine stateMachine) {}
    public void Exit() {}
    public void Tick() {}
    public void HandleCollision(Collision2D other) {}
    public void HandleInput() {}
    public void HandleTrigger(Collider2D other) {}
}
