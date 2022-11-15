using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject Target;
    Vector3 TargetPos;

    float offsetX=0.0f;
    float offsetY=2.0f;
    float offsetZ=-2.0f;

    float CameraSpeed=5.0f;

    float angleX = 20.0f;
    float angleY = 0.0f;
    float angleZ = 0.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        TargetPos = new Vector3(
            Target.transform.position.x+offsetX,
            Target.transform.position.y+offsetY,
            Target.transform.position.z+offsetZ
        );

        //카메라의 움직임을 부드럽게 만들기 위해서 Lerp함수를 이용한다.
        transform.position = Vector3.Lerp(transform.position,TargetPos,Time.deltaTime*CameraSpeed);
        transform.rotation = Quaternion.Euler(angleX, angleY, angleZ);
    }
}
