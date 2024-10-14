using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Monster
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
    [SerializeField]
    GameObject meat;
    [SerializeField]
    GameObject leather;

    State state = State.Moving;

    public Transform target = null;
    Animator anim;
    MeshRenderer[] mesh;
    CapsuleCollider colider;

    int targetMask = (1 << 10) | (1 << 11);
    float searchRange = 10f;

    MoleMovement moleMovement;
    MoleAttack moleAttack;

    bool trackPlayer = false;

    bool runAwayFlag = false;

    Vector3 pushVec = Vector3.zero;
    public Vector3 PushVec { get => pushVec; }

    private void Awake()
    {
        type = MonsterType.MOLE;

        anim = GetComponent<Animator>();
        mesh = GetComponentsInChildren<MeshRenderer>();
        colider = GetComponent<CapsuleCollider>();

        MAX_HP = (int)MonsterHP.MOLE;
        hp = (int)MonsterHP.MOLE;
        meatAmount = (int)MonsterMeatAmount.MOLE;

        moleMovement = GetComponent<MoleMovement>();
        moleAttack = GetComponent<MoleAttack>();
    }

    public override void BringLife(Transform[] paths)
    {
        base.BringLife(paths);
        ResetSelf(paths);
    }

    public override void ResetSelf(Transform[] paths)
    {

        base.ResetSelf(paths);
        GetComponent<MoleMovement>().ResetSelf(paths);
        GetComponent<MoleAttack>().ResetSelf();

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

        if (trackPlayer)
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
        else if (moleAttack.CheckAttackDistance())
            state = State.Attacking;
        else
            state = State.Tracking;


        if (state == State.Moving)
        {
            moleMovement.MovePath();
        }
        else if (state == State.Tracking)
        {
            moleMovement.MoveTo();
        }
        else if (state == State.Attacking)
        {
            moleAttack.AttackTarget();
        }
        else if(state == State.RunAway)
        {
            moleMovement.RunAway();
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
        foreach (MeshRenderer m in mesh)
            m.material.color = Color.red;

        if (hp <= 0)
        {
            Instantiate(meat, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            Instantiate(leather, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            Instantiate(leather, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            isDying = true;
            anim.SetBool("IsDead", true);
            colider.enabled = false;
            minimapMark.SetActive(false);
            StopCoroutine(nameof(TrackPlayerDuration));
            moleMovement.Dead();
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
        yield return new WaitForSeconds(0.1f);
        foreach (MeshRenderer m in mesh)
            m.material.color = Color.white;
    }


    public override void RunAway()
    {
        if (isDying == true) return;
        runAwayFlag = true;
    }

    public override void Push(Vector3 dir)
    {
        StopCoroutine("PushEnd");
        pushVec = dir;
        moleMovement.Pushed();
        StartCoroutine("PushEnd");
    }

    IEnumerator PushEnd()
    {
        yield return new WaitForSeconds(0.5f);
        pushVec = Vector3.zero;
    }
}
