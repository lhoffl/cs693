public class HasTarget : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        if(context.target == null) return Status.FAILURE;
        return Status.SUCCESS;
    }
    protected override void OnReset() {}
}