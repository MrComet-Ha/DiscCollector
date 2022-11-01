using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StandType { StarPlatina = 0, SPTW, MR, HG, SC, TW, CD, TH, KQ, SF, KC, CM}
public enum StandStates{ Idle = 0, Attack, Skill1, Skill2, Swap, GlobalStates}

public class Stand : BaseStateMech{

    #region Config
    public Vector3 offset;
    public Player parent;
    public Sprite[] Sprites;
    #endregion

    #region Reference
    public SpriteRenderer Spr;
    public GameObject[] attackPrefab;
    ObjectPool objectPool;
    #endregion
    
    State<Stand>[] states;
    StateMachine<Stand> stateMachine;
    public StandType standType{ private set; get;}
    public StandStates CurState{ private set; get;}

    void Awake(){
        Setup();
    }
    public override void Setup(){
        base.Setup();

        parent = GetComponentInParent<Player>();
        Spr = GetComponent<SpriteRenderer>();

        states = new State<Stand>[6];
        states[(int)StandStates.Idle] = new StandBaseStates.Idle();
        states[(int)StandStates.Swap] = new StandBaseStates.Swap();
        states[(int)StandStates.GlobalStates] = new StandBaseStates.GlobalStates();

        SetStand(StandType.StarPlatina);

        stateMachine = new StateMachine<Stand>();
        stateMachine.Setup(this,states[(int)StandStates.Idle]);
        stateMachine.SetGlobalState(states[(int)StandStates.GlobalStates]);
    }

    public void SetStand(StandType newType){
        standType = newType;
        int index = (int)standType;
        states[(int)StandStates.Attack] = null;
        states[(int)StandStates.Skill1] = null;
        states[(int)StandStates.Skill2] = null;
        switch(index){
            case 0 : 
                states[(int)StandStates.Attack] = new StarPlatina.Attack();
                states[(int)StandStates.Skill1] = new StarPlatina.Skill1();
                states[(int)StandStates.Skill2] = new StarPlatina.Skill2();
                break;
            default : 
                break;
        }
    }

    void Update(){
        stateMachine.Update();
    }

    void FixedUpdate() {
        stateMachine.FixedUpdate();
    }

    public void ChangeState(StandStates newState){
        CurState = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    public void RevertToPreviousState(){
        stateMachine.RevertToPreviousState();
    }

    public override bool HandleMessage(Telegram telegram){
        return false;
    }
}