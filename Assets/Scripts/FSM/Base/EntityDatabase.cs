using System.Collections.Generic;

public class EntityDatabase{

    static readonly EntityDatabase instance = new EntityDatabase();
    public static EntityDatabase Instance => instance;

    Dictionary<string, BaseStateMech> entityDictionary;

    public void Setup(){
        entityDictionary = new Dictionary<string, BaseStateMech>();
    }

    public void RegisterEntity(BaseStateMech newEntity){
        entityDictionary.Add(newEntity.EntityName, newEntity);
    }

    public BaseStateMech GetEntityFromID(string entityName){
        foreach(KeyValuePair<string, BaseStateMech> entity in entityDictionary){
            if(entity.Key == entityName){
                return entity.Value;
            }
        }
        return null;
    }

    public void RemoveEntity(BaseStateMech removeEntity){
        entityDictionary.Remove(removeEntity.EntityName);
    }
}