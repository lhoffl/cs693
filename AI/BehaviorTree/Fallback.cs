public class Fallback : Composite {
    int current = 0;

    public Fallback(string name, params Node[] nodes) : base(name, nodes) {}

    protected override Status OnTicked(BehaviorState state) {
        if(current >= children.Count) return Status.FAILURE;

        Status status = children[current].Tick(state);

        switch(status) {
            case Status.SUCCESS: return Status.SUCCESS;

            case Status.FAILURE:
                current++;
                return OnTicked(state);
        }

        return Status.RUNNING;
    }

    protected override void OnReset() {
        current = 0;
        foreach(Node child in children)
            child.Reset();
    }
}