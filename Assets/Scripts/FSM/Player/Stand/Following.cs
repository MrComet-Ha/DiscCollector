using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    #region Config
    [SerializeField] Vector3 offset;
    [SerializeField] Player parent;
    #endregion
    SpriteRenderer spr;
    
    
    void OnEnable(){
        spr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        Follow();
    }

    
    void Follow(){
        spr.flipX = parent.Spr.flipX;
        float turn = spr.flipX ? -1 : 1;
        Vector3 trueOffset = new Vector3(offset.x * turn, offset.y, offset.z);
        transform.position = parent.transform.position + trueOffset;
    }

}
