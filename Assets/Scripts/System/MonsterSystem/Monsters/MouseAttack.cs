using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAttack : MonoBehaviour
{
    int damage = 2;
    int barrigateDamage = 3;
    int storageDamage = 8;

    bool canAttack = true;
    float atkDelay = 0.5f;

    float atkRange = 10f;

    Animator anim;

    Mouse mouse;
    Transform target => mouse.target;

    private void Awake()
    {
        mouse = GetComponent<Mouse>();
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
                temp.Damaged(new DamageInfo( storageDamage, gameObject));
            else
                temp.Damaged(new DamageInfo(barrigateDamage, gameObject));

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
