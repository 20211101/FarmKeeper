using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Monster
{
    enum State
    {
        Moving,
        Tracking,
        Attacking,
        RunAway
    }


    [SerializeField]
    GameObject minimapMark;

    State state = State.Moving;

    public Transform target = null;
    Animator anim;
    MeshRenderer[] mesh;
    CapsuleCollider colider;

    int targetMask = (1 << 10) | (1 << 11);
    float searchRange = 10f;

    MouseMovement mouseMovement;
    MouseAttack   mouseAttack;

    bool trackPlayer = false;

    bool runAwayFlag = false;

    private void Awake()
    {
        type = MonsterType.MOUSE;

        anim = GetComponent<Animator>();
        mesh = GetComponentsInChildren<MeshRenderer>();
        colider = GetComponent<CapsuleCollider>();

        MAX_HP = (int)MonsterHP.MOUSE;
        hp = (int)MonsterHP.MOUSE;
        meatAmount = (int)MonsterMeatAmount.MOUSE;

        mouseMovement = GetComponent<MouseMovement>();
        mouseAttack = GetComponent<MouseAttack>();
    }

    public override void BringLife(Transform[] paths)
    {
        base.BringLife(paths);
        ResetSelf(paths);
    }

    public override void ResetSelf(Transform[] paths)
    {

        base.ResetSelf(paths);
        GetComponent<MouseMovement>().ResetSelf(paths);
        GetComponent<MouseAttack>().ResetSelf();

        colider.enabled = true;

        target = null;
        trackPlayer = false;
        hp = MAX_HP;

        minimapMark.SetActive(true);
        foreach (MeshRenderer m in mesh)
            m.material.color = Color.white;

        state = State.Moving;
        isDying = false;
        runAwayFlag = false;
        anim.SetBool("IsDead", false);
    }

    private void Update()
    {
        if (isDying) return;

        if(trackPlayer)
        {

        }
        else
        {
            FindTarget();
        }

        if (runAwayFlag)
            state = State.RunAway;
        else if (target == null)
            state = State.Moving;
        else if (mouseAttack.CheckAttackDistance())
            state = State.Attacking;
        else
            state = State.Tracking;

        if(state == State.Moving)
        {
            mouseMovement.MovePath();
        }
        else if(state == State.Tracking)
        {
            mouseMovement.MoveTo();
        }
        else if(state == State.Attacking)
        {
            mouseAttack.AttackTarget();
        }
        else if(state == State.RunAway)
        {
            mouseMovement.RunAway();
        }

    }
    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRange, targetMask);

        if (colliders.Length == 0)
        {
            target = null;
            return;
        }

        float dist = 0f;
        foreach (Collider c in colliders)
        {
            float temp = (c.transform.position - transform.position).sqrMagnitude;
            if (temp > dist)
            {
                target = c.transform;
                dist = temp;
            }
        }

        if (dist == 0f)
            target = null;
    }


    public override void Damaged(DamageInfo damage)
    {
        if (isDying) return;

        
        if (damage.damager != null && damage.damager.CompareTag("Player"))
        {
            trackPlayer = true;
            StopCoroutine(nameof(TrackPlayerDuration));
            StartCoroutine(nameof(TrackPlayerDuration));
            target = damage.damager.transform;
        }
        hp -= damage.damage;
        foreach(MeshRenderer m in mesh)
            m.material.color = Color.red;
        if(hp <= 0)
        {
            isDying = true;
            runAwayFlag = false;
            anim.SetBool("IsDead", true);
            colider.enabled = false;
            minimapMark.SetActive(false);
            StopCoroutine(nameof(TrackPlayerDuration));
            mouseMovement.Dead();
        }
        else
        {
            StopCoroutine(nameof(ReturnWhiteMesh));
            StartCoroutine(nameof(ReturnWhiteMesh));
        }
    }
    IEnumerator TrackPlayerDuration()
    {
        yield return new WaitForSeconds(5f);
        trackPlayer = false;
    }
    IEnumerator ReturnWhiteMesh()
    {
        yield return new WaitForSeconds (0.1f);
        foreach (MeshRenderer m in mesh)
            m.material.color = Color.white;
    }

    public override void RunAway()
    {
        if (isDying == true) return;
        runAwayFlag = true;
    }
}
