using UnityEngine;

public class Context : BehaviorState {
    public Entity me;
    public Vector3? target = null;
    public Vector2? moveTarget = null;
    public string tag;
    public float timeOfLastAttack = 0f;
}