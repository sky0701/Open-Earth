using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour { 

    [SerializeField] GameObject cameraHolder = null;
    [SerializeField] float mouseSensitivity = 1f, sprintSpeed = 1f, walkSpeed = 1f, smoothTime = 1f;

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    public PhotonView PV;
    public Animator AN;
    public Rigidbody RB;

    RectTransform joystickholder;
    RectTransform joystick;

    float joystickradius;
    Vector2 fingerposition;
    bool TouchScreen;
    Vector2 touchvector;


    void Awake()
    {

    }

    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(RB); //이거 해주고 fixed Update에다가 추가해줘야 됨 모르겠어.. 두개의 물리엔진인가봄

        }

    }

    void Update()
    {
        if (PV.IsMine)
        {
            Look();
            Move();
        }
        Debug.Log("플레이어 위치" + transform.position);

    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity); //y축 회전
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;//다시다시다시다시
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);//최대 최소 설정

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;


    }

    void Move()
    {

        Vector3 moveDir = GameObject.Find("Controller").GetComponent<Joystick>().Dir;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        RB.transform.eulerAngles= GameObject.Find("Controller").GetComponent<Joystick>().char_rotate;
    }
 

    public void SetGroundedState(bool _grounded)
    {
        Debug.Log("바닥상태 변경!");
        grounded = _grounded;
        Debug.Log(grounded);
    }

    void FixedUpdate() //찾아보자
    {
        if (!PV.IsMine)
        {
            return;
        }
        RB.MovePosition(RB.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

}
