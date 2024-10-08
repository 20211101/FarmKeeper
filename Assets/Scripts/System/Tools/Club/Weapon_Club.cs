using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 액션(공격)
public class Weapon_Club : Tool
{
    Animator anim;
    [SerializeField]
    GameObject hitBox;
    bool _isAttacking = false;
    bool isAttacking { get { return _isAttacking; } set {_isAttacking = value; } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        toolCnt = 1;
    }

    public override void Action()
    {
        if(hitBox == null)
        {
            Debug.Log("히트박스 안 넣음");
            return;
        }
        if (isAttacking)
        {
            return;
        }
        
        anim.SetTrigger("Attack");
        isAttacking = true;
    }
    public override void Setting()
    {
        anim.SetLayerWeight(1, 1);
    }
    public override void ClassReset()
    {
        anim.SetLayerWeight(1, 0);
        StopCoroutine(nameof(OffBox));
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
