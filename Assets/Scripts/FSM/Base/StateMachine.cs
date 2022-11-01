public class StateMachine<T> where T : BaseStateMech
{
    private T ownerEntity;
    private State<T> curState;
    private State<T> preState;
    private State<T> globalState;

    public void Setup(T owner, State<T> entryState){
        ownerEntity = owner;
        curState = null;
        preState = null;
        globalState = null;
        ChangeState(entryState);
    }

    public void Update(){
        if(globalState != null)
            globalState.Update(ownerEntity);
        if(curState != null)
            curState.Update(ownerEntity);
    }
    public void FixedUpdate(){
        if(globalState != null)
            globalState.FixedUpdate(ownerEntity);
        if(curState != null)
            curState.FixedUpdate(ownerEntity);
    }
    public void ChangeState(State<T> newState){
        if(newState == null)
            return;
        if(curState != null){
            preState = curState;
            curState.Exit(ownerEntity);
        }

        curState = newState;
        curState.Enter(ownerEntity);
    }

    public void SetGlobalState(State<T> newState){
        globalState = newState;
    }

    public void RevertToPreviousState(){
        ChangeState(preState);
    }
    public bool HandleMessage(Telegram telegram){
        if(globalState != null && globalState.OnMessage(ownerEntity, telegram))
            return true;
        if(curState != null && curState.OnMessage(ownerEntity,telegram))
            return true;
        return false;
    }
}