using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamage {
    [SerializeField] float _moveSpeed = 10f;
    Vector3 _direction;
    
    public int Damage => 1;

    void Update() => transform.position += _direction * _moveSpeed * Time.deltaTime;

    public void SetDirection(Vector3 direction) => _direction = direction;

    void OnCollisionEnter2D(Collision2D other) {
        var hit = other.collider.GetComponent<ITakeHit>();
        if(hit != null) Impact(hit);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        var hit = other.GetComponent<ITakeHit>();
        if(hit != null) Impact(hit);
        gameObject.SetActive(false);
    }

    void Impact(ITakeHit hit) {
        hit.TakeHit(this);
        gameObject.SetActive(false);
    }
}
