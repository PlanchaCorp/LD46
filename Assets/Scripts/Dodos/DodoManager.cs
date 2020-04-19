using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodoManager : MonoBehaviour
{
    public const float RELAX_TIME_MIN = 10; // Time before the dodo can relax itself after eating
    public const float RELAX_TIME_MAX = 40; // Time the dodo can go without going to the litter
    private const float HUNGER_INCREASE = 0.005f; // How many point hunger loses by second

    public SpaceStationManager spaceStationManager { get; set; }
    public List<LuringMachineAbstract> luringMachines { get; set; }
    [HideInInspector]
    public LuringMachineAbstract luringMachine;

    [SerializeField]
    private float speed;
    [SerializeField]
    public float hunger = 0; // Hunger ratio, from 0 (no hunger) to 1.2 (death)
    [SerializeField]
    public float mealTimeAgo = 0; // Time since the dodo has last eaten, if he needs to go to the litter

    public Animator stateMachine { get; set; }
    private Rigidbody2D rb;

    private Vector2 goal;

    private Vector2 pushVector;

    public bool canMove = true;

    void Start()
    {
        stateMachine = GetComponent<Animator>();
        luringMachines = new List<LuringMachineAbstract>();
        rb = GetComponent<Rigidbody2D>();
        GameObject spaceStation = GameObject.FindWithTag("SpaceStation");
        if (spaceStation == null)
        {
            Debug.LogError("Unable to find SpaceStation gameObject to register \"" + gameObject.name + "\" machine!");
        } else {
            spaceStationManager = spaceStation.GetComponent<SpaceStationManager>();
        }
    }

    void Update() {
        hunger += HUNGER_INCREASE * Time.deltaTime;
        if(canMove) {
            Vector2 newPos = Vector2.MoveTowards(transform.position,goal, speed * Time.deltaTime);
            rb.MovePosition(newPos);
        } 
        if (mealTimeAgo > 0)
        {
            mealTimeAgo += Time.deltaTime;
            if (mealTimeAgo > RELAX_TIME_MAX)
            {
                mealTimeAgo = 0;
                Debug.Log("Oh no! The dodo could not take it anymore!");
                // TODO: Implement dodo relaxing animation
            }
        }
    }

    public void setObjective(Vector2 newObjective){
        this.goal = newObjective;
        float angle = (Mathf.Atan2(goal.y-transform.position.y, goal.x- transform.position.x) * Mathf.Rad2Deg) -90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void PushInDirection(Vector2 pushVector){
        this.pushVector = pushVector;
        stateMachine.SetTrigger("stun");
        goal = pushVector;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.CompareTag("Conveyer")){
            stateMachine.SetBool("Conveyed",true);
        }
        LuringMachineAbstract machine = collision.gameObject.GetComponentInParent<LuringMachineAbstract>();
        if (machine != null)
        {
        Debug.Log(machine.name);
            luringMachines.Add(machine);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject machineObject = collision.gameObject.GetComponentInParent<Transform>().gameObject;
        if (machineObject.CompareTag("Machine"))
        {
            LuringMachineAbstract machine = machineObject.GetComponent<LuringMachineAbstract>();
            if (machine != null)
            {
                luringMachines.Remove(machine);
            }
        }
           if(collision.CompareTag("Conveyer")){
            stateMachine.SetBool("Conveyed",false);
        }
    }
}
