using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonsterMovement
{
    int speed = (int)MonsterSpeed.MOUSE;
    int targetIdx = 0;
    int targetChangeDistance = 3;

    private void Start()
    {
        monster.resetDel += () => { targetIdx = 0; };
    }

    private void Update(){
        if (isMoving == false) return;
        if(target == null){
            Debug.Log("타겟 미설정 오류");
            return;
        }


        Vector3 temp = (target.position - transform.position).normalized;
        Vector3 dir = new Vector3(temp.x, 0, temp.z);
        dir = dir.normalized;
        float yAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        //float yAngle = Vector3.Angle(Vector3.forward, dir);

        transform.rotation = Quaternion.Euler(0, yAngle, 0);
        transform.position += dir * speed * Time.deltaTime;
        
        if (targetChangeDistance * targetChangeDistance > (target.position - transform.position).sqrMagnitude)
            ChangeTarget();
    }

    public void ChangeTarget()
    {
        ++targetIdx;
        if (targetIdx == targetArr.Length)
        {
            target = null;
        }
        else
            target = targetArr[targetIdx];
    }
}
