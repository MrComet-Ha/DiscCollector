using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int damage;
    [SerializeField] float speed;

    public int HP;
    public int DAMAGE;
    public float SPEED;

    //탄환의 기본적인 정보를 선언
    //hp : 탄환의 체력, 탄환끼리 상쇄될 때 씀
    //damage : 탄환의 피해량
    //speed : 탄환의 속도
    //public으로 선언된 것들은 실제로 사용할 변수로, 에디터에서 설정한 초기값을 받아오고 변경되는 역할임

    ObjectPool objectPool;

    public void Setup(ObjectPool objectPool){
        this.objectPool = objectPool;
    }

    //활성화 됐을 때
    void OnEnable(){
        HP=hp;
        DAMAGE=damage;
        SPEED=speed;
        Enabled();
    }

    public abstract void Enabled();

    //매 프레임마다 실행
    void Update(){
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(SPEED,0,0);
        transform.position = curPos+nextPos;
    }

    //접촉시 코드
    void OnTriggerEnter2D(Collider2D other){
        TriggerEnter2D(other);
    }

    public abstract void TriggerEnter2D(Collider2D other);

    public void BulletDamage(int dmg){
        //탄환 체력 소모
        HP -= dmg;
        //탄환의 HP를 0 이하로는 내려가지 않게 함.
        HP = (int)Mathf.Max(0,HP);
        //만약 HP가 0이라면 탄환을 비활성화시킴
        if(HP == 0){
            Disable();
        }
    }

    void OnDisable(){
        Disabled();
    }

    public abstract void Disabled();



    public void Disable(){
        objectPool.DeactivatePlayerBullets(gameObject);
    }

}