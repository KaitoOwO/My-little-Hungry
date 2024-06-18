using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CustomCursor customCursor;

    private void Start()
    {
        customCursor = FindObjectOfType<CustomCursor>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (customCursor != null)
        {
            customCursor.SetHoverState(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (customCursor != null)
        {
            customCursor.SetHoverState(false);
        }
    }
}