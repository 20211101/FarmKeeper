using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleMovement : Movement
{
    Mole mole;
    Rigidbody rigid;

    int _speed = 6;
    float speed { get => _speed * speedIngagement; }

    Transform[] paths;
    Transform targetPath;
    Transform trackTarget => mole.target;
    int targetIdx = 0;
    int targetChangeDistance = 5;

    private void Awake()
    {
        mole = GetComponent<Mole>();
        rigid = GetComponent<Rigidbody>();
    }

    public void MovePath()
    {
        if (targetPath == null)
        {
            Debug.Log("경로 미설정 오류");
            return;
        }
        if (mole.PushVec != Vector3.zero) return;

        Vector3 temp = (targetPath.position - transform.position).normalized;
        Vector3 dir = new Vector3(temp.x, 0, temp.z);
        dir = dir.normalized;
        float yAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        //float yAngle = Vector3.Angle(Vector3.forward, dir);

        transform.rotation = Quaternion.Euler(0, yAngle, 0);
        rigid.velocity = dir * speed;

        if (targetChangeDistance * targetChangeDistance > (targetPath.position - transform.position).sqrMagnitude)
            ChangeTargetPath();
    }

    public void MoveTo()
    {
        if (trackTarget == null)
        {
            Debug.Log("추적 대상 미설정 오류");
            return;
        }
        if (mole.PushVec != Vector3.zero) return;

        Vector3 temp = (trackTarget.position - transform.position).normalized;
        Vector3 dir = new Vector3(temp.x, 0, temp.z);
        dir = dir.normalized;
        float yAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        //float yAngle = Vector3.Angle(Vector3.forward, dir);

        transform.rotation = Quaternion.Euler(0, yAngle, 0);
        rigid.velocity = dir * speed;
    }

    public void Pushed()
    {
        rigid.AddForce(mole.PushVec * 5, ForceMode.Impulse);
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
        rigid.useGravity = true;
        paths = _paths;
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
            transform.rotation = Quaternion.Euler(Vector3.forward);
            rigid.velocity = Vector3.forward * speed;
        }
    }

    public void Dead()
    {
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Monster"))
        {
            if (transform.position.y > collision.transform.position.y)
                transform.position = new Vector3(transform.position.x, collision.transform.position.y, transform.position.z);
        }
    }
}
