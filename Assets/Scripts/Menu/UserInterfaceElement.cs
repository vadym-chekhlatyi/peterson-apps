using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInterfaceElement : MonoBehaviour,IPointerClickHandler
{
    [Space]
    [SerializeField]
    UnityEvent mainEvent;
    public UnityEvent MainEvent
    {
        get
        {
            return mainEvent;
        }
        set
        {
            mainEvent = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MainEvent.Invoke();
    }
}
