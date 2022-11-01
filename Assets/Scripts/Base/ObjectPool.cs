using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool :MonoBehaviour
{
    [SerializeField] GameObject[] PlayerBulletPrefabs;
    private class PoolItem{
        public bool isActive;
        public GameObject gameObject;
    }

    [SerializeField] int increaseCount = 3;  //부족할 때 추가 생성되는 개수
    [SerializeField] int maxCount;   // 현재 리스트에 등록된 오브젝트 수
    [SerializeField] int activeCount;    // 현재 사용중인 오브젝트 수

    private GameObject poolObject;
    private List<PoolItem> playerBullets;

    public ObjectPool(GameObject poolObject){
        maxCount        = 0;
        activeCount     = 0;
        this.poolObject = poolObject;
        playerBullets   = new List<PoolItem>();

        for(int i = 0; i > PlayerBulletPrefabs.Length; i++){
            InstantiatePlayerBullets(i);
        }
    }

    public void InstantiatePlayerBullets(int index){
        maxCount += increaseCount;

        for(int i = 0; i < increaseCount; ++i){
            PoolItem poolObject = new PoolItem();

            poolObject.isActive = false;
            poolObject.gameObject = GameObject.Instantiate(PlayerBulletPrefabs[index]);
            poolObject.gameObject.SetActive(false);

            playerBullets.Add(poolObject);
        }

    }

    public void DestroyPlayerBullets(){
        if(playerBullets == null) return;
        int count = playerBullets.Count;
        for(int i = 0; i < count ; ++ i){
            GameObject.Destroy(playerBullets[i].gameObject);
        }

        playerBullets.Clear();
    }

    public GameObject ActivatePlayerBullets(int index){
        if(playerBullets == null) return null;
        if(maxCount == activeCount){
            InstantiatePlayerBullets(index);
        }

        int count = playerBullets.Count;
        for(int i = 0; i < count; ++i){
            PoolItem poolObject = playerBullets[i];
            if(poolObject.gameObject == PlayerBulletPrefabs[count] 
            && poolObject.isActive == false){
                
                activeCount ++;
                poolObject.isActive = true;
                poolObject.gameObject.SetActive(true);

                return poolObject.gameObject;
            }
        }
        return null;
    }
    public void DeactivatePlayerBullets(GameObject removeObject){
        if(playerBullets == null || removeObject == null) return;
        int count = playerBullets.Count;

        for(int i = 0; i < count; ++i){
            PoolItem poolObject = playerBullets[i];
            if(poolObject.gameObject == removeObject){
                activeCount --;
                poolObject.isActive = false;
                poolObject.gameObject.SetActive(false);

                return;
            }
        }
    }
    public void DeactivateAllPlayerBullets(){
        if(playerBullets == null) return;
        int count = playerBullets.Count;

        for(int i = 0; i < count; ++i){
            PoolItem poolObject = playerBullets[i];
            if(poolObject.gameObject != null && poolObject.isActive == true){
                poolObject.isActive = false;
                poolObject.gameObject.SetActive(false);
            }
        }
        activeCount = 0;
    }
}
