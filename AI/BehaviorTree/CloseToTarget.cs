using UnityEngine;

public class CloseToTarget : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;

        if(context.moveTarget == null) return Status.FAILURE;
        if (AtDestination(context)) {
            context.moveTarget = null;
            return Status.SUCCESS;
        }
        
        return Status.FAILURE;
    }

    bool AtDestination(Context context) {
        Vector3 moveTarget = (Vector3)context.moveTarget;
        if (moveTarget.x == 0)
            moveTarget.x = context.me.transform.position.x;
        else if (moveTarget.y == 0)
            moveTarget.y = context.me.transform.position.y;
        
        return (Vector2.Distance(context.me.transform.position, (Vector2)context.moveTarget) < 2.0f);
    }

    protected override void OnReset() {}
}