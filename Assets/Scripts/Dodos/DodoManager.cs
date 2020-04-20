using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodoManager : MonoBehaviour
{
    public const float RELAX_TIME_MIN = 10; // Time before the dodo can relax itself after eating
    public const float RELAX_TIME_MAX = 40; // Time the dodo can go without going to the litter
    private const float HUNGER_INCREASE = 0.005f; // How many point hunger loses by second
    public const float DODO_HUNGER = 60f;
    public const float DODO_STARVE = 120f;

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
    [SerializeField]
    public float readyToEgg = 0; // When reaching 0, dodo is ready to lay an egg

    public Animator stateMachine { get; set; }
    private Rigidbody2D rb;

    public bool canMove = true;
    public int hungerAnimationProgress = 0;

    private int onConveyer;

    void Start()
    {
        hungerAnimationProgress = 0;
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
        if (hunger > DODO_STARVE) 
        {
            // TODO: Die, dodo, die
        } else if (hunger > DODO_HUNGER + ((DODO_STARVE - DODO_HUNGER)/3) * 2 && hungerAnimationProgress < 2) {
            Animate("StarveAnimation", AnimationPosition.TopLeft);
            hungerAnimationProgress = 2;
        } else if (hunger > DODO_HUNGER && hungerAnimationProgress < 1) {                
            Animate("HungerAnimation", AnimationPosition.TopLeft);
            hungerAnimationProgress = 1;
        }
        if (mealTimeAgo < RELAX_TIME_MAX / 3)
            readyToEgg = Mathf.Max(readyToEgg + Time.deltaTime, 1);
        if (mealTimeAgo > 0)
        {
            mealTimeAgo += Time.deltaTime;
            if (mealTimeAgo > RELAX_TIME_MAX)
            {
                mealTimeAgo = 0;
                Debug.Log("Oh no! The dodo could not take it anymore!");
                Animate("DisgraceAnimation", AnimationPosition.TopRight);
            }
        }
    }

    public void PushInDirection(Vector2 pushVector, float pushForce) {
        rb.AddForce(pushVector.normalized * pushForce);
        stateMachine.SetTrigger("stun");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Conveyer")){
            stateMachine.SetBool("Conveyed",true);
           onConveyer ++;
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
            onConveyer--;
            
        }
        if(onConveyer == 0){
            stateMachine.SetBool("Conveyed",false);
        }
    }

    public enum AnimationPosition { TopRight, TopLeft }
    public void Animate(string animationName, AnimationPosition positionName)
    {
        Vector3 position = (positionName == AnimationPosition.TopRight) ? new Vector3(0.4f, 0.6f, 0) : new Vector3(-0.4f, 0.6f, 0);
        GameObject animation = Instantiate(Resources.Load<GameObject>(animationName));
        if (animation == null) {
            Debug.LogError("Could not find " + animationName + " prefab in Resources folder!");
        } else {
            animation.transform.parent = transform;
            animation.transform.position = transform.position + position;
        }
    }
}
