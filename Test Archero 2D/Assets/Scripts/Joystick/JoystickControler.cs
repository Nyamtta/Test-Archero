using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickControler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image JoystickImag = default;
    [SerializeField] private Image stickImag = default;

    private Vector2 InputVector;


    public void OnPointerDown(PointerEventData eventData) {

        OnDrag(eventData);
    }
 

    public void OnPointerUp(PointerEventData eventData) {

        InputVector = Vector2.zero;
        stickImag.rectTransform.anchoredPosition = Vector2.zero;

    }
    public void OnDrag(PointerEventData eventData) {
       
        Vector2 localPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickImag.rectTransform, eventData.position,
            eventData.pressEventCamera, out localPosition)) {

            localPosition.x = (localPosition.x / JoystickImag.rectTransform.sizeDelta.x);
            localPosition.y = (localPosition.y / JoystickImag.rectTransform.sizeDelta.y);

            InputVector = new Vector2(localPosition.x * 2 - 1, localPosition.y * 2 - 1);

            InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;

            stickImag.rectTransform.anchoredPosition = new Vector2(
                InputVector.x * (JoystickImag.rectTransform.sizeDelta.x / 2), 
                InputVector.y * (JoystickImag.rectTransform.sizeDelta.y / 2));


        }
    }

    public float Horizontal() {

        return InputVector.x;
    }


    public float Vertical() {

        return InputVector.y;
    }
}
