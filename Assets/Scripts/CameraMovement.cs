using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    // 시작 시 초기화, 휠 스크롤에 따라 증감
    float distanceOffset;                           // 카메라 거리 기본값
    // 대입 없음
    float distanceMinOffset = 1;
    // 시작 시 재초기화, 이후 대입 없음
    float distanceMaxOffset = 10;
    float xMoveOffset = 200f;
    float yMoveOffset = 150f;
    const float yMoveMin = -45;
    const float yMoveMax = 45f;
    float wheelMoveOffset = 100f;
    

    private void Awake()
    {
        distanceOffset = Vector3.Distance(player.position, transform.position);
        distanceMaxOffset = distanceOffset;
    }

    private void Update()
    {
        XRotate();
        YRotate();
        SetDistanceOffset();
        SetPosition();
    }

    void XRotate()
    {
        float movX = Input.GetAxis("Mouse X");

        if (movX > 0)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * xMoveOffset, 0), Space.World);
        }
        else if (movX < 0)
        {
            transform.Rotate(new Vector3(0, -Time.deltaTime * xMoveOffset, 0), Space.World);
        }
    }
    void YRotate()
    {
        float movY = Input.GetAxis("Mouse Y");
        float X = transform.rotation.eulerAngles.x;
        
        if (movY > 0)
        {
            if (X < 360f+yMoveMin && X > 180f) return;
            transform.Rotate(new Vector3(-Time.deltaTime * yMoveOffset, 0, 0), Space.Self);
        }
        else if (movY < 0)
        {
            if (X > yMoveMax && X < 180f) return;
            transform.Rotate(new Vector3(Time.deltaTime * yMoveOffset, 0, 0), Space.Self);
        }


    }
    int mask = (1 << 3) | (1 << 9);
    float finalDistance;
    void SetPosition()
    {
        RaycastHit hit;
        Physics.Raycast(player.position, -transform.forward, out hit, distanceOffset, ~mask);

        if (hit.collider != null) finalDistance = hit.distance;
        else finalDistance = distanceOffset;

        // 플레이어 위치 - 카메라 위치
        Vector3 finalOffset = player.position - (transform.forward * finalDistance);
        transform.position = finalOffset;
    }

    void SetDistanceOffset()
    {
        float wheelVal = Input.GetAxis("Mouse ScrollWheel");
        if (wheelVal < 0)
        {
            distanceOffset -= wheelMoveOffset * Time.deltaTime;
        }
        else if (wheelVal > 0)
        { 
            distanceOffset += wheelMoveOffset * Time.deltaTime;
        }

        distanceOffset = Mathf.Clamp(distanceOffset, distanceMinOffset, distanceMaxOffset);
    }
}
