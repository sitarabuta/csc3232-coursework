using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : BaseState
{
    private readonly BeeFlockStateMachine sm;

    public ReturnState(BeeFlockStateMachine stateMachine) : base(stateMachine)
    {
        sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        sm.target = sm.gameObject;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        bool wasDetected = false;
        for (int i = 0; i < sm.flockUnits.Length; i++)
        {
            if (Vector2.Distance(sm.player.transform.position, sm.flockUnits[i].transform.position) < sm.detectionRadius)
            {
                sm.target = sm.player;
                wasDetected = true;
                break;
            }
        }

        if (wasDetected)
            sm.ChangeState(sm.chaseState);

        for (int i = 0; i < sm.flockUnits.Length; i++)
        {
            if (Vector2.Distance(sm.target.transform.position, sm.flockUnits[i].transform.position) > sm.reachMargin && wasDetected)
            {
                Vector2 direction = sm.target.transform.position - sm.flockUnits[i].transform.position;
                direction = direction + new Vector2(Random.Range(0f, 0.25f) * (Random.value < 0.5f ? 1 : -1), Random.Range(0f, 0.25f) * (Random.value < 0.5f ? 1 : -1));
                sm.flockUnits[i].transform.Translate(direction * Time.deltaTime);

                if (direction.x > 0.1f)
                    sm.flockUnits[i].GetComponent<SpriteRenderer>().flipX = false;
                else if (direction.x < -0.1f)
                    sm.flockUnits[i].GetComponent<SpriteRenderer>().flipX = true;
            }
            for (int j = 0; j < sm.flockUnits.Length; j++)
            {
                if (sm.flockUnits[i] != sm.flockUnits[j])
                {
                    float distance = Vector2.Distance(sm.flockUnits[i].transform.position, sm.flockUnits[j].transform.position);
                    if (distance <= sm.spaceBetween)
                    {
                        Vector2 direction = sm.flockUnits[j].transform.position - sm.flockUnits[i].transform.position;
                        sm.flockUnits[j].transform.Translate(direction * Time.deltaTime * (5f - direction.normalized.magnitude));
                    }
                }
            }
        }

        if (wasDetected)
            sm.ChangeState(sm.chaseState);

        /*// Move towards next waypoint
        _sm.enemy.position = Vector2.MoveTowards(_sm.enemy.position, _sm.waypoints[_currentWaypoint].position, _sm.moveSpeed * Time.deltaTime);

        // Set enemy's animation
        _sm.ChangeAnimationState("Walking_Enemy_Walk");*/
    }
}
