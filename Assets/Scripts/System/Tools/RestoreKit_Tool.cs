using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreKit_Tool : Tool
{
    Animator anim;
    bool _isAttacking = false;
    bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

    int healAmount = 20;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        InventoryToolPannelUI.instance.ChangeToolCount((int)toolType, _toolCnt);
    }

    public override void Action()
    {
        if (isAttacking)
        {
            return;
        }

        anim.SetTrigger("Restore");
        isAttacking = true;
        StartCoroutine("IsAttackingFalse");
    }
    IEnumerator IsAttackingFalse()
    {
        yield return new WaitForSeconds(1.7f);
        isAttacking = false;
    }
    public override void Setting()
    {
        anim.SetLayerWeight(4, 1);
        isAttacking = false;
    }
    public override void ClassReset()
    {
        anim.SetLayerWeight(4, 0);
        isAttacking = false;
        StopCoroutine("IsAttackingFalse");
    }

    // 애니메이션 이벤트로 호출
    public void Restore()
    {
        RaycastHit hit;
        if(Physics.BoxCast(transform.position, Vector3.one * 3, transform.forward, out hit, transform.rotation, 1.5f, (1 << 10)))
        {
            hit.transform.GetComponent<Damagable>().Heal(healAmount);
            toolCnt--;
            if (toolCnt < 1)
            {
                PlayerHand.instance.ChangeTool(type.Club);
            }
        }
    }

}
