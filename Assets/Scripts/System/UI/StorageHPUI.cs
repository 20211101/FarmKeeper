using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StorageHPUI : MonoBehaviour
{
    private static StorageHPUI _instance;
    public static StorageHPUI instance { get => _instance; private set { } }

    [SerializeField]
    Storage storage;

    [SerializeField]
    Image fill;
    [SerializeField]
    TextMeshProUGUI hpText;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateUI()
    {
        hpText.text = $"{storage.Hp}/{storage.MaxHp}";
        fill.fillAmount = (float)storage.Hp / (float)storage.MaxHp;
    }
}
