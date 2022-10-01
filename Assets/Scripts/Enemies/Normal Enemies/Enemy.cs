using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitObject
{
    public Vector3 movementTarget;

    public Vector2 targetDisplacement;

    public AIState state;
    public float AIStateDuration;

    public Vector2 zBounds;
    public float range = 2.5f;


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdatePosition();
    }
    void UpdatePosition()
    {

        if(Vector3.Distance( PlayerObject.Instance.transform.position, transform.position) < 2 * range)
        {
            state = AIState.Attacking;
        }

        if(state == AIState.Attacking)
        {
            if (Random.Range(0, 1000) < 30)
            {
                movementTarget = PlayerObject.Instance.transform.position;
            }

            if (Random.Range(0, 1000) < 5)
            {
                targetDisplacement = Random.onUnitSphere;
                targetDisplacement.y /= 2f;
                targetDisplacement = targetDisplacement.normalized;
                targetDisplacement *= range;
            }
        }
        if (movementTarget != null)
        {
            Vector3 dir = directionToTarget();
            if (state == AIState.Attacking)
            {
                //Fully walking animation
                if (Vector3.Distance(transform.position, actualTarget()) > 0.1f){
                    rb.MovePosition(transform.position + (Vector3)(dir * speed * Time.deltaTime));
                }
            }
            else if (state == AIState.RoamingAround)
            {
                //Slow walking animation
                rb.MovePosition(transform.position + (Vector3)(dir * 1 * Time.deltaTime));
            }
            else if (state == AIState.Flee)
            {
                //Flee animation
                rb.MovePosition(transform.position + (Vector3)(dir * speed * Time.deltaTime));
            }
            else
            {
                //Idle animation
            }
        }
    }

    Vector3 actualTarget()
    {
        return movementTarget + new Vector3(targetDisplacement.x, 0, targetDisplacement.y);
    }
    Vector3 directionToTarget()
    {
        Vector3 dir = actualTarget() - transform.position;
        dir = dir.normalized;

        return dir;
    }

}

public enum AIState
{
    Idle,
    RoamingAround,
    Attacking,
    Flee
}
