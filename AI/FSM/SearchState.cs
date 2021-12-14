using UnityEngine;

public class SearchState : IState {
    StateMachine _stateMachine;
    Entity _entity;
    
    public void Enter(StateMachine stateMachine) {
        _stateMachine = stateMachine;
        _entity = _stateMachine.GetEntity();
    }

    public void Tick() => Search();

    void Search() {
        Collider2D[] hitColliders = _entity.Sense();
        
        foreach (Collider2D collider in hitColliders) {
            if (collider != null) {
                Player player = collider.GetComponent<Player>();
                if (player != null) {
                    //_stateMachine.EnterState(new MoveState(player.transform.position));
                    return;
                }
            }
        }
        
        MoveRandom();
    }
    
    public void HandleTrigger(Collider2D other) {
        IDamage damagedBy = other.GetComponent<IDamage>();
        if (damagedBy == null) return;
        _stateMachine.EnterState(new TakeDamageState(damagedBy));
    }

    public void HandleCollision(Collision2D other) {}

    public void Exit() {}
    public void HandleInput() {}

    void MoveRandom() {
        float random = UnityEngine.Random.Range(-10f, 10f);
        if((int)random % 2 == 0)
            _stateMachine.EnterState(new MoveState(new Vector2(random, _entity.transform.position.y)));
        else    
            _stateMachine.EnterState(new MoveState(new Vector2(_entity.transform.position.x, random)));
    }
}