using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurableJointSetup : MonoBehaviour
{
    public Transform CopyCatRoot;

    
    public Transform OriginalRoot;

    public float weight;

    public void AssignJoints(Transform copycat, Transform OriginalRoot)
    {

        Transform originalTransformMatch = OriginalRoot.parent.gameObject.FindInChildren(copycat.name).transform;

        if (originalTransformMatch != null)
        {
            Rigidbody CCRB = copycat.gameObject.AddComponent<Rigidbody>();
            ConfigurableJoint CCCJ = copycat.gameObject.AddComponent<ConfigurableJoint>();
           

            //First Connection made to originalModel
            if (copycat == CopyCatRoot)
            {
                CCCJ.connectedBody = originalTransformMatch.GetComponent<Rigidbody>();
                setConfigurableModelPositionLocked(ref CCCJ);
                setConfigurableModelRotationLocked(ref CCCJ);
            }
            else
            {

                LimbCopy CCLC = copycat.gameObject.AddComponent<LimbCopy>();
                CCCJ.connectedBody = copycat.parent.GetComponent<Rigidbody>();
                setConfigurableModelPositionLocked(ref CCCJ);
                setConfigurableModelRotationDrive(ref CCCJ, weight);
                CCLC.toCopy = originalTransformMatch;
                CCLC.LOCKED = true;
            }



            for (int i = 0; i < copycat.childCount; i++)
            {
                AssignJoints(copycat.GetChild(i), OriginalRoot);
            }
                


        }
        else
            Debug.Log("Could not find '" + copycat.name +  "' in Original");

    }


    public void setConfigurableModelPositionLocked(ref ConfigurableJoint CJ)
    {
        CJ.xMotion = ConfigurableJointMotion.Locked;
        CJ.yMotion = ConfigurableJointMotion.Locked;
        CJ.zMotion = ConfigurableJointMotion.Locked;
    }


    public void setConfigurableModelRotationLocked(ref ConfigurableJoint CJ)
    {
        CJ.angularXMotion = ConfigurableJointMotion.Locked;
        CJ.angularYMotion = ConfigurableJointMotion.Locked;
        CJ.angularZMotion = ConfigurableJointMotion.Locked;
    }

    public void setConfigurableModelRotationDrive(ref ConfigurableJoint CJ , float value)
    {
        JointDrive JD = new JointDrive();
        JD.positionSpring = value;
        JD.maximumForce = CJ.angularXDrive.maximumForce;
        CJ.angularXDrive = JD;
        CJ.angularYZDrive = JD;
    }



}
