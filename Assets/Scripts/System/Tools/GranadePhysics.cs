using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadePhysics : MonoBehaviour
{
    [SerializeField]
    GameObject explodeEffect;
    int force = 10;
    Rigidbody rigid;
    SphereCollider explodeCollider;
    int damage = 100;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        explodeCollider = GetComponent<SphereCollider>();
        StartCoroutine(nameof(WaitTimeForExplode));
    }

    IEnumerator WaitTimeForExplode()
    {
        yield return new WaitForSeconds(3);
        explodeEffect.SetActive(true);
        explodeCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        explodeCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public void AddForce(Vector3 dir)
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody>();
        rigid.AddForce(dir * force, ForceMode.Impulse);
        rigid.AddForce(new Vector3(0, 12, 0), ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().Damaged(damage);
        }
    }
}
