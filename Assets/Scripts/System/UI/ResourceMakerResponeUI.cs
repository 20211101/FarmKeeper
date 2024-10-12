using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceMakerResponeUI : MonoBehaviour
{
    [SerializeField]
    Image image;
    float RESPONE_T = 5f;
    float cur_responeTime = 5f;
    ResourceOrigin parent;
    private void Awake()
    {
        parent = GetComponentInParent<ResourceOrigin>();
    }
    private void OnEnable()
    {
        RESPONE_T = parent.responeTime;
        cur_responeTime = RESPONE_T;
    }

    private void Update()
    {
        cur_responeTime -= Time.deltaTime;
        image.fillAmount = cur_responeTime / RESPONE_T;
        if (cur_responeTime <= 0)
        {
            parent.Respone();
            gameObject.SetActive(false);
        }
    }
}
