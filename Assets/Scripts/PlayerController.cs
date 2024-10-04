using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¿Ãµø
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
