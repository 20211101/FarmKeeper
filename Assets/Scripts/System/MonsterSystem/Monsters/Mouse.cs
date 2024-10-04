using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Monster
{
    [SerializeField]
    GameObject minimapMark;

    int damage = 4;
    float atkDelay = 0.5f;
    Structure target = null;
    Animator anim;
    MeshRenderer[] mesh;
    CapsuleCollider colider;

    private void Awake()
    {
        type = MonsterType.MOUSE;

        anim = GetComponent<Animator>();
        mesh = GetComponentsInChildren<MeshRenderer>();
        colider = GetComponent<CapsuleCollider>();

        resetDel += () => colider.enabled = true;

        hp = (int)MonsterHP.MOUSE;
        meatAmount = (int)MonsterMeatAmount.MOUSE;
    }

    private void Update()
    {
        if (isDying) return;
        if (canAttack == false) return;
        if (isAttacking == true) return;

        isAttacking = true;

        anim.SetTrigger("Attack");

        StartCoroutine(nameof(WaitAtkDelay));
    }

    IEnumerator WaitAtkDelay()
    {
        yield return new WaitForSeconds(atkDelay);
        isAttacking = false;
    }

    public override void ResetSelf()
    {
        base.ResetSelf();
        target = null;
        foreach (MeshRenderer m in mesh)
            m.material.color = Color.white;
        hp = (int)MonsterHP.MOUSE;
        minimapMark.SetActive(true);
    }

    protected override void Attack()
    {
        if (isDying) return;
        if (target == null || target.gameObject.activeSelf == false)
        {
            target = null;
            canAttack = false;
            movement.StartMoving();
            return;
        }

        target.Damaged(damage);
    }

    public void StartAttack(Structure s)
    {
        target = s;
        canAttack = true;
    }

    public override void Damaged(int damage)
    {
        if (isDying) return;

        hp -= damage;
        foreach(MeshRenderer m in mesh)
            m.material.color = Color.red;

        if(hp <= 0)
        {
            isDying = true;
            anim.SetBool("IsDead", true);
            colider.enabled = false;
            movement.StopMoving();
            minimapMark.SetActive(false);
        }
        else
        {
            StartCoroutine(nameof(ReturnWhiteMesh));
        }
    }
    IEnumerator ReturnWhiteMesh()
    {
        yield return new WaitForSeconds (0.1f);
        foreach (MeshRenderer m in mesh)
            m.material.color = Color.white;

    }

    protected override void Dead()
    {
        resetDel();

    }
}
