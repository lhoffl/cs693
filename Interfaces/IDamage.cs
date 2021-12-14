using UnityEngine;
public interface IDamage {
    Transform transform { get; }
    int Damage { get; }
    GameObject gameObject { get; }
}
