using UnityEngine;
using UnityEngine.EventSystems;

public class PressableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator animRef;

    private void Start()    {
        animRef = GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        if (animRef != null)
        {
            animRef.enabled = false;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        transform.localScale = Vector3.one;

        if (animRef != null)
        {
            animRef.enabled = true;
        }
    }
}
