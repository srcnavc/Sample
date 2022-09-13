using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PatrollingAI))]
public class PatrollMove : MonoBehaviour
{
    public PatrollingAI AIBase;
    public float LastStationedTime;
    bool lookingAround;
    public PatrolState state
    {
        get {
            return AIBase.state;
        }

    }
    public PatrolPoint path
    {
        get {
            return AIBase.getPatrollingPoint();
        }
    }
    public NavMeshAgent agent
    {
        get {
            return AIBase.getNavMeshAgent();
        }
    }


   
    public void setup()
    {
        LastStationedTime = Time.time;
        AIBase = GetComponent<PatrollingAI>();
        agent.ResetPath();
        PatrolToNext();
        transform.position = AIBase.currentStation.point;
        
    
    }


    private void LateUpdate()
    {
        if (AIBase.isPlaying())
        {
            switch (state)
            {
                case PatrolState.Patroling:
                    if (AIBase.currentStation == null)
                        AIBase.Patroll();

                    checkPosition(AIBase.currentStation.point);
                    break;
                case PatrolState.Stationed:
                    checkStation();
                    break;
                case PatrolState.Alarmed:
                    checkPosition(AIBase.LastInterestPoint);
                    break;
            }
        }
       
        
        
    }

    public void checkPosition(Vector3 position)
    {
        if (agent.destination != position)
            agent.destination = position;

        if (agent.remainingDistance < 0.2f )
        {
            if (AIBase.state == PatrolState.Alarmed)
                AIBase.AlarmEnd();
            agent.isStopped = true;
            AIBase.DockToStation();
            LastStationedTime = Time.time;
            
        }
        else
            agent.isStopped = false;

        

    }
    public void PatrolToNext()
    {
       // Debug.Log("patrollNext");
        if (AIBase.currentStation != null)
            path.releaseOccupied(AIBase.currentStation);
    
        AIBase.currentStation = path.getNextStation();
        AIBase.Patroll();
        agent.destination = AIBase.currentStation.point;
        agent.isStopped = false;
    }

    public void checkStation()
    {
        float waittime = 5; //TODO Fix This Patch take it from Settings 
        if (AIBase.currentStation != null )//This is null when enemy ends following
        {
            waittime = AIBase.currentStation.waitTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, AIBase.currentStation.WaitRotateAngle, 0), .05f);
        }
       

        if (LastStationedTime + waittime < Time.time)
        {
            PatrolToNext();
            return;
        }
     
    }
}
