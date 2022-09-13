using FlatKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    [SerializeField] InfiniteRoad infiniteRoad;
    [SerializeField] FogImageEffect fogImageEffect;
    [SerializeField] GameObject player;
    [SerializeField] float idleGroundOffset;
    [SerializeField] float runnerOffset;
    [SerializeField] float k;
    [SerializeField] float speed = 1f;
    MapComponents mapComponents => InfiniteRoad.ins.ActiveZone;
    bool IsReady => infiniteRoad != null && fogImageEffect != null && player != null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    float dis;
    float dis2;
    Vector3 tempVec3;
    float myFar;
    bool b;
    // Update is called once per frame
    void LateUpdate()
    {
        if (IsReady)
        {
            tempVec3 = player.transform.position;
            tempVec3.z = mapComponents.transform.position.z;
                tempVec3.z += mapComponents.Renderer.bounds.extents.z;
            
            dis = Vector3.Distance(player.transform.position, tempVec3);
            
            

            if (mapComponents.transform.CompareTag("IdleGround"))
                k = idleGroundOffset;
            else if (mapComponents.transform.CompareTag("Runner"))
                k = runnerOffset;

            

            if (Mathf.Abs(dis - dis2) > 5f || b)
            {
                myFar = Mathf.Lerp(fogImageEffect.far - k, dis, speed * Time.deltaTime) + k;
                b = true;

                if (Mathf.Abs(fogImageEffect.far - myFar) <= 0.02f)
                    b = false;
            }
            else
                myFar = k + dis;

            Debug.Log("myFar : " + myFar + " mathf : " + Mathf.Abs(dis - dis2) + " bool : " + b);

            fogImageEffect.far = myFar;

            dis2 = dis;
        }
    }
}
