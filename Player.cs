using UnityEngine;
using UnityEngine.PlayerLoop;
public class Player : Entity {
    Entity _entity;

    void Awake() => _entity = GetComponent<Entity>();

    void Update() {
        if (Input.GetKey(KeyCode.W)) 
            _entity.Move(Vector2.up);
        if (Input.GetKey(KeyCode.S))
            _entity.Move(Vector2.down);
        if (Input.GetKey(KeyCode.A))
            _entity.Move(Vector2.left);
        if (Input.GetKey(KeyCode.D))
            _entity.Move(Vector2.right);
        if (Input.GetKeyDown(KeyCode.Space))
            _entity.Attack();
    }
}
