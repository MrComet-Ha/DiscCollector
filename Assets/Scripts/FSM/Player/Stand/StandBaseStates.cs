using System.Collections;
using UnityEngine;
namespace StandBaseStates{
    public class Idle : State<Stand>{
        public override void Enter(Stand entity){

        }
        public override void Exit(Stand entity){
            
        }
        public override void Update(Stand entity){
            if(Input.GetKeyDown(PlayerInputManager.Instance.SwapKey))
                entity.ChangeState(StandStates.Swap);
            if(PlayerInputManager.Instance.ADown)
                entity.ChangeState(StandStates.Attack);
        }
        public override void FixedUpdate(Stand entity){
            
        }
        public override bool OnMessage(Stand entity, Telegram telegram){
            return false;
        }
    }
    public class Swap : State<Stand>{
        public override void Enter(Stand entity){
            if(entity.standType == StandType.StarPlatina)
                entity.SetStand(StandType.SPTW);
            else
                entity.SetStand(StandType.StarPlatina);
            int standIndex = (int)entity.standType;
            entity.Spr.sprite = entity.Sprites[standIndex];
            entity.ChangeState(StandStates.Idle);
        }
        public override void Exit(Stand entity){
            
        }
        public override void Update(Stand entity){
            
        }
        public override void FixedUpdate(Stand entity){
            
        }
        public override bool OnMessage(Stand entity, Telegram telegram){
            return false;
        }
    }
    public class GlobalStates : State<Stand>{
        public override void Enter(Stand entity){

        }
        public override void Exit(Stand entity){
            
        }
        public override void Update(Stand entity){
            Follow(entity);
        }
        void Follow(Stand entity){
            entity.Spr.flipX = entity.parent.Spr.flipX;
            float turn = entity.Spr.flipX ? -1 : 1;
            Vector3 trueOffset = new Vector3(entity.offset.x * turn, entity.offset.y, entity.offset.z);
            entity.transform.position = entity.parent.transform.position + trueOffset;
        }
        public override void FixedUpdate(Stand entity){
            
        }
        public override bool OnMessage(Stand entity, Telegram telegram){
            return false;
        }
    }
}
