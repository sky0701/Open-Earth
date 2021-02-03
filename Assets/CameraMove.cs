using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    Vector2 fingerposition;
    bool TouchScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    {
    }

    
}
