using UnityEngine;

public abstract class BaseStateMech : MonoBehaviour
{
    private static int m_NextValidId = 0;
    private int id;
    public int ID{
        set{
            id = value;
            m_NextValidId++;
        }
        get => id;
    }

    string entityName;

    public string EntityName => entityName;

    
    public virtual void Setup(){
        ID = m_NextValidId;
    }
    public abstract bool HandleMessage(Telegram telegram);
}