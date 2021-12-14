using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;

public class ProjectileAttacker : MonoBehaviour, IDamage {

    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] float _launchYOffset = 3f;
    [SerializeField] float _launchDelay = 1f;
    [SerializeField] float _cooldown = 600f;

    float _currentTimer = 0f;

    public int Damage => 1;
    public bool CanAttack => _currentTimer > _cooldown;

    void Update() => _currentTimer++;

    public void Attack(Vector2 direction) {
        _currentTimer = 0;
        StartCoroutine(LaunchAfterDelay(direction));
    }

    IEnumerator LaunchAfterDelay(Vector2 direction) {
        yield return new WaitForSeconds(_launchDelay);
        
        Projectile projectile = (Projectile)GameObject.Instantiate(_projectilePrefab);
        projectile.transform.position = transform.position + (Vector3) direction * _launchYOffset;
        projectile.transform.rotation = quaternion.identity;
        
        projectile.SetDirection(direction);
    }
}
