using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher{

    static readonly MessageDispatcher instance = new MessageDispatcher();
    public static MessageDispatcher Instance => instance;

    private SortedDictionary<float, Telegram> prioritySD;

    public void Setup(){
        prioritySD = new SortedDictionary<float, Telegram>();
    }

    public void DispatchMessage(float delayTime, string senderName, string receiverName, Message message){
        BaseStateMech receiver = EntityDatabase.Instance.GetEntityFromID(receiverName);
        if(receiver == null){
            Debug.Log($"<color=red>There is no Receiver with ID of <b><i>{receiverName}</b></i> found</color>");
            return;
        }
        Telegram telegram = new Telegram();
        telegram.SetTelegram(0,senderName,receiverName,message);
        if(delayTime <= 0){
            Discharge(receiver,telegram);
        }
        else{
            telegram.dispatchTime = Time.time + delayTime;
            prioritySD.Add(telegram.dispatchTime, telegram);
        }
    }

    void Discharge(BaseStateMech receiver, Telegram telegram){
        receiver.HandleMessage(telegram);

    }

    public void DispatchDelayedMessage(){
        foreach(KeyValuePair<float, Telegram> entity in prioritySD){
            if(entity.Key <= Time.time){
                BaseStateMech receiver = EntityDatabase.Instance.GetEntityFromID(entity.Value.receiver);
                Discharge(receiver,entity.Value);
                prioritySD.Remove(entity.Key);

                return;
            }
        }
    }
}