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
    public float speed = 1;
    [SerializeField]
    public float hunger = 0; // Hunger ratio, from 0 (no hunger) to 1.2 (death)
    [SerializeField]
    public float mealTimeAgo = 0; // Time since the dodo has last eaten, if he needs to go to the litter

    public Animator stateMachine { get; set; }
    private Rigidbody2D rb;

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
        if (mealTimeAgo > 0)
        {
            mealTimeAgo += Time.deltaTime;
            if (mealTimeAgo > RELAX_TIME_MAX)
            {
                mealTimeAgo = 0;
                Debug.Log("Oh no! The dodo could not take it anymore!");
                GameObject animation = Instantiate(Resources.Load<GameObject>("DisgraceAnimation"));
                if (animation == null) {
                    Debug.LogError("Could not find DisgraceAnimation prefab in Resources folder!");
                } else {
                    animation.transform.parent = transform;
                    animation.transform.position = transform.position + new Vector3(0.4f, 0.6f, 0);
                }
            }
        }
    }

    public void PushInDirection(Vector2 pushVector, float pushForce) {
        rb.AddForce(pushVector.normalized * pushForce);
        stateMachine.SetTrigger("stun");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.CompareTag("Conveyer")){
            stateMachine.SetBool("Conveyed", true);
        }
        LuringMachineAbstract machine = collision.gameObject.GetComponentInParent<LuringMachineAbstract>();
        if (machine != null)
        {
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
        if(collision.CompareTag("Conveyer")) {
            stateMachine.SetBool("Conveyed",false);
        }
    }
}
