using UnityEngine;
public class RandomMoveTarget : Node {

    float _range;

    public RandomMoveTarget(float range) => _range = range;

    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;
        if (context.moveTarget != null) return Status.FAILURE;
        context.moveTarget = PickRandomDirection();
        return Status.SUCCESS;
    }
    
    Vector2 PickRandomDirection() {
        float r = UnityEngine.Random.Range(-_range, _range);
        float randomDirection = UnityEngine.Random.Range(-_range, _range);
        Vector2 moveTo = r > 0 ? new Vector2(randomDirection, 0) : new Vector2(0, randomDirection);
        Mathf.Clamp(moveTo.x, -8, 8);
        Mathf.Clamp(moveTo.y, -4, 4);
        return moveTo;
    }

    protected override void OnReset() {}
}
