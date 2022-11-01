using System;
using UnityEngine;

public class BulletPlayer : BulletBase{

    public Player player;
    
    SpriteRenderer spr;
    BulletEnemy[] ExceptionBullet;

    public override void Enabled(){
        spr = GetComponent<SpriteRenderer>();
        spr.flipX = player.Spr.flipX;
        int dir = spr.flipX ? -1 : 1;
        SPEED *= dir;
        ExceptionBullet = new BulletEnemy[3];
    }
    
    public override void TriggerEnter2D(Collider2D other){
         //탄환끼리 충돌
        if(other.gameObject.layer == LayerMask.NameToLayer("BulletEnemy")){
            //상대 정보 복사
            BulletEnemy info = other.GetComponent<BulletEnemy>();
            for(int i = 0; i > 3; i++){
                if(ExceptionBullet[i] == null){
                    ExceptionBullet[i] = info;
                    break;
                }
                else if(ExceptionBullet[i] == info){
                    break;
                }
                if(i == 3){
                    Disable();
                    return;
                }   
            }
            //탄환끼리 대미지 처리
            info.BulletDamage(DAMAGE);
            BulletDamage(info.DAMAGE);    
        }
    }
    public override void Disabled(){
        Array.Clear(ExceptionBullet,0,3);
    }
}