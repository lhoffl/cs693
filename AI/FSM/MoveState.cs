using Newtonsoft.Json.Serialization;
using TMPro;
using UnityEngine;
class MoveState : IState {
    Vector3 _position;
    StateMachine _stateMachine;
    Entity _entity;

    float _threshold = 0.1f;
    float _maxTime = 30f;
    float _currentTime = 0f;

    public MoveState(Vector3 position) => _position = position;
    
    public void Enter(StateMachine stateMachine) {
        _stateMachine = stateMachine;
        _entity = _stateMachine.GetEntity();
    }

    public void Exit() {}

    public void Tick() => Move();

    public void HandleCollision(Collision2D other) {
        if(_entity.transform.position.x == _position.x)
            _position = new Vector3(_entity.transform.position.x, _position.y * -1);
        if(_entity.transform.position.y == _position.y)
            _position = new Vector3(_position.x * -1, _entity.transform.position.y);
    }
    
    public void HandleTrigger(Collider2D other) {
        IDamage damagedBy = other.GetComponent<IDamage>();
        if (damagedBy == null) return;
        _stateMachine.EnterState(new TakeDamageState(damagedBy));
    }
    
    public void HandleInput() {}

    void Move() {

        Vector3 normalizedPosition = Vector3.Normalize(_position - _entity.transform.position);
        _entity.Move(normalizedPosition);

        if (Vector2.Distance(_entity.transform.position, _position) <= _threshold) {
            _entity.transform.position = _position;
            _stateMachine.EnterState(new SearchState());
        }

        _currentTime += Time.fixedDeltaTime;
        
        if (_currentTime >= _maxTime) {
            Debug.Log("timed out");
            _stateMachine.EnterState(new SearchState());
        }
    }

    bool CheckForNearbyEnemies() {
        Collider2D[] hitColliders = _entity.AttackSense();
        
        foreach (Collider2D collider in hitColliders) {
            if (collider != null) {
                Player player = collider.GetComponent<Player>();
                if (player != null) {
                    Debug.Log("About to attack");
                    _stateMachine.EnterState(new AttackState(player.transform.position));
                    return true;
                }
            }
        }

        return false;
    }
}