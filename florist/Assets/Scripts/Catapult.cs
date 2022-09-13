using UnityEngine;
using DG.Tweening;

public class Catapult : MonoBehaviour
{
    [SerializeField] string Tag;
    [SerializeField] PoolInfo projectile;
    [SerializeField] PoolInfo blueProjectile;
    [SerializeField] PoolInfo redProjectile;
    [SerializeField] PoolInfo yellowProjectile;
    [SerializeField] Vector2 RandomAngularSpeedRange;// => GameManager.Ins.gameSettingsSC.randomAngularSpeedRange;
    [SerializeField] float forwardDistance = 10f;
    [SerializeField] float jumpPower = 50f;
    [SerializeField] float jumpDuration = 1f;
    [SerializeField] Transform targetLocation;
    Rigidbody tempRb;
    GameObject throwThis;
    Vector3 tempRandomVector3;
    VariableContainer Variables { get => VariableManager.ins.GetVariableList(Tag); }
    
    public float ForwardDistance { get => forwardDistance; }//Variables.GetFloat("CatapultForwardDistance"); }
    public float JumpPower { get => jumpPower; }//Variables.GetFloat("JumpPower"); }
    public float JumpDuration { get => jumpDuration; }//Variables.GetFloat("JumpDuration"); }

    public void Throw(GameObject go)
    {
        if (go != null)
        {
            throwThis = PoolManager.fetch(GetPoolName(go));
            
            SetProperties(throwThis);
        }
    }

    public void Throw()
    {
        if (projectile != null)
        {
            throwThis = PoolManager.fetch(projectile.PoolName);

            SetProperties(throwThis);
        }
    }

    string tempPoolName;
    FlowerTypeSC tempFlowerTypeSC;
    private string GetPoolName(GameObject flowerGo)
    {
        tempFlowerTypeSC = flowerGo.GetComponent<FlowerType>().Type;

        switch (tempFlowerTypeSC.Color)
        {
            case FlowerColor.Blue:
                tempPoolName = blueProjectile.PoolName;
                break;
            case FlowerColor.Red:
                tempPoolName = redProjectile.PoolName;
                break;
            case FlowerColor.Yellow:
                tempPoolName = yellowProjectile.PoolName;
                break;
            default:
                tempPoolName = "";
                break;
        }

        return tempPoolName;
    }

    private void SetProperties(GameObject go)
    {
        go.transform.position = transform.position;

        tempRb = go.GetComponent<Rigidbody>();
        tempRb.angularVelocity = RandomVector3();

        go.SetActive(true);

        /*Vector3 vec = Random.insideUnitCircle;
        vec.z = -Mathf.Abs(vec.z);

        tempRb.AddForce(vec * 50 +
           transform.root.up  * 20, ForceMode.Impulse);*/

        if (targetLocation == null)
        {
            tempVec3 = RandomLocation(go);
            go.transform.DOJump(tempVec3, JumpPower, 1, JumpDuration)
                .OnComplete(() => SetCollectableAvailable(go));
        }
        else
            go.transform.DOJump(targetLocation.position, JumpPower, 1, JumpDuration)
                .OnComplete(() => SetCollectableAvailable(go));

        
        //go.transform.DOJump(transform.position + (Vector3.forward * forwardDistance), jumpPower, 1, jumpDuration);
    }
     Vector3 tempVec3;
    private static Vector3 RandomLocation(GameObject go)
    {
        return new Vector3(-4f + Random.Range(0f, 8f), go.transform.position.y,
                                go.transform.position.z + 13f + Random.Range(0f, 3f));
    }

    private void SetCollectableAvailable(GameObject go)
    {
        go.GetComponent<CollectableFlower>().IsAvailable = true;
    }

    private Vector3 RandomVector3()
    {
        tempRandomVector3.x = RandomFloat();
        tempRandomVector3.y = RandomFloat();
        tempRandomVector3.z = RandomFloat();

        return tempRandomVector3; 
    }

    private float RandomFloat()
    {
        return Random.Range(RandomAngularSpeedRange.x, RandomAngularSpeedRange.y);
    }
}
