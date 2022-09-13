using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class NavMeshPersonMovement : MonoBehaviour
{
    [SerializeField] string Tag;
    [SerializeField] private float speed;
    [SerializeField] PlayerController pController;
    NavMeshAgent agent;
    Vector3 directionVector;
    IinputBridge input;
    VariableContainer Variables { get => VariableManager.ins.GetVariableList(Tag); }
    public float Speed { get => speed; } //Variables.GetFloat("IdleSpeed"); }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        input = FindObjectsOfType<MonoBehaviour>().OfType<IinputBridge>().First();
    }

    private void Update()
    {
        if (input.moveInput.magnitude == 0)
        {
            pController.State = PlayerState.Idle;
            return;
        }

        pController.State = PlayerState.Run;

        directionVector.x = input.moveInput.x;
        directionVector.z = input.moveInput.y;
        directionVector.Normalize();
        agent.Move(Speed * Time.deltaTime * directionVector);

        // Rotation --------------------------------------------------------------------------->
        transform.rotation = Quaternion.LookRotation(directionVector);// Quaternion.Lerp(transform.rotation, Quaternion.Euler(directionVector),rotationSpeed);
        // Rotation --------------------------------------------------------------------------->
    }




}
