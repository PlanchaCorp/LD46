using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float wanderingRadius;
    [SerializeField]
    private float minWaitingTime = 3;
    [SerializeField]
    private float maxWaitingTime = 7;
    private DodoManager dodoManager;
    private float nextMoveTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextMoveTime = Time.time;
        dodoManager = animator.GetComponent<DodoManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Wander();
        if (dodoManager.luringMachines.Count > 0)
        {
            foreach(LuringMachineAbstract machine in dodoManager.luringMachines)
            {
                if (machine.IsDodoLured(dodoManager))
                {
                    dodoManager.luringMachine = machine;
                    dodoManager.stateMachine.SetTrigger("lure");
                    return;
                }
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
            dodoManager.setObjective(GetNewGoal());  
        }
    }
    
    private Vector2 GetNewGoal() {
        Vector2 pos = dodoManager.GetComponent<Transform>().position;
        float angle = Random.Range(0,360);
        float x = Mathf.Cos(angle) * wanderingRadius;
        float y = Mathf.Sin(angle) * wanderingRadius;
        return new Vector2(x,y) + pos;
    }
}
