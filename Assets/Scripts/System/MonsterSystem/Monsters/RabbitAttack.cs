using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAttack : MonoBehaviour
{
    int damage = 3;
    int storageDamage = 6;

    bool canAttack = true;
    float atkDelay = 0.5f;

    float atkRange = 10f;

    Animator anim;

    Rabbit rabbit;
    Transform target => rabbit.target;

    private void Awake()
    {
        rabbit = GetComponent<Rabbit>();
        anim = GetComponent<Animator>();
    }
    public void AttackTarget()
    {
        if (canAttack == false) return;
        canAttack = false;
        anim.SetTrigger("Attack");
        StartCoroutine(nameof(MakeDelay));
    }
    IEnumerator MakeDelay()
    {
        yield return new WaitForSeconds(atkDelay);
        canAttack = true;
    }
    public void Attack()
    {
        if (target == null) return;
        Damagable temp = target.GetComponent<Damagable>();
        if (temp is Structure)
        {
            if (target.CompareTag("Storage"))
                temp.Damaged(new DamageInfo(storageDamage, gameObject));
            else
                temp.Damaged(new DamageInfo(damage, gameObject));

        }
        else
            temp.Damaged(new DamageInfo(damage, gameObject));
    }

    public bool CheckAttackDistance()
    {
        float dist = (target.transform.position - transform.position).sqrMagnitude;
        return (dist < atkRange * atkRange);
    }

    public void ResetSelf()
    {
        canAttack = true;
    }
}
