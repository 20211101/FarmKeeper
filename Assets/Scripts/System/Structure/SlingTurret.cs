using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingTurret : Structure
{
    [SerializeField] GameObject yAxis;

    [SerializeField] GameObject zAxis;

    [SerializeField] float radious = 25f;

    [SerializeField] LayerMask monsterLayer;

    Monster target;

    private void Update()
    {
        FindTarget();
        if(target != null)
        {
            LookTarget();
            Attack();
        }
    }

    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radious, monsterLayer);

        if(colliders == null)
        {
            target = null;
            return;
        }

        float dist = 10000f;
        foreach(Collider c in colliders)
        {
            float temp = (c.transform.position -transform.position).sqrMagnitude;
            if (temp < dist)
            {
                target = c.GetComponent<Monster>();
                dist = temp;
            }
        }

        if (dist == 10000f)
            target = null;
    }

    public void LookTarget()
    {
        Vector3 temp = (target.transform.position - transform.position).normalized;
        Vector3 dir = new Vector3(temp.x, 0, temp.z);
        dir = dir.normalized;
        float yAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        //float yAngle = Vector3.Angle(Vector3.forward, dir);

        yAxis.transform.rotation = Quaternion.Euler(0, yAngle, 0);
        zAxis.transform.LookAt(target.transform.position);
    }

    bool canAttack = true;
    void Attack()
    {
        if (canAttack == false) return;
        canAttack = false;
        GameObject bullet = BulletPool.instance.SendBullet(BulletType.Rock);
        bullet.transform.position = zAxis.transform.position;
        bullet.transform.rotation = zAxis.transform.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet_Rock>().maker = gameObject;
        
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.9f);
        canAttack = true;
    }
}
