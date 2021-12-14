public class Sequence : Composite {
    int current = 0;

    public Sequence(string name, params Node[] nodes) : base(name, nodes) {}

    protected override Status OnTicked(BehaviorState state) {
        Status status = children[current].Tick(state);

        switch(status) {
            case Status.SUCCESS:
                current++;
                break;
            case Status.FAILURE: return Status.FAILURE;
        }

        if(current >= children.Count) return Status.SUCCESS;
        else if(status == Status.SUCCESS) return OnTicked(state);

        return Status.RUNNING;
    }

    protected override void OnReset() {
        current = 0;
        
        foreach(Node child in children)
            child.Reset();
    }
}