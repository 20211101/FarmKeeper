using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    [SerializeField] GameObject boomEffect;
    private void Awake()
    {
        boomEffect.SetActive(false);
        Invoke("Boom", 3);
        Destroy(gameObject, 4);
    }
    public void Boom()
    {
        SoundPlayer.instance.PlayExplosionSound();
        boomEffect.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20, 1 << 12);
        foreach(Collider c in colliders)
        {
            c.GetComponent<Spawner>().Close();
        }
    }
}
