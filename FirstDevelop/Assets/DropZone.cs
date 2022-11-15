using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{

    GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController=GameObject.Find("GameController");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name=="BlockGreen"){
            gameController.SendMessage("StageFailed");
        }

        Destroy(gameObject);
    }
}
