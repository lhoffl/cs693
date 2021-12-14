public class RemoveTarget : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        context.target = null;
        context.moveTarget = null;
        return Status.SUCCESS;
    }
    protected override void OnReset() {}
}
