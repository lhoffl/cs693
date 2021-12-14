using UnityEngine;

public class Wait : Node {

    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        context.me.CanMove = false;

        if(ticks > 60) {
            context.me.CanMove = true;
            context.target = null;
            context.moveTarget = null;
            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }

    protected override void OnReset() {}
}