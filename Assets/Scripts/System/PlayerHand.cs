using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    GameObject[] toolObjs;
    GameObject activatedTool;
    Tool selectedTool;
    PlayerInventory inventory;

    private void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        ChangeTool(Tool.type.Club);
    }

    void ChangeTool(Tool.type t)
    {
        if (inventory.CanGet(t) == false) return;

        if (selectedTool != null)
            selectedTool.ClassReset();
        selectedTool = inventory.GetTool(t);
        
        if(activatedTool != null)
            activatedTool.SetActive(false);
        toolObjs[(int)t].SetActive(true);

        activatedTool = toolObjs[(int)t];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Action();
    }

    void Action()
    {
        if (selectedTool != null)
            selectedTool.Action();
    }
}
