using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody CharRigidBody;
    [SerializeField]
    float speed; 
    [SerializeField]
    float JumpPower;
    Animator CharAni;
    private readonly float m_interpolation = 10;
    //readonly는 읽기 전용을 나타내는 변수이다.
    bool isJumping=false;
    private Vector3 m_currentDirection = Vector3.zero; //현재 방향을 나타낼때 쓰는 변수. 
    private float currentV = 0; //현재 vertical을 나타냄
    private float currentH = 0; //현재 horizontal을 나타냄
    // Start is called before the first frame update
    void Start()
    {
        CharRigidBody=GetComponent<Rigidbody>(); //현재 오브젝트에 붙여있는 rigidbody 옵션 가져오기 
        CharAni=GetComponent<Animator>();
    }

    private void OnCollisionStay(Collision other) {
        CharAni.SetBool("Grounded",true); //애니메이션 mode : 착지
        isJumping=false;
    }
    // Update is called once per frame
    void Update()
    {
      MoveAndRot(); //프레임별 검사를 통해 이동을 감지 
      Jump(); //프레임별 검사를 통해 점프를 감지
    }

    //캐릭터 이동을 위한 함수
    private void MoveAndRot(){
        //GetAxisRaw함수는 정수값을 반환, GetAxis함수는 실수값을 반환.
        float inputX=Input.GetAxis("Horizontal"); 
        float inputZ=Input.GetAxis("Vertical"); // 키보드에 입력된 값 위,아래에 대한 값을 입력받음 -1~1 사이
        //앞으로 가고 뒤로 가고를 구현하기 위해 일단, z만 구현 x값은 좌우로 옆으로 이동이 가능해짐.

        Transform camera = Camera.main.transform; //현재 카메라의 위치를 가져온다.

        currentV = Mathf.Lerp(currentV, inputZ, Time.deltaTime * m_interpolation);
        //보간은 두 점을 연결하는 방법을 의미. 곱하여 자연스러운 연결을 위함(?) 
        currentH = Mathf.Lerp(currentH, inputX, Time.deltaTime * m_interpolation);

        Vector3 moveVec = camera.forward * currentV + camera.right * currentH;
        //vertical은 앞 뒤를 의미 right 좌우를 의미하기 때문에 horizontal값을 곱해서 moveVec를 구성.
        float directionLength = moveVec.magnitude;
        //tail - head의 거리를 구하여 특정 object의 속력을 구할 때 활용한다.
        moveVec.y=0;
        //y의 값은 점프를 한것이기 때문에 이동시 건드리면 안된다.
        moveVec = moveVec.normalized * directionLength;
        transform.position+=moveVec*speed*Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(m_currentDirection);
        
        if (moveVec != Vector3.zero){
            m_currentDirection = Vector3.Slerp(m_currentDirection, moveVec, Time.deltaTime * m_interpolation);
            //Vector3.Slerp 란?
            //구면보간법을 사용함 : 두 지점 사이의 위치를 파악한다는 것은 같지만, 곡선으로 파악하기 때문에 선형보다 어려움.
            //구면 보간법을 이용해 회전이 가능해진다. 
            transform.position += m_currentDirection * speed * Time.deltaTime;
            CharAni.SetFloat("MoveSpeed",moveVec.magnitude);
        }

    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space)&&!isJumping){
            CharAni.SetBool("Grounded",false); //Jumping 중 (애니메이션)
            //물리 이용
            CharRigidBody.AddForce(Vector3.up*JumpPower,ForceMode.Impulse);
            //Jump횟수 제한할 예정
            isJumping=true; //이렇게 구현했더니 2번 점프가 되는 매직. 
        }
    }
    /*개발 1. Move 정리 */
    /*유투브 : https://www.youtube.com/watch?v=WkMM7Uu2AoA을 보면서 Move 로직 구현
    transform.position을 조작할 경우 단점: 충돌검사가 잘 안이루어질 수 있음 그렇기 때문에 충돌을 'Continue'로 변경
    3D에서 간단하게 Z축은 이동 X축은 방향 Y축은 점프 를 의미한다. inputX(방향) inputZ(이동)을 이용해 새로운 Vector을 만들고,
    normalized를 한다. Why? 피타고라스 정리에 따르면 1:1:루트2(가로지르는 방향)을 의미한다. 루트2일때 속도가 달라지는 것을 방지
    하기 위해서 사용한다. 시간별 * 스피드 * 이동,방향을 곱해서 더해주면 캐릭터가 이동하는 값이 나온다.
    
    Animation의 경우 MoveSpeed를 이용한다면 쉬운 이동이 가능하다.*/

    /*개발 2. Jump 정리*/
    /*C#에서 AddForce란? 힘을 줘서 Player을 움직이는 코드로, 특히 ForceMode.Impulse는 뒤에서 누가 밀듯이
    속도를 붙여주는 방식으로, 정지상태에서 이동하려고 할 때 적합한 힘이다.
    위로 JumpPower만큼 곱해진 값을 이용해 미는 힘을 발생시키고 그럼으로써 속도를 변화시킨다.*/

    /*개발 3. 회전 정리*/
    /**/
}
