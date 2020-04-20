using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingBehaviour : StateMachineBehaviour
{
    private float wanderingRadius = 2.0f;
    private float minWaitingTime = 4;
    private float maxWaitingTime = 8;
    private float minMovingTime = 1;
    private float maxMovingTime = 2.5f;
    private DodoManager dodoManager;
    private float nextMoveTime;
    private float moveTime;
    private Vector2 moveDirection;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextMoveTime = Time.time;
        moveTime = Time.time;
        dodoManager = animator.GetComponent<DodoManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Wander();

        if (dodoManager.luringMachines.Count > 0)
        {
            int i = 0;
            foreach(LuringMachineAbstract machine in dodoManager.luringMachines)
            {
                if (machine.IsDodoLured(dodoManager))
                {
                    dodoManager.luringMachine = machine;
                    dodoManager.stateMachine.SetTrigger("lure");
                    return;
                }
                i++;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
    public void Wander() {
        if (Time.time > nextMoveTime) {
            nextMoveTime += Random.Range(minWaitingTime, maxWaitingTime);
            moveTime = Time.time + Random.Range(minMovingTime, maxMovingTime);
            moveDirection = (GetNewGoal() - dodoManager.transform.position).normalized;
        }
        if (Time.time < moveTime && dodoManager.canMove)
        {
            dodoManager.transform.Translate(moveDirection * Time.deltaTime * dodoManager.speed, Space.World);
            float angle = Mathf.Atan2(-moveDirection.y, -moveDirection.x) * Mathf.Rad2Deg;
            dodoManager.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }
    
    private Vector3 GetNewGoal() {
        Vector2 pos = dodoManager.transform.position;
        float angle = Random.Range(0,360);
        float x = Mathf.Cos(angle) * wanderingRadius;
        float y = Mathf.Sin(angle) * wanderingRadius;
        return new Vector2(x, y) + pos;
    }
}
