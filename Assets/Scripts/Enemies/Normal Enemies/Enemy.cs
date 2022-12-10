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

    float attackingTime = 0;

    bool punchAnim = false;

    // Update is called once per frame
    public override void Update()
    {
        if (isInvincible)
        {
            return;
        }
        base.Update();

        if(attackingTime <= 0)
        {
            if(Vector3.Distance(PlayerObject.Instance.transform.position, transform.position) < 2.5f)
            {
                if (punchAnim)
                {
                    animator.SetTrigger("Punch1");
                    punchAnim = false;
                }
                else
                {
                    animator.SetTrigger("Punch2");
                    punchAnim = true;
                }
                CastAttack(0);
                attackingTime = 4;
            }
        }
        else
        {
            attackingTime -= Time.deltaTime;
        }
    }
    public override void FixedUpdate()
    {
        if (isInvincible)
        {
            return;
        }
        base.FixedUpdate();
        UpdatePosition();
    }
    void UpdatePosition()
    {

        if(Vector3.Distance(PlayerObject.Instance.transform.position, transform.position) < 2 * range)
        {
            state = AIState.Attacking;
        }

        if(state == AIState.Attacking)
        {
            if (Random.Range(0, 1000) < 30)
            {
                movementTarget = PlayerObject.Instance.transform.position;
            }

            if (Random.Range(0, 1000) < 4)
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
                    animator.SetBool("Moving", true);
                    rb.MovePosition(transform.position + (dir * speed * Time.deltaTime));
                }
                else
                {
                    animator.SetBool("Moving", false);
                }
                if (rb.position.x > PlayerObject.Instance.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.GetChild(2).rotation = Quaternion.Euler(80, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.identity;
                    transform.GetChild(2).rotation = Quaternion.Euler(80, 180, 0);
                }
            }
            else if (state == AIState.RoamingAround)
            {
                //Slow walking animation
                animator.SetBool("Moving", true);
                rb.MovePosition(transform.position + (dir * 1 * Time.deltaTime));
            }
            else if (state == AIState.Flee)
            {
                //Flee animation
                animator.SetBool("Moving", true);
                rb.MovePosition(transform.position + (dir * speed * Time.deltaTime));
            }
            else
            {
                //Idle animation
                animator.SetBool("Moving", false);
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
