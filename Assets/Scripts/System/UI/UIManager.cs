using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject createToolPannel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            createToolPannel.SetActive(!createToolPannel.activeSelf);
    }
}
