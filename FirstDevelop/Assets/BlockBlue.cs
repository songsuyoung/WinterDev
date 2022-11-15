using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBlue : MonoBehaviour
{

    float rotateSpeed=60f;
    bool isRotate=false;

    private void OnMouseDown() {
        if(isRotate){
            return;
        }

        isRotate=true;
        StartCoroutine("RotateDestroy");
    }

    IEnumerator RotateDestroy(){
        float angle=0;

        while(angle<90){
            angle+=rotateSpeed*Time.deltaTime; 
            this.gameObject.transform.Rotate(0,rotateSpeed*Time.deltaTime,0);
            yield return null;
        }
        Destroy(this.gameObject);
    } 
}

