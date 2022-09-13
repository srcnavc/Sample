using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineConnector : MonoBehaviour,ISpecialBlock
{

    static string MoveOffsetKey = "SplineConnectorData_MoveOffset";
    static string StartPercentageKey = "SplineConnectorData_percent";
    [SerializeField] string SplineComputerTag;
    [SerializeField] string SpecialBlockIdentifier;
    [SerializeField] float startPercentage;
    [SerializeField] Vector2 MoveOffset;
    SplineFollower follower;
    SplinePositioner positioner;
    ObjectController objectController;
    [SerializeField] bool autoConnect;

    public void getSpecialParameters(ref List<specialData> specials)
    {
        follower = GetComponent<SplineFollower>();
        positioner = GetComponent<SplinePositioner>();
        objectController = GetComponent<ObjectController>();
        if (follower != null)
        {
            MoveOffset = follower.motion.offset;
            startPercentage = (float)follower.startPosition;

        }
        else if (positioner != null)
        {

            MoveOffset = positioner.motion.offset;
            startPercentage = (float)positioner.result.percent;
            // Debug.Log(name + " - PositionerPercentage: " + startPercentage);
        }
        else if (objectController != null)
        { 
        //nose
        }
        specials.Add(new specialData(SpecialBlockIdentifier + MoveOffsetKey, MoveOffset.x + "/" + MoveOffset.y));
        specials.Add(new specialData(SpecialBlockIdentifier + StartPercentageKey, startPercentage.ToString()));
    }

    public void setSpecialParameters(List<specialData> specials)
    {
        follower = GetComponent<SplineFollower>();
        positioner = GetComponent<SplinePositioner>();
        objectController = GetComponent<ObjectController>();

        for (int i = 0; i < specials.Count; i++)
        {
            if (specials[i].SpecialKey.Equals(SpecialBlockIdentifier + MoveOffsetKey))
            {
#if PLATFORM_IOS
            
                MoveOffset.x = (float)double.Parse(specials[i].Data.Split('/')[0].Replace(',','.'),System.Globalization.CultureInfo.InvariantCulture);
                MoveOffset.y = (float)double.Parse(specials[i].Data.Split('/')[1].Replace(',', '.'),System.Globalization.CultureInfo.InvariantCulture);
#else
                MoveOffset.x = (float)double.Parse(specials[i].Data.Split('/')[0].Replace('.', ','));
                MoveOffset.y = (float)double.Parse(specials[i].Data.Split('/')[1].Replace('.', ','));
#endif



            }
            else if (specials[i].SpecialKey.Equals(SpecialBlockIdentifier + StartPercentageKey))
            {

#if PLATFORM_IOS
                startPercentage = (float) double.Parse(specials[i].Data.Replace(',', '.'),System.Globalization.CultureInfo.InvariantCulture);
#else

                startPercentage = (float) double.Parse(specials[i].Data.Replace('.', ','));
#endif
            }

        }
        Connect();
    }

    protected void Connect()
    {
        follower = GetComponent<SplineFollower>();
        positioner = GetComponent<SplinePositioner>();

        SplineComputer spline = GameObject.FindGameObjectWithTag(SplineComputerTag).GetComponent<SplineComputer>();

        if (follower != null)
        {
            follower.startPosition = startPercentage;
            follower.spline = spline;
            follower.motion.offset = MoveOffset;
        }
        if (positioner != null)
        {
            positioner.position = startPercentage;
            positioner.spline = spline;
            positioner.motion.offset = MoveOffset;
        }
        else if (objectController != null)
        {
            objectController.spline = spline;
         objectController.spawnCount = objectController.spawnCount + 1;
          objectController.spawnCount = objectController.spawnCount - 1;
        }

    }
    private void Start()
    {
        if (autoConnect)
        {
            Connect();
        }
    }
    // Update is called once per frame

}
