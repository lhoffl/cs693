public class Alive : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        if (!context.me.Alive) return Status.FAILURE;
        
        return Status.SUCCESS;
    }
    protected override void OnReset() {}
}
