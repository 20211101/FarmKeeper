using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Spear : Tool
{
    Animator anim;
    [SerializeField]
    GameObject Spear;
    [SerializeField]
    Transform SpawnPos;
    bool _isAttacking = false;
    bool isAttacking { get { return _isAttacking; } set { Debug.Log(value); _isAttacking = value; } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        toolCnt = 100;
    }

    public override void Action()
    {
        if (isAttacking)
            return;

        isAttacking = true;
        anim.SetTrigger("Attack");
    }
    public override void Setting()
    {
        anim.SetLayerWeight(2, 1);
    }
    public override void ClassReset()
    {
        StopCoroutine(nameof(AtkDelay));
        anim.SetLayerWeight(2, 0);
        isAttacking = false;
    }

    public void ThrowSpear()
    {
        GameObject temp = Instantiate(Spear);
        temp.transform.position = SpawnPos.position;
        temp.GetComponent<SpearPhysics>().AddForce(transform.forward);
        StartCoroutine(nameof(AtkDelay));

        toolCnt--;
        if (toolCnt < 1)
        {
            PlayerHand.instance.ChangeTool(type.Club);
        }
    }
    IEnumerator AtkDelay()
    {
        yield return new WaitForSeconds(0.73f);
        isAttacking = false;
    }
}
