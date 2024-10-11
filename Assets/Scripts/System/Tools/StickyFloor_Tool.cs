using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyFloor_Tool : Tool
{
    [SerializeField]
    GameObject grid;
    [SerializeField]
    GameObject debugBall;
    [SerializeField]
    GameObject Turret;
    [SerializeField]
    GameObject miniature;
    bool isOnHand = false;
    private void Start()
    {
        toolCnt = 3;
        grid.SetActive(false);
        debugBall.SetActive(false);
        Turret.SetActive(false);
        miniature.SetActive(false);
    }
    public override void Action()
    {
        if (isOnHand == false) return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position + new Vector3(0, 4, 0), Camera.main.transform.forward, out hit, 30f))
        {
            if (hit.collider.CompareTag("BuildArea"))
            {
                BuildArea temp = hit.collider.GetComponent<BuildArea>();
                if (temp.state == BuildArea.State.filled)
                    return;

                temp.state = BuildArea.State.filled;
                GameObject g = Instantiate(Turret, hit.collider.transform.position, hit.transform.rotation);
                g.GetComponent<Structure>().builtedArea = temp;

                toolCnt--;
                if (toolCnt < 1)
                {
                    PlayerHand.instance.ChangeTool(type.Club);
                }
            }
        }
    }
    private void Update()
    {
        if (isOnHand == false) return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position + new Vector3(0, 4, 0), Camera.main.transform.forward, out hit, 30f))
        {
            debugBall.transform.position = hit.point;
            if (hit.collider.CompareTag("BuildArea"))
            {
                BuildArea temp = hit.collider.GetComponent<BuildArea>();
                if (temp.state == BuildArea.State.filled)
                {
                    BuildArea.hilightedObj = null;
                    return;
                }
                BuildArea.hilightedObj = hit.collider.GetComponent<BuildArea>();

                if (!miniature.activeSelf) miniature.SetActive(true);
                miniature.transform.position = hit.collider.transform.position;
                miniature.transform.rotation = hit.transform.rotation;
                if (Input.GetMouseButtonDown(1))
                {
                    Instantiate(Turret, hit.collider.transform.position, hit.transform.rotation);
                }
            }
            else
            {
                if (miniature.activeSelf) miniature.SetActive(false);
                BuildArea.hilightedObj = null;
            }
        }
    }
    public override void Setting()
    {
        isOnHand = true;
        grid.SetActive(true);
        debugBall.SetActive(true);
        Turret.SetActive(true);
        miniature.SetActive(true);
    }

    public override void ClassReset()
    {
        isOnHand = false;
        grid.SetActive(false);
        debugBall.SetActive(false);
        Turret.SetActive(false);
        miniature.SetActive(false);
    }
}
