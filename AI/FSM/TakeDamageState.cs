using UnityEngine;
class TakeDamageState : IState {
    StateMachine _stateMachine;
    Entity _entity;

    float _invincibilityTimer = 3f;
    float _currentTimer = 0f;
    IDamage _damagedBy;

    public TakeDamageState(IDamage damagedBy) => _damagedBy = damagedBy;

    public void Enter(StateMachine stateMachine) {
        _stateMachine = stateMachine;
        _entity = _stateMachine.GetEntity();
        _entity.TakeHit(_damagedBy);
        if(!_entity.Alive)
            _stateMachine.EnterState(new DeadState());
    }
    
    public void Exit() {}

    public void Tick() {
        _currentTimer += Time.fixedDeltaTime;
        if (_currentTimer >= _invincibilityTimer) {
            float random = UnityEngine.Random.Range(-10f, 10f);
            float randomDir = UnityEngine.Random.Range(0f, 10f);
            
            if(random % 2 == 0)
                _stateMachine.EnterState(new MoveState(new Vector2(_entity.transform.position.x, random)));
            else 
                _stateMachine.EnterState(new MoveState(new Vector2(_entity.transform.position.x, random))); 
                _stateMachine.EnterState(new MoveState(new Vector2(random, _entity.transform.position.y))); 
        }
    }
    public void HandleCollision(Collision2D other) {}
    public void HandleInput() {}
    public void HandleTrigger(Collider2D other) {}
}