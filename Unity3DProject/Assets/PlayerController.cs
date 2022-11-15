using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody CharRigidBody;
    [SerializeField]
    float speed=20f;
    Animator CharAni;

    // Start is called before the first frame update
    void Start()
    {
        CharRigidBody=GetComponent<Rigidbody>(); //현재 오브젝트에 붙여있는 rigidbody 옵션 가져오기 
        CharAni=GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) {
        CharAni.SetBool("Grounded",true);
    }
    // Update is called once per frame
    void Update()
    {
      Move(); //프레임별 검사를 통해 이동을 감지 
    }

    //캐릭터 이동을 위한 함수
    private void Move(){
        //GetAxisRaw함수는 정수값을 반환, GetAxis함수는 실수값을 반환.
        float inputX=Input.GetAxis("Horizontal"); 
        float inputZ=Input.GetAxis("Vertical"); // 키보드에 입력된 값 위,아래에 대한 값을 입력받음 -1~1 사이
        //앞으로 가고 뒤로 가고를 구현하기 위해 일단, z만 구현 x값은 좌우로 옆으로 이동이 가능해짐.

        Vector3 moveVec = new Vector3(inputX,0,inputZ).normalized; 
        transform.position+=moveVec*speed*Time.deltaTime;

        CharAni.SetFloat("MoveSpeed",moveVec.z);
    }
    /*첫 3D 개발 1. Move 정리 */
    /*유투브 : https://www.youtube.com/watch?v=WkMM7Uu2AoA을 보면서 Move 로직 구현
    transform.position을 조작할 경우 단점: 충돌검사가 잘 안이루어질 수 있음 그렇기 때문에 충돌을 'Continue'로 변경
    3D에서 간단하게 Z축은 이동 X축은 방향 Y축은 점프 를 의미한다. inputX(방향) inputZ(이동)을 이용해 새로운 Vector을 만들고,
    normalized를 한다. Why? 피타고라스 정리에 따르면 1:1:루트2(가로지르는 방향)을 의미한다. 루트2일때 속도가 달라지는 것을 방지
    하기 위해서 사용한다. 시간별 * 스피드 * 이동,방향을 곱해서 더해주면 캐릭터가 이동하는 값이 나온다.
    
    Animation의 경우 MoveSpeed를 이용한다면 쉬운 이동이 가능하다.*/
}
