using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 가질 수 있는 도구의 종류
// 도구를 바꾸고, 실행시키는 기능
// 인벤토리랑 공조해서 아이템 바꾸기 구현
public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    // 보여지는 도구 OBj (기능 없음)
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
