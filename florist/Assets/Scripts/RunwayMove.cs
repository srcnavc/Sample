using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class RunwayMove : MonoBehaviour
{
    [SerializeField] string Tag;
    [SerializeField] float speed;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float slowDownSpeed;
    [SerializeField] float slowDownDuration;
    [SerializeField] float goBackSpeed;
    [SerializeField] float goBackDuration;
    [SerializeField] PlayerController pController;
    bool goBack = false;
    NavMeshAgent agent;
    Vector3 directionVector;
    IinputBridge input;
    float goBackTimer;
    bool slowDown = false;
    float slowDownTimer;
    
    VariableContainer Variables { get => VariableManager.ins.GetVariableList(Tag); }
    
    public float Speed { get => Variables.GetFloat("RunnerForwardSpeed"); }
    public float HorizontalSpeed { get => Variables.GetFloat("RunnerHorizontalSpeed"); }
    public float GoBackSpeed { get => Variables.GetFloat("RunnerGoBackSpeed"); }
    public float GoBackDuration { get => Variables.GetFloat("RunnerGoBackDuration"); }
    public float SlowDownSpeed { get => Variables.GetFloat("RunnerSlowDownSpeed"); }
    public float SlowDownDuration { get => Variables.GetFloat("RunnerSlowDownDuration"); }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barricade"))
        {
            GoBack();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Barricade"))
        {
            GoBack();
        }
    }
    private void Start()
    {
        input = FindObjectsOfType<MonoBehaviour>().OfType<IinputBridge>().First();
        LogCutter.OnLogCutting += SlowDown;
    }

    private void Update()
    {
        if (goBack && goBackTimer >= Time.time)
            Move(-Vector3.forward, GoBackSpeed, Vector3.forward);
        else if(slowDown && slowDownTimer >= Time.time)
            Move(Vector3.forward, SlowDownSpeed, Vector3.forward);
        else
            Move(Vector3.forward, Speed, Vector3.forward);
    }

    private void Move(Vector3 direction, float _speed, Vector3 lookDirection)
    {
        pController.State = PlayerState.Run;

        directionVector = direction;
        directionVector.Normalize();

        directionVector.z *= _speed;
        directionVector.x = input.moveInput.x * HorizontalSpeed;
        agent.Move(Time.deltaTime * directionVector);

        // Rotation --------------------------------------------------------------------------->
        transform.rotation = Quaternion.LookRotation(lookDirection);// Quaternion.Lerp(transform.rotation, Quaternion.Euler(directionVector),rotationSpeed);
        // Rotation --------------------------------------------------------------------------->

    }
    public void GoBack()
    {
        goBackTimer = Time.time + GoBackDuration;
        goBack = true;
    }

    public void SlowDown()
    {
        slowDownTimer = Time.time + SlowDownDuration;
        slowDown = true;
    }

}
