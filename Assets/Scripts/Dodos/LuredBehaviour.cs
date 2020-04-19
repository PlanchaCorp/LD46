using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuredBehaviour : StateMachineBehaviour
{
    public const float LURE_TIMEOUT = 6;
    private DodoManager dodoManager;
    private LuringMachineAbstract luringMachine;
    private Transform occupation;
    private bool isRigid;
    private float occupationFinishTime = 0;
    private float occupationTime;
    private float lureTimeout;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lureTimeout = Time.time + LURE_TIMEOUT;
        occupationFinishTime = 0;
        dodoManager = animator.GetComponent<DodoManager>();
        luringMachine = dodoManager.luringMachine;
        Transform machineChild  = luringMachine.transform.GetChild(0);
        if (machineChild != null) 
        {
            int occupationCount = machineChild.childCount;
            if (occupationCount > 0)
            {
                int occupationNumber = Random.Range(0, occupationCount);
                occupation = machineChild.GetChild(occupationNumber);
            }

        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 dodoToMachine = occupation.position - dodoManager.transform.position;
        if (dodoToMachine.magnitude < 0.05f)
        {
            if (occupationFinishTime == 0)
            {
                dodoManager.transform.rotation = occupation.rotation;
                luringMachine.StartInteraction(dodoManager);
                occupationFinishTime = Time.time + luringMachine.occupationTime;
            } else if (Time.time > occupationFinishTime) {
                if (luringMachine != null)
                {
                    luringMachine.FinishInteraction(dodoManager);
                    luringMachine = null;
                }
                dodoManager.stateMachine.SetTrigger("delure");
            }
        } else if (Time.time > lureTimeout) {
            dodoManager.stateMachine.SetTrigger("delure");
        } else {
            dodoManager.transform.Translate(dodoToMachine * Time.deltaTime * dodoManager.speed, Space.World);
            float angle = Mathf.Atan2(-dodoToMachine.y, -dodoToMachine.x) * Mathf.Rad2Deg;
            dodoManager.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (luringMachine != null)
            luringMachine.CancelInteraction(dodoManager);
        //dodoManager.canMove = true;
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
}
