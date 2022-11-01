using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates{ Idle = 0, Run, Jump, Attack, Skill1, Skill2, OnFall,GlobalState}
public class Player : BaseStateMech
{
    #region Config
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpPower;
    #endregion
    #region Reference
    public Rigidbody2D Rigid;
    public SpriteRenderer Spr;
    public Animator Ani;
    [SerializeField] Vector2 spd;
    #endregion

    #region Data
    public float SPEED{
        get => speed;
    }
    public float MAXSPEED{
        get => maxSpeed;
    }
    public float JUMPPOWER{
        get => jumpPower;
    }
    #endregion

    #region StateMachine
    State<Player>[] states;
    StateMachine<Player> stateMachine;
    public PlayerStates CurState{ private set; get;}
    #endregion

    void Awake(){
        Setup();
    }

    public override void Setup(){
        base.Setup();
        gameObject.name = $"Player_{ID:D8}";

        Rigid = GetComponent<Rigidbody2D>();
        Spr = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();

        states = new State<Player>[8];
        states[(int)PlayerStates.Idle] = new PlayerOwnStates.Idle();
        states[(int)PlayerStates.Run] = new PlayerOwnStates.Run();
        states[(int)PlayerStates.Jump] = new PlayerOwnStates.Jump();
        states[(int)PlayerStates.OnFall] = new PlayerOwnStates.OnFall();
        states[(int)PlayerStates.GlobalState] = new PlayerOwnStates.GlobalState();

        stateMachine = new StateMachine<Player>();
        stateMachine.Setup(this,states[(int)PlayerStates.Idle]);
        stateMachine.SetGlobalState(states[(int)PlayerStates.GlobalState]);
    }

    void Update(){
        spd = Rigid.velocity;
        stateMachine.Update();
    }

    void FixedUpdate() {
        stateMachine.FixedUpdate();
    }

    public void ChangeState(PlayerStates newState){
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
