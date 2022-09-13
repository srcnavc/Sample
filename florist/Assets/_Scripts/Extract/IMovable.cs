
using UnityEngine;
public interface IMovable 
{
    Vector3 speed { get; }
    bool isStopped { get; }

    void move();


}
