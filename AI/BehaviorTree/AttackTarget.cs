using UnityEngine;

public class AttackTarget : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;

        if(context.target == null) return Status.FAILURE;
        if(context.me.Weapon == null) return Status.FAILURE;
        
        context.me.Attack();
        context.timeOfLastAttack = Time.time;
        return Status.SUCCESS;
    }

    protected override void OnReset() {}
}