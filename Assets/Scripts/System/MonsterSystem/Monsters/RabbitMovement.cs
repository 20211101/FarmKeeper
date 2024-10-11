using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : Movement
{
    Rabbit rabbit;
    Rigidbody rigid;

    int _speed = 12;
    float speed { get => _speed * speedIngagement; }

    Transform[] paths;
    Transform targetPath;
    Transform trackTarget => rabbit.target;
    int targetIdx = 0;
    int targetChangeDistance = 5;

    private void Awake()
    {
        rabbit = GetComponent<Rabbit>();
        rigid = GetComponent<Rigidbody>();
    }


    IEnumerator CoJump()
    {
        float MOVETIME = 1.5f;
        float time = 0f;
        Vector3 startPos = transform.position;
        Vector3 startDir = transform.up;
        Vector3 topDir = transform.forward;
        while (time <= MOVETIME)
        {
            if(time <= MOVETIME / 2)
            {
            //    startPos, top, time / (MOVETIME / 2f)
                Vector3 dir = Vector3.Lerp(startDir, topDir, time / (MOVETIME / 2f)).normalized;
                rigid.velocity = dir * 30;
            }
            else
            {
                Vector3 dir = Vector3.Lerp(topDir, -startDir, time / (MOVETIME)).normalized;
                rigid.velocity = dir * 30;
            }

            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log("종료");
        rigid.velocity = Vector3.zero;
    }

    bool isJumpStart = false;
    float MOVETIME = 1.5f;
    float time = 0f;


    Vector3 startPos;
    Vector3 startDir;
    Vector3 top ;
    Vector3 topDir;
    public void MovePath()
    {

        if (isJumpStart)
        {
            if (time <= MOVETIME)
            {
                if (time <= MOVETIME / 2)
                {
                    Vector3 dir = Vector3.Lerp(startDir, topDir, time / (MOVETIME / 2f)).normalized;
                    rigid.velocity = dir * 30;
                }
                else
                {
                    Vector3 dir = Vector3.Lerp(topDir, -startDir, time / (MOVETIME)).normalized;
                    rigid.velocity = dir * 30;
                }

                time += Time.deltaTime;
            }
            else
            {
                isJumpStart = false;
                rigid.velocity = Vector3.zero;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.BoxCast(transform.position, new Vector3(1f, 1, 0.1f), transform.forward, out hit, transform.rotation, 7, (1 << 10)))
            {
                Debug.Log("히트여 히트!");
                isJumpStart = true;
                MOVETIME = 1.5f;
                time = 0f;

                startDir = transform.up;
                topDir = transform.forward;
            }
            else
            {
                if (targetPath == null)
                {
                    Debug.Log("경로 미설정 오류");
                    return;
                }


                Vector3 temp = (targetPath.position - transform.position).normalized;
                Vector3 dir = new Vector3(temp.x, 0, temp.z);
                dir = dir.normalized;
                float yAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
                //float yAngle = Vector3.Angle(Vector3.forward, dir);

                transform.rotation = Quaternion.Euler(0, yAngle, 0);
                rigid.velocity = dir * speed + new Vector3(0, rigid.velocity.y, 0);

                if (targetChangeDistance * targetChangeDistance > (targetPath.position - transform.position).sqrMagnitude)
                    ChangeTargetPath();
            }
        }
    }

    public void MoveTo()
    {
        if (trackTarget == null)
        {
            Debug.Log("추적 대상 미설정 오류");
            return;
        }
        Vector3 temp = (trackTarget.position - transform.position).normalized;
        Vector3 dir = new Vector3(temp.x, 0, temp.z);
        dir = dir.normalized;
        float yAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        //float yAngle = Vector3.Angle(Vector3.forward, dir);

        transform.rotation = Quaternion.Euler(0, yAngle, 0);
        rigid.velocity = dir * speed;
    }

    public void ChangeTargetPath()
    {
        ++targetIdx;
        if (targetIdx >= paths.Length)
        {
            targetPath = null;
        }
        else
            targetPath = paths[targetIdx];
    }

    public void ResetSelf(Transform[] _paths)
    {
        paths = _paths;
        rigid.useGravity = true;
        targetIdx = 0;
        targetPath = paths[targetIdx];
    }

    public void RunAway()
    {
        if (trackTarget != null)
        {
            Vector3 temp = (trackTarget.position - transform.position).normalized;
            Vector3 dir = new Vector3(temp.x, 0, temp.z);
            dir = -dir.normalized;
            float yAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);

            transform.rotation = Quaternion.Euler(0, yAngle, 0);
            rigid.velocity = dir * speed;
        }
        else
        {
            rigid.velocity = Vector3.forward * speed;
        }
    }

    public void Dead()
    {
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
    }
    void Jump()
    {
        GetComponent<Rigidbody>().AddForce((transform.forward * 2 + transform.up * 5).normalized * 15, ForceMode.Impulse);
    }
}
