using System.Collections;
using UnityEngine;
namespace PlayerOwnStates{
    public class Idle : State<Player>{
        public override void Enter(Player entity){
            
        }
        public override void Exit(Player entity){
            
        }
        public override void Update(Player entity){
            if(PlayerInputManager.Instance.HDown){
                entity.ChangeState(PlayerStates.Run);
            }
            if(PlayerInputManager.Instance.JDown){
                entity.ChangeState(PlayerStates.Jump);
            }
        }
        
        public override void FixedUpdate(Player entity){
            
        }
    
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }

    public class Run : State<Player>{
        Idle idle = new Idle();
        public override void Enter(Player entity){
            entity.Ani.SetBool("isRun",true);
        }
        public override void Exit(Player entity){
            entity.Ani.SetBool("isRun",false);
        }
        public override void Update(Player entity){
            
            if(!PlayerInputManager.Instance.HDown)
                entity.ChangeState(PlayerStates.Idle);
            if(PlayerInputManager.Instance.JDown){
                entity.ChangeState(PlayerStates.Jump);
            }
        }
        public override void FixedUpdate(Player entity){
            
        }
        
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }

    public class Jump : State<Player>{
        Idle idle = new Idle();
        Run run = new Run();
        public override void Enter(Player entity){
            entity.Ani.SetTrigger("doJump");
            entity.Rigid.AddForce(Vector2.up * entity.JUMPPOWER,ForceMode2D.Impulse);
        }
        public override void Exit(Player entity){
            entity.Rigid.velocity = Vector2.zero;
        }
        public override void Update(Player entity){
            
        }
        public override void FixedUpdate(Player entity){

        }
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }

    public class Attack : State<Player>{
        public override void Enter(Player entity){
            
        }
        public override void Exit(Player entity){
            
        }
        public override void Update(Player entity){
            
        }
        public override void FixedUpdate(Player entity){
            
        }
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }

    public class Skill1 : State<Player>{
        public override void Enter(Player entity){
            
        }
        public override void Exit(Player entity){
            
        }
        public override void Update(Player entity){
            
        }
        public override void FixedUpdate(Player entity){
            
        }
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }

    public class Skill2 : State<Player>{
        public override void Enter(Player entity){
            
        }
        public override void Exit(Player entity){
            
        }
        public override void Update(Player entity){
            
        }
        public override void FixedUpdate(Player entity){
            
        }
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }

    public class OnFall : State<Player>{
        public override void Enter(Player entity){
            entity.Ani.SetBool("isFall",true);
        }
        public override void Exit(Player entity){
            entity.Ani.SetBool("isFall",false);
        }
        public override void Update(Player entity){
            
        }
        public override void FixedUpdate(Player entity){
            Rigidbody2D rigid = entity.Rigid;
            //플레이어 레이어 제외하고
            int layerPlayer = 1 << LayerMask.NameToLayer("Player");
            layerPlayer = ~layerPlayer;
            //바닥과 낭떠러지 체크
            int layerFloor = LayerMask.NameToLayer("Floor");
            if(entity.Rigid.velocity.y < 0){
                Debug.DrawRay(rigid.position,
                Vector3.down,Color.green,1);
                RaycastHit2D rayCheck =
                Physics2D.Raycast(
                    rigid.position,
                    Vector3.down,
                    1,layerPlayer);
                //맞았을 때
                if(rayCheck.collider != null && rayCheck.distance <= 0.5){
                    //바닥이라면 Idle 실행
                    if(rayCheck.transform.gameObject.layer == layerFloor){
                        entity.ChangeState(PlayerStates.Idle);
                    }    
                } 
            }
        }
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }

    public class GlobalState : State<Player>{
        public override void Enter(Player entity){
            
        }
        public override void Exit(Player entity){
            
        }
        public override void Update(Player entity){
            if(PlayerInputManager.Instance.HUp){
                entity.Rigid.velocity = new Vector2(entity.Rigid.velocity.normalized.x * 0.1f, entity.Rigid.velocity.y);
            }
            Flip(entity);
        }
        public void Flip(Player entity){
            if(PlayerInputManager.Instance.HDown){
                float hKey = PlayerInputManager.Instance.H;
                if(hKey != 0){
                    bool flip = false;
                    if(hKey < 0)
                        flip = true;
                    else
                        flip = false;
                    entity.Spr.flipX = flip;
                }
            }
        }
        public override void FixedUpdate(Player entity){
            Move(entity);
            if(entity.CurState == PlayerStates.OnFall)
                return;
            if(entity.Rigid.velocity.y < 0){
                int layerPlayer = 1 << LayerMask.NameToLayer("Player");
                layerPlayer = ~layerPlayer;
                //바닥과 낭떠러지 체크
                int layerFloor = LayerMask.NameToLayer("Floor");
                Debug.DrawRay(entity.Rigid.position,
                Vector3.down,Color.green,1);
                RaycastHit2D rayCheck =
                Physics2D.Raycast(
                    entity.Rigid.position,
                    Vector3.down,
                    1f,layerPlayer);
                if(rayCheck.collider != null){
                    return;  
                }
                else
                    entity.ChangeState(PlayerStates.OnFall);
            }
        }
        public void Move(Player entity){
            Rigidbody2D rigid = entity.Rigid;
            //속도 설정
            Vector2 spd = new Vector2(PlayerInputManager.Instance.H,0).normalized;
            
            //해당 속도로 움직이게 하고
            rigid.AddForce(Vector2.right * spd * entity.SPEED * entity.Rigid.gravityScale * Time.deltaTime, ForceMode2D.Impulse);
            //만약 최고 속도를 넘기면 제약함
            if(rigid.velocity.x > entity.MAXSPEED * Time.deltaTime){
                rigid.velocity = new Vector2(entity.MAXSPEED * Time.deltaTime, rigid.velocity.y);
            }
            else if(rigid.velocity.x < entity.MAXSPEED * (-1) * Time.deltaTime){
                rigid.velocity = new Vector2(entity.MAXSPEED * Time.deltaTime * (-1) , rigid.velocity.y);
            }
        }
        
        public override bool OnMessage(Player entity, Telegram telegram){
            return false;
        }
    }
}