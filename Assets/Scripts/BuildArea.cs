using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    public enum State { emety, filled};

    public State state = State.emety;
    public static Material hilightedMaterial;
    static BuildArea _hilightedObj;
    public static BuildArea hilightedObj
    {
        get {
            
            return _hilightedObj;
        }
        set { 
            if(value == null)
            {
                if(_hilightedObj != null)
                    _hilightedObj.render.material = _hilightedObj.originMat;
            }
            else
            {
                if (_hilightedObj != null)
                {
                    _hilightedObj.render.material = _hilightedObj.originMat;
                }
                value.render.material = hilightedMaterial;
            }
            _hilightedObj = value;
        }

    }
    [SerializeField]
    Material _hilightedMaterial;
    public MeshRenderer render;
    public Material originMat;
    public bool highlight = false;
    void Start()
    {
        if (_hilightedMaterial != null)
            hilightedMaterial = _hilightedMaterial;
        render = GetComponent<MeshRenderer>();
        originMat = GetComponent<MeshRenderer>().material;
    }
}
