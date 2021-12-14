using UnityEngine;
public class AttackState : IState {
    StateMachine _stateMachine;
    Vector3 _position;

    public AttackState(Vector3 position) => _position = position;

    public void Enter(StateMachine stateMachine) {
        _stateMachine = stateMachine;
        Entity entity = _stateMachine.GetEntity();
        entity.Attack();
        float randomX = UnityEngine.Random.Range(-10f, 10f);
        float randomY = UnityEngine.Random.Range(-10f, 10f);
        _stateMachine.EnterState(new MoveState(new Vector2(randomX, randomY))); 
    }
    
    public void Exit() {}

    public void Tick() {}
    
    public void HandleCollision(Collision2D other) {}
    
    public void HandleInput() {}
    
    public void HandleTrigger(Collider2D other) {
        IDamage damagedBy = other.GetComponent<IDamage>();
        if (damagedBy == null) return;
        _stateMachine.EnterState(new TakeDamageState(damagedBy));
    }
}
