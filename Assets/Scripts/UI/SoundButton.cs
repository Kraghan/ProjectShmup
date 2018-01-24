using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, ISubmitHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AkSoundEngine.PostEvent("UpDown", gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
		AkSoundEngine.PostEvent("UpDown", gameObject);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        AkSoundEngine.PostEvent("Select", gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AkSoundEngine.PostEvent("Select", gameObject);
    }
}