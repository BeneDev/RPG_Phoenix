using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    bool controllerMode = false;
    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster raycaster;
    Vector3 currentDestination, clickPoint;
    private float stopRadius;
    private Camera cam;
    State actingState;
    private GameObject enemyToHit;
    [SerializeField] float damageOutput = 10;
    [SerializeField] GameObject frontCheck;
    [SerializeField] float cooldownMelee = 1f;
    private AICharacterControl aiController;
    GameObject walkTarget = null;
    private float cooldown;
    public enum State
    {
        waiting,
        attacking,
        cooldown
    }
    private State oldState;
    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;

    void Start()
    {
        raycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        cam = Camera.main;
        actingState = State.waiting;
        aiController = GetComponent<AICharacterControl>();
        raycaster.notifyMouseClickObservers += OnClickEnter;
        walkTarget = new GameObject("WalkTarget");
    }
    
    void OnClickEnter(RaycastHit target, int newLayer)
    {
        switch(newLayer)
        {
            default:
                Debug.Log("Don't know where to go");
                return;

            case walkableLayerNumber:
                walkTarget.transform.position = target.point;
                aiController.target = walkTarget.transform;
                break;

            case enemyLayerNumber:
                GameObject enemy = target.collider.gameObject;
                aiController.target = enemy.transform;
                break;
        }
    }

    void HandleCooldown()
    {
        cooldown = cooldownMelee;
        StartCoroutine(CountDownCooldown(cooldown));
        actingState = oldState;
    }

    IEnumerator CountDownCooldown(float pCooldown)
    {
        print(Time.time);
        yield return new WaitForSeconds(pCooldown);
        print(Time.time);
    }

    private void AttackEnemy()
    {
        thirdPersonCharacter.Move(Vector3.zero);
        enemyToHit.GetComponent<Enemy>().TakeDamage(damageOutput);
        oldState = actingState;
        actingState = State.cooldown;
    }

    void ProcessControllerInput()
    {
        // clear click target
        currentDestination = transform.position;
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // calculate camera relative direction to move
        Vector3 camForward = Vector3.Scale(cam.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
        Vector3 moveValue = v * camForward + h * cam.transform.right;
        thirdPersonCharacter.Move(moveValue);
    }

}

