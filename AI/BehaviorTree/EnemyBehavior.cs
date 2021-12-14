using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class EnemyBehavior : MonoBehaviour {

    [SerializeField] float _targetDistance = 2f;
    [SerializeField] string _tag = "Player";
    [SerializeField] BehaviorType _type = BehaviorType.NORMAL;
    
    Node _behaviorTree;
    Node _interactionTree;
    Context _behaviorState;

    void Start() {
        _behaviorTree = BehaviorTree(_type);
        _behaviorState = new Context();
        _behaviorState.me = GetComponent<Entity>();
        _behaviorState.tag = _tag;

        _interactionTree = InteractionTree();
    }

    void FixedUpdate() {
        _behaviorTree.Tick(_behaviorState);
        _interactionTree.Tick(_behaviorState);
    }

    Node BehaviorTree(BehaviorType type) {

        Sequence dead = new Sequence("dead",
            new Inverter(new Alive()));
        
        Sequence patrol = new Sequence("patrol",
            new HasMoveTarget(),
            new Inverter(new HasTarget()),
            new Inverter(new CloseToTarget()),
            new Inverter(new EnemyNearby(_targetDistance)),
            new Move());

        Sequence moveToAttack = new Sequence("moveToAttack",
            new HasMoveTarget(),
            new HasTarget(),
            new Inverter(new CloseToTarget()),
            new Move());

        Fallback move = new Fallback("move",
            moveToAttack,
            patrol);
        
       Sequence wait = new Sequence("wait",
            new Wait());

        Sequence attackTarget = new Sequence("attackTarget",
            new HasTarget(),
            new CanAttack(),
            new FaceTarget(),
            new StopMoving(),
            new AttackTarget(),
            new Wait(),
            new RemoveTarget());

        Fallback chooseTarget = new Fallback("chooseTarget",
            new EnemyNearby(_targetDistance),
            new RandomMoveTarget(_targetDistance * 3));

        Fallback normal = new Fallback("normal",
            dead,
            attackTarget,
            move,
            chooseTarget);

        Repeater normalRepeater = new Repeater(normal);
        return normalRepeater;
    }
    
    Node InteractionTree() {
        Repeater repeater = new Repeater(new HandleInteraction());
        return repeater;
    }
}

public enum BehaviorType {
    PASSIVE,
    NORMAL,
    AGGRESSIVE
}