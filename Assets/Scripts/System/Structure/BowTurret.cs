using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTurret : Structure
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject bullet;

    [SerializeField] GameObject yAxis;

    [SerializeField] GameObject zAxis;
    [SerializeField] GameObject spawnPos;

    [SerializeField] float radious = 25f;

    [SerializeField] LayerMask monsterLayer;

    Monster target;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        FindTarget();
        if (target != null)
        {
            LookTarget();
            AttackAnim();
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

    public void LookTarget()
    {
        zAxis.transform.LookAt(target.transform.position + new Vector3(0,1,0));
    }

    bool canAttack = true;
    void AttackAnim()
    {
        if (canAttack == false) return;
        canAttack = false;
        anim.SetTrigger("Attack");
    }
    public void Attack()
    {
        SoundPlayer.instance.PlayShootSound();
        GameObject bullet = BulletPool.instance.SendBullet(BulletType.Arrow);
        bullet.transform.position = spawnPos.transform.position;
        bullet.transform.rotation = zAxis.transform.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<BulletArrow>().maker = gameObject;
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.9f);
        anim.SetTrigger("Reload");
        canAttack = true;
    }
}
