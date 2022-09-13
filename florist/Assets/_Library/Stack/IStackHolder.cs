using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackHolder 
{
    void AddToStack(IStackable stackable);
    IStackable fetch();
    int getCount();
    GameObject getGameObject();

  //  void updateVaribles();

}
