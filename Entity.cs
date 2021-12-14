using System;
using TMPro;
using UnityEngine;
public class Entity : MonoBehaviour, ITakeHit, IDie {
    [SerializeField] int _maxHealth = 3;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _sightRange = 10f;
    [SerializeField] float _attackRange = 6f;
    [SerializeField] ProjectileAttacker _attack;

    [SerializeField] BoxCollider2D _front;
    
    public event Action<int, int> OnHealthChanged = delegate {};
    public event Action<Collision2D> OnCollision = delegate {};
    public event Action<Collider2D> OnTriggered = delegate {};
    public event Action<IDie> OnDied = delegate {};
    public event Action OnHit = delegate {};

    public ProjectileAttacker Weapon => _attack;
    public float Speed => _speed;
    
    public Vector3 StartingPosition { get; private set; }
    public bool Alive { get; private set; }
    public bool CanMove { get; set; }
    public Animator Animator => _animator;

    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _renderer;
    
    int _currentHealth;
    Vector2 _facingDirection;

    public Collider2D[] Sense(float range) => Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range);
    public Collider2D[] Sense() => Sense(_sightRange);
    public Collider2D[] AttackSense() => Sense(_attackRange);

    public void Die() {
        Alive = false;
        CanMove = false;
        OnDied(this);
        gameObject.SetActive(false);
    }

    public void UpdateRotation(Vector2 direction) {
        if (direction == Vector2.up) {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            _facingDirection = Vector2.up;
        }
        if (direction == Vector2.down) {
            transform.localEulerAngles = new Vector3(0, 0, 180);
            _facingDirection = Vector2.down;
        }
        if (direction == Vector2.left) {
            transform.localEulerAngles = new Vector3(0, 0, 90);
            _facingDirection = Vector2.left;
        }
        if (direction == Vector2.right) {
            transform.localEulerAngles = new Vector3(0, 0, 270);
            _facingDirection = Vector2.right;
        }
    }
    
    public void Attack() {
        if(_attack != null) _attack.Attack(_facingDirection); 
    }

    public void Move(Vector2 moveTo, float maxSpeed) {
        if(!CanMove) return;
        if(moveTo == null) return;
        
        UpdateRotation(moveTo);
        transform.position += (Vector3)moveTo * _speed * Time.fixedDeltaTime;
    }

    public void Move(Vector2 moveTo) => Move(moveTo, _speed);

    public void TakeHit(IDamage hitBy) {
        OnHit();
        ModifyHealth(-hitBy.Damage);
    }

    public bool WithinAttackRange(Vector3 position) => Vector3.Distance(position, transform.position) <= _attackRange;

    void ModifyHealth(int modifier) {
        _currentHealth += modifier;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnHealthChanged(_currentHealth, modifier);

        if(_currentHealth <= 0) Die();
    }

    void OnCollisionEnter2D(Collision2D other) => OnCollision?.Invoke(other);

    void OnTriggerEnter2D(Collider2D other) => OnTriggered?.Invoke(other);

    void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        _currentHealth = _maxHealth;
        Alive = true;
        CanMove = true;
    }
    
    public void FaceDirection(Vector3 contextTarget) {
        Vector2 direction;
        if(transform.position.x - contextTarget.x < 0)
            direction = Vector2.left;
        if(transform.position.x - contextTarget.x >= 0)
            direction = Vector2.right;
        if(transform.position.y - contextTarget.y >= 0)
            direction = Vector2.up;
        else
            direction = Vector2.down;

        UpdateRotation(direction);
    }
}