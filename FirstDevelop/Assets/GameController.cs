using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image texClear;
    public Image texFailed;
    //GUITexture -> Image
    //GUITexture은 구식이라, 제거되었고, 그대신에 UI.Image를 사용함.

    void Start()
    {
        texClear.enabled=false;
        texFailed.enabled=false;
    }

    void StageClear(){
        texClear.enabled=true;
    }

    void StageFailed(){
        texFailed.enabled=true;
    }

}
