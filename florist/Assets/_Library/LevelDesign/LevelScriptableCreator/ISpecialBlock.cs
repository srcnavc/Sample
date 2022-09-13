using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialBlock 
{
     void  getSpecialParameters(ref List<specialData> specials);
     void  setSpecialParameters(List<specialData> specials);

}
