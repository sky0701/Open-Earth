using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    RectTransform joystickholder;
    RectTransform joystick;

    float joystickradius;
    Vector2 fingerposition;
    bool TouchScreen;
    Vector2 touchvector;

    public Vector3 Dir;
    public Vector3 char_rotate;

    // Start is called before the first frame update
    void Start()
    {//일단 참조 
        joystickholder = transform.Find("JoystickHolder").GetComponent<RectTransform>();
        joystick = transform.Find("JoystickHolder/Joystick").GetComponent<RectTransform>();

        joystickradius = joystickholder.rect.width * 0.5f;
        //배경의 반지름

    }

    // Update is called once per frame
    void Update()
    {
        MoveDirection();
    }
    void MoveDirection()
    {
        if (TouchScreen)
        {

            touchvector = new Vector2(fingerposition.x - joystickholder.position.x, fingerposition.y - joystickholder.position.y);
            touchvector = Vector2.ClampMagnitude(touchvector, joystickradius);
            joystick.anchoredPosition = touchvector;
            
            Dir = new Vector3(touchvector.normalized.x, 0f, touchvector.normalized.y);

        }
        char_rotate = new Vector3(0f, Mathf.Atan2(touchvector.normalized.x, touchvector.normalized.y) * Mathf.Rad2Deg, 0f);
        //확인
    }



    public void OnDrag(PointerEventData eventData)
    {
        fingerposition = eventData.position;
        TouchScreen = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        fingerposition = eventData.position;
        TouchScreen = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {//손떼면 원래위치
        joystick.transform.position = new Vector2(85,85);
        TouchScreen = false;
        Dir = Vector3.zero;
    }
}
