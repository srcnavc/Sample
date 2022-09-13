using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssigner : MonoBehaviour
{
    [SerializeField]IObjectFollower follower;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        if (follower == null)
            follower = GetComponent<IObjectFollower>();

        follower.setFollowObject(Player.getCurrentPlayer().gameObject);
    }

 
}
