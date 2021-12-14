using UnityEngine;
public class FaceTarget : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context)state;
        if (context.target == null) return Status.FAILURE;
        context.me.UpdateRotation(FacingDirection((Vector3)context.target, context.me.transform.position));
        return Status.SUCCESS;
    }

    Vector2 FacingDirection(Vector3 target, Vector3 self) {
        Vector2 direction;

        float xDistance = target.x - self.x;
        float yDistance = target.y - self.y;

        if (Mathf.Abs(xDistance) >= Mathf.Abs(yDistance)) {
            if(xDistance >= 0) direction = Vector2.right;
            else direction = Vector2.left;
        } 
        else {
            if(yDistance <= 0) direction = Vector2.down;
            else direction = Vector2.up;
        }
        
        return direction;
    }
    
    protected override void OnReset() {}
}
