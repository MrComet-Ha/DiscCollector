using UnityEngine;

public class Enemy : BaseStateMech{
    public override void Setup(){
        base.Setup();
    }

    public override bool HandleMessage(Telegram telegram){
        return false;
    }
}