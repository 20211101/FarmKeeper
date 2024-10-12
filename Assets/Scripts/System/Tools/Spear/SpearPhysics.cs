using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPhysics : MonoBehaviour
{
    public GameObject maker;
    Rigidbody rigid;
    public float damageIncreaseRate = 0.1f;
    public bool hit = false;
    public int force = 3;
    public int MAX_DAMAGE = 100;
    public int MIN_DAMAGE = 25;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (hit)
            return;
        if (rigid.velocity == Vector3.zero) return;
        damageIncreaseRate += 0.1f * Time.deltaTime;

        Vector3 v = rigid.velocity * (3 * Time.deltaTime);
        rigid.velocity += new Vector3(v.x, -0.002f, v.z);
        transform.rotation = Quaternion.LookRotation(rigid.velocity.normalized);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hit) return;
        StartCoroutine(nameof(DestroyDelay));
        hit = true;
        if(collision.gameObject.tag == "Monster")
        {
            float rate = damageIncreaseRate;
            int damage = (int)Mathf.Clamp(rigid.velocity.magnitude, MIN_DAMAGE, MAX_DAMAGE);
            collision.gameObject.GetComponent<Monster>().Damaged(new DamageInfo(damage, maker));
        }
        rigid.velocity = Vector3.zero;
    }
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    public void AddForce(Vector3 dir)
    {
        if(rigid == null)
            rigid = GetComponent<Rigidbody>();
        rigid.AddForce(dir * force, ForceMode.Impulse);
        rigid.AddForce(new Vector3(0,5,0), ForceMode.Impulse);
    }
}
