using UnityEngine;
public class HasMoveTarget : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        if(context.moveTarget == null) return Status.FAILURE;
        return Status.SUCCESS;
    }
    protected override void OnReset() {}
}
