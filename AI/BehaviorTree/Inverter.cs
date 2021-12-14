public class Inverter : Decorator {
    public Inverter(Node child) : base(child) {}

    protected override Status OnTicked(BehaviorState state) {
        switch(child.Tick(state)) {
            case Status.RUNNING: return Status.RUNNING;
            case Status.SUCCESS: return Status.FAILURE;
            case Status.FAILURE: return Status.SUCCESS;
        }

        return Status.FAILURE;
    }

    protected override void OnReset() {}
}