public class StopMoving : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        context.me.CanMove = false;
        return Status.SUCCESS;
    }

    protected override void OnReset() {}
}
