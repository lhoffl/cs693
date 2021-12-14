using UnityEngine;
public class EnemyMoveTarget : Node {

    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context)state;
        Vector3 target = (Vector3)context.target;
        if (target == null) return Status.FAILURE;

        float xDiff = Mathf.Abs(context.me.transform.position.x - target.x);
        float yDiff = Mathf.Abs(context.me.transform.position.y - target.y);

        if (context.target == null) return Status.FAILURE;
        context.moveTarget = xDiff > yDiff ? 
            new Vector2(target.x, 0) : 
            new Vector2(0, target.y); 
        
        return Status.SUCCESS;
    }
    protected override void OnReset() {}
}
