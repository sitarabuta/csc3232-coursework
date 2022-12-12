using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFlockStateMachine : BaseStateMachine
{
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public ReturnState returnState;

    public GameObject flockUnitPrefab;
    public GameObject player;
    public int numberOfUnits = 8;
    public float spaceBetween = 1f;
    public float spawnRadius = 5f;
    public float reachMargin = 1.5f;
    public float detectionRadius = 10f;

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public GameObject[] flockUnits;

    void Awake()
    {
        idleState = new IdleState(this);
        chaseState = new ChaseState(this);
        returnState = new ReturnState(this);

        flockUnits = new GameObject[numberOfUnits];
    }

    void Start()
    {
        for (int i = 0; i < numberOfUnits; i++)
        {
            flockUnits[i] = Instantiate(flockUnitPrefab, (Vector2)transform.position + Random.insideUnitCircle * spawnRadius, Quaternion.identity);
            flockUnits[i].transform.SetParent(gameObject.transform);
        }
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
