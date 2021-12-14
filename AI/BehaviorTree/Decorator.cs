public abstract class Decorator : Node {
    protected Node child;
    public Decorator(Node node) => child = node;
}