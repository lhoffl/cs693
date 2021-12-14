using System.Collections.Generic;

public abstract class Composite : Node {
    protected List<Node> children = new List<Node>();
    public string name;

    public Composite(string name, params Node[] nodes) {
        this.name = name;
        children.AddRange(nodes);
    }

    public override Status Tick(BehaviorState state) {
        Status status = base.Tick(state);
        return status;
    }
}