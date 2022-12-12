using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private readonly BeeFlockStateMachine sm;
    private float leftBound;
    private float rightBound;
    private bool isFacingRight;

    public IdleState(BeeFlockStateMachine stateMachine) : base(stateMachine)
    {
        sm = stateMachine;

        leftBound = sm.gameObject.transform.position.x - 2f;
        rightBound = sm.gameObject.transform.position.x + 2f;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("test");

        bool wasDetected = false;
        for (int i = 0; i < sm.flockUnits.Length; i++)
        {
            if (isFacingRight)
            {
                sm.flockUnits[i].transform.position = new Vector2(sm.flockUnits[i].transform.position.x + Time.deltaTime * 5f, sm.flockUnits[i].transform.position.y);
            }
            else
            {
                sm.flockUnits[i].transform.position = new Vector2(sm.flockUnits[i].transform.position.x - Time.deltaTime * 5f, sm.flockUnits[i].transform.position.y);
            }

            if (sm.flockUnits[i].transform.position.x > rightBound && isFacingRight)
            {
                isFacingRight = false;
                sm.flockUnits[i].GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (sm.flockUnits[i].transform.position.x < leftBound && !isFacingRight)
            {
                isFacingRight = true;
                sm.flockUnits[i].GetComponent<SpriteRenderer>().flipX = false;
            }

            if (Vector2.Distance(sm.player.transform.position, sm.flockUnits[i].transform.position) < sm.detectionRadius)
            {
                sm.target = sm.player;
                wasDetected = true;
                break;
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
