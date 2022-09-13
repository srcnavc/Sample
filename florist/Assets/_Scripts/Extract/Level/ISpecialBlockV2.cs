using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialBlockV2<T> 
{// I dunno whatIm doin
    void getSpecialParameters(ref List<specialParameterList<T>> specials);
     void setSpecialParameters(specialParameterList<T> specials);
  
}
[System.Serializable]
public class specialParameterList<T>{

    public string parameterOwnerID;
    public List<T> parameters;

}
