using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTurret : Structure
{
    [SerializeField] GameObject yAxis;

    [SerializeField] GameObject zAxis;

    [SerializeField] float radious = 15f;

    [SerializeField] LayerMask monsterLayer;

    Monster target;

    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject bulletSpawnPos;
    [SerializeField]
    GameObject bulletSpawnDir;
    [SerializeField]
    float pow = 10;


    private void Update()
    {
        FindTarget();
        if (target != null)
        {
            LookTarget();
            Attack();
        }
    }

    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radious, monsterLayer);

        if (colliders == null)
        {
            target = null;
            return;
        }

        float dist = 10000f;
        foreach (Collider c in colliders)
        {
            float temp = (c.transform.position - transform.position).sqrMagnitude;
            if (temp < dist)
            {
                target = c.GetComponent<Monster>();
                dist = temp;
            }
        } 

        if (dist == 10000f)
            target = null;
    }
    const float offset = 5;
    float distance10 = 10 + offset;
    float distance20 = 12 + offset;
    float distance30 = 14 + offset;
    float distance45 = 15 + offset;
    public void LookTarget()
    {
        float distanceSqared = (target.transform.position - transform.position).sqrMagnitude + offset * offset;
        float yAngle = 0;
        if (distanceSqared > distance30 * distance30)
            yAngle = 45;
        else if (distanceSqared > distance20 * distance20)
            yAngle = 30;
        else if (distanceSqared > distance10 * distance10)
            yAngle = 20;
        else 
            yAngle = 10;

        yAxis.transform.localRotation = Quaternion.Euler(yAngle, 0, 0);
        zAxis.transform.LookAt(target.transform.position);
    }

    bool canAttack = true;
    void Attack()
    {
        if (canAttack == false) return;
        SoundPlayer.instance.PlayShootSound();
        SoundPlayer.instance.PlayShootSound();
        canAttack = false;
        GameObject g = Instantiate(bullet, bulletSpawnPos.transform.position, bulletSpawnDir.transform.rotation);
        g.GetComponent<BulletMortar>().maker = gameObject;
        g.GetComponent<Rigidbody>().AddForce(g.transform.forward * pow, ForceMode.Impulse);

        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        canAttack = true;
    }

}
