using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : SingletonBehavior<CameraMovement>
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    
    protected override void OnAwake(){}

    void Update(){
        transform.position = target.position + offset;
    }
}
