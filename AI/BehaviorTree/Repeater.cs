public class Repeater : Decorator {
    public Repeater(Node child) : base(child) {}

    protected override Status OnTicked(BehaviorState state) {
        Status status = child.Tick(state);

        if(status != Status.RUNNING) {
            Reset();
            child.Reset();
        }

        return Status.SUCCESS;
    }

    protected override void OnReset() {}
}