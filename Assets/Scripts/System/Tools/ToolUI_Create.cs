using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolUI_Create : MonoBehaviour,IPointerClickHandler 
{
    [SerializeField]
    CreateToolManager manager;
    
    [Header("ToolInfo")]
    [SerializeField]
    Image toolImg;
    [SerializeField]
    ToolRecipy recipy;
    [SerializeField]
    [TextArea(1, 10)]
    string description;

    public Image ToolImg { get => toolImg; }
    public ToolRecipy Recipy { get => recipy; }
    public string Description { get => description; }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.ShowInfo(this);
    }
}
