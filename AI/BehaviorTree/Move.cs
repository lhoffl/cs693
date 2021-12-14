using UnityEngine;

public class Move : Node {

    BehaviorState _state;
    bool _hitSomething = false;
    float _duration;
    float _collisionTime = 0f;
    float _initialCollision = 0f;

    Entity _me;
    
    public Move(float duration = 75) => _duration = duration;

    protected override Status OnTicked(BehaviorState state) {
        _state = state;
        Context context = (Context) state;
        _me = context.me;
        context.me.CanMove = true;

        if (context.moveTarget == null || ticks > _duration) {
            OnExit();
            return Status.FAILURE;
        }
        
        if (_hitSomething && _collisionTime == 0) {
            context.moveTarget *= -1;
            _collisionTime = ticks;
            _initialCollision = _collisionTime;
        }

        if (_hitSomething && _collisionTime != 0)
            _collisionTime++;

        if (_collisionTime >= _initialCollision + _duration / 8) {
            OnExit();
            return Status.FAILURE;
        }

        if (isStopped)
            context.me.OnTriggered += HandleTrigger;
        
        
        Vector3 moveTarget = Vector3.Normalize((Vector3) context.moveTarget);
        context.me.Move(moveTarget, context.me.Speed);
         
        return Status.RUNNING;
    }

    void HandleCollision(Collision2D other) => _hitSomething = true;
    void HandleTrigger(Collider2D other) {
        if (other.GetComponent<Projectile>() != null)
            _me.TakeHit(other.GetComponent<Projectile>());
        
        _hitSomething = true;
    }

    void OnExit() {
        Context context = (Context) _state;
        context.me.OnTriggered -= HandleTrigger;
        context.moveTarget = null;
    }

    protected override void OnReset() {
        _collisionTime = 0;
        _initialCollision = 0;
        _hitSomething = false;
    }
}