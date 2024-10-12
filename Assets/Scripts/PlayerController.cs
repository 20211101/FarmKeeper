using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¿Ãµø
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    PlayerHealth playerHealth;
    Animator anim;

    Camera cam;
    [SerializeField] private float speed = 8.0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }


    Vector3 groundDir;
    public LayerMask msk;
    void Update()
    {
        if (playerHealth.isFallen == true) return;

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);

        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        anim.SetFloat("xInput", xInput);
        anim.SetFloat("zInput", zInput);
        
        groundDir = new Vector3(xInput, 0, zInput).normalized;
        
        controller.Move((transform.rotation * groundDir) * speed * Time.deltaTime);
    }

}
