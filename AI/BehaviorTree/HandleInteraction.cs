using UnityEngine;
public class HandleInteraction : Node {
    BehaviorState _state;
    Entity _me;
    
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        _me = context.me;
        _state = state;
        
        if (!_me.Alive) return Status.FAILURE;
        
        if (isStopped) {
            context.me.OnCollision += HandleCollision;
            context.me.OnTriggered += HandleTrigger;
        }
        
        return Status.RUNNING;
    }
    void HandleTrigger(Collider2D other) {
        if (other.GetComponent<Projectile>() != null)
            _me.TakeHit(other.GetComponent<Projectile>());
    }
    void HandleCollision(Collision2D other) {}
    
    void OnExit() {
        Context context = (Context) _state;
        context.me.OnTriggered -= HandleTrigger;
        context.me.OnCollision -= HandleCollision;
    }

    protected override void OnReset() => OnExit();
}
