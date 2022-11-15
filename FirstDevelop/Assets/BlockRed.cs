using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRed : MonoBehaviour
{
    //상속받지 않아도 OnMouseDown을 이용하면 삭제가 된다. 
    //처음 알았던 사실.
    private void OnMouseDown() {
        Destroy(this.gameObject);    
    }
}
