using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject Target;
    Vector3 TargetPos;

    float offsetX=0.0f;
    float offsetY=3.0f;
    float offsetZ=-3.0f;



    // Update is called once per frame
    void FixedUpdate()
    {
        
        TargetPos = new Vector3(
            Target.transform.position.x+offsetX,
            Target.transform.position.y+offsetY,
            Target.transform.position.z+offsetZ
        );

        transform.position = TargetPos;
    }
}
