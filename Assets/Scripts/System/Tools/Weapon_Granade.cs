using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Granade : Tool
{
    Animator anim;
    [SerializeField]
    GameObject Granade;
    [SerializeField]
    Transform SpawnPos;
    bool _isAttacking = false;
    bool isAttacking { get { return _isAttacking; } set { Debug.Log(value); _isAttacking = value; } }

    private void Start()
    {
        toolCnt = _toolCnt;
        anim = GetComponent<Animator>();
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
        anim.SetLayerWeight(3, 1);
    }
    public override void ClassReset()
    {
        StopCoroutine(nameof(AtkDelay));
        anim.SetLayerWeight(3, 0);
        isAttacking = false;
    }

    public void ThrowGranade()
    {
        GameObject temp = Instantiate(Granade);
        temp.transform.position = SpawnPos.position;
        temp.GetComponent<GranadePhysics>().AddForce(transform.forward);
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
