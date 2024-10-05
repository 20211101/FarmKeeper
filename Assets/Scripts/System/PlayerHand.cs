using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 가질 수 있는 도구의 종류
// 도구를 바꾸고, 실행시키는 기능
// 인벤토리랑 공조해서 아이템 바꾸기 구현
public class PlayerHand : MonoBehaviour
{
    private static PlayerHand _instance;
    public static PlayerHand instance { get => _instance; private set { } }

    [SerializeField]
    // 보여지는 도구 OBj (기능 없음)
    GameObject[] toolObjs;
    [SerializeField]
    InventoryToolPannelUI inventoryToolUI;
    GameObject activatedTool;
    Tool selectedTool;
    PlayerInventory inventory;

    public Tool.type selectedToolType => selectedTool.toolType;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        ChangeTool(Tool.type.Club);
    }

    public void ChangeTool(Tool.type t)
    {
        if (inventory.CanGet(t) == false)
        {
            Debug.Log("안댐");
            InventoryToolPannelUI.instance.CantChangeReaction(t);
            return;
        }
        if (selectedTool != null)
            selectedTool.ClassReset();
        selectedTool = inventory.GetTool(t);
        
        if(activatedTool != null)
            activatedTool.SetActive(false);
        toolObjs[(int)t].SetActive(true);

        activatedTool = toolObjs[(int)t];
        inventoryToolUI.ChangeSelectedTool((int)selectedToolType);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            ChangeTool(Tool.type.Club);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeTool(Tool.type.Spear);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeTool(Tool.type.Granade);
        if (Input.GetMouseButtonDown(0))
            Action();
    }

    void Action()
    {
        if (selectedTool != null)
            selectedTool.Action();
    }
}
