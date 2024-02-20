using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    private AudioClip clip;

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.SFXPlay("hover", clip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.SFXPlay("hover", clip);
    }
}
