using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    Camera cam;
    [SerializeField] private float speed = 8.0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    // ¹æ¸ÁÀÌ °ø°Ý
    void Attack()
    {
        Debug.Log(gameObject.name);
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, 5), Color.red, 3, true);
        RaycastHit[] hits;
        hits = Physics.BoxCastAll(transform.position , Quaternion.Euler(transform.forward) * new Vector3(0.0112f, 0.6984f, 0.521f), transform.forward, Quaternion.identity
            , 1);
        foreach (RaycastHit i in hits)
            Debug.Log(i.collider.gameObject.name);
    }

    Vector3 groundDir;
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);
        
        anim.SetFloat("xInput", InputManager.xInput);
        anim.SetFloat("zInput", InputManager.zInput);
        
        groundDir = new Vector3(InputManager.xInput, 0, InputManager.zInput).normalized;
        
        controller.Move((transform.rotation * groundDir) * speed * Time.deltaTime);
    }
}
