using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGreen : MonoBehaviour
{
    
    [SerializeField]
    GameObject blokenBlocksPrefab;

    float hardness=5f;

    private void OnCollisionEnter(Collision other) {
        //상대방의 속도가 경도를 초과하는지 체크
        Debug.Log("디버그중, 부딪힌 상대의 속도 "+other.relativeVelocity.magnitude);
        if(other.relativeVelocity.magnitude>hardness)
        {
            Instantiate(blokenBlocksPrefab,transform.position,blokenBlocksPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}
