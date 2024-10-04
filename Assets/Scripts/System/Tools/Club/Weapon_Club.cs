using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Club : Tool
{
    Animator anim;
    [SerializeField]
    GameObject hitBox;
    bool _isAttacking = false;
    bool isAttacking { get { return _isAttacking; } set { Debug.Log(value); _isAttacking = value; } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Action()
    {
        if(hitBox == null)
        {
            Debug.Log("히트박스 안 넣음");
            return;
        }
        if (isAttacking)
            return;
        
        isAttacking = true;
        anim.SetTrigger("Attack");
    }
    public override void Setting()
    {
        anim.SetLayerWeight(1, 1);
    }
    public override void ClassReset()
    {
        anim.SetLayerWeight(1, 0);
        StopCoroutine(nameof(OffBox));
        isAttacking = false;
        hitBox.SetActive(false);
        isAttacking = false;
    }

    // 애니메이션 이벤트로 호출
    public void ActiveHitBox()
    {
        hitBox.SetActive(true);
        StartCoroutine(nameof(OffBox));
    }

    IEnumerator OffBox()
    {
        yield return new WaitForSeconds(0.1f);
        hitBox.SetActive(false);
        yield return new WaitForSeconds(0.63f);
        isAttacking = false;
    }
}
