using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : SingletonBehavior<PlayerInputManager>{
    #region CONFIG
        [SerializeField] KeyCode jumpKey;
        [SerializeField] KeyCode swapKey;
        [SerializeField] KeyCode aktKey;
        public VariableJoystick variableJoystick;
    #endregion
        float h;
        bool hDown;
        bool hUp;
        bool jDown;
        bool aDown;

    public KeyCode SwapKey{
        get {return swapKey;}
    }
    public float H{
        get => h;
    }
    public bool HDown{
        get => hDown;
    }
    public bool HUp{
        get =>hUp;
    }
    public bool JDown{
        get => jDown;
    }
    public bool ADown{
        get => aDown;
    }

    protected override void OnAwake(){
        jumpKey = KeyCode.Space;
        swapKey = KeyCode.LeftShift;
        aktKey = KeyCode.Z;
    }

    void Update(){
        
        h       = Input.GetAxisRaw("Horizontal") == 0 ? variableJoystick.Horizontal : Input.GetAxisRaw("Horizontal");
        hDown   = (Input.GetAxisRaw("Horizontal") != 0 || variableJoystick.Horizontal != 0) ? true : false;
        jDown   = (Input.GetKeyDown(jumpKey))? true : false;
        aDown   = (Input.GetKeyDown(aktKey))? true : false;
        hUp     = !hDown;
    }

    public void JButtonDown(){
        jDown = true;
    }
    public void JButtonUp(){
        jDown = false;
    }
    public void AButtonDown(){
        aDown = true;
    }
    public void AButtonUp(){
        aDown = false;
    }
}