using System;
using UnityEngine;
[RequireComponent(typeof(Entity))]
public class StateMachine : MonoBehaviour {
    Entity _entity;
    public IState _currentState;
    
    void Awake() {
        _entity = GetComponent<Entity>();
        EnterState(new SearchState());
    }

    public Entity GetEntity() => _entity;

    public void EnterState(IState state) {
        if (state == null) return;
        if(_currentState == state) return;
        
        if(_currentState != null)
            _currentState.Exit();
        
        _currentState = state;
        _currentState.Enter(this);
    }

    void Update() => _currentState.Tick();

    void OnCollisionEnter2D(Collision2D other) => _currentState.HandleCollision(other);

    void OnTriggerEnter2D(Collider2D other) => _currentState.HandleTrigger(other);
}
