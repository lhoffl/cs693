using UnityEngine;
using UnityEngine.UI;
public class CanAttack : Node {
    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;

        if(context.target == null) return Status.FAILURE;
        if(context.me.Weapon == null) return Status.FAILURE;
        if(context.me.Weapon.CanAttack) return Status.SUCCESS;
        
        return Status.FAILURE;
    }

    protected override void OnReset() {}
}