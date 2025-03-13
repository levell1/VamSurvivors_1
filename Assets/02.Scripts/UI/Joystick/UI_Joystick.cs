
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    [SerializeField]
    Image _bg;
    [SerializeField]
    Image _handler;

    float _joystickRadius;
    Vector2 _touchPosition;
    Vector2 _moveDir;


    void Start()
    {
        _joystickRadius = _bg.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _touchPosition = eventData.position;
        _bg.transform.position = eventData.position;
        _handler.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _handler.transform.position = _touchPosition;
        _moveDir = Vector2.zero;

        // Temp2 Joystick-Player (Manager(Static))
        Managers.Game.MoveDir = _moveDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchDir = (eventData.position - _touchPosition);
        float moveDist = Mathf.Min( touchDir.magnitude, _joystickRadius);
        _moveDir = touchDir.normalized; //πÊ«‚
        Vector2 newPosition = _touchPosition + _moveDir * moveDist;
        _handler.transform.position = newPosition;

        // Temp2 Joystick-Player (Manager(Static))
        Managers.Game.MoveDir = _moveDir;
    }

}
