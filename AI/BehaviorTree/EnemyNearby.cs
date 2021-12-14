using System;
using UnityEngine;

public class EnemyNearby : Node {
    float _distanceThreshold;

    public EnemyNearby(float threshold) => _distanceThreshold = threshold;

    protected override Status OnTicked(BehaviorState state) {
        Context context = (Context) state;

        Collider2D[] sensed = new Collider2D[10];
        sensed = context.me.Sense(_distanceThreshold);

        if(sensed == null) { 
            context.target = null;
            return Status.FAILURE;
        }

        for(int i = 0; i < sensed.Length; i++) {
            if(sensed[i] == null) break;
            if(sensed[i].CompareTag(context.tag)) {
                context.target = sensed[i].transform.position;
                return Status.SUCCESS;
            }
        }

        context.target = null;

        return Status.FAILURE;
    }

    protected override void OnReset() {}
}