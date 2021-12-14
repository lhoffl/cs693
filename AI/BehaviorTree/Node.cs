using UnityEngine;

public abstract class Node {

    protected bool isStopped = true;
    protected int ticks = 0;

    protected abstract Status OnTicked(BehaviorState state);
    protected abstract void OnReset();
    
    public void Reset() {
        OnReset();
        ticks = 0;
        isStopped = true;
    }

    public virtual Status Tick(BehaviorState state) {
        Status status = OnTicked(state);
        
        isStopped = false;
        ticks++;

        if(status != Status.RUNNING) Reset();
        return status;
    }
}
