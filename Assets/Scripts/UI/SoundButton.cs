using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, ISubmitHandler, IPointerDownHandler
{
    [SerializeField]
    bool m_selectSound = true,
        m_submitSound  = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_selectSound)
            AkSoundEngine.PostEvent("UpDown", gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(m_selectSound)
		    AkSoundEngine.PostEvent("UpDown", gameObject);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if(m_submitSound)
            AkSoundEngine.PostEvent("Select", gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(m_submitSound)
            AkSoundEngine.PostEvent("Select", gameObject);
    }
}