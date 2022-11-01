public abstract class State<T> where T : BaseStateMech{
    //모든 엔티티의 상태는 다음의 3가지를 가짐
    //1. 상태에 돌입했을 때 1회 실행
    public abstract void Enter(T entity);
    //2. 상태에서 나왔을 때 1회 실행
    public abstract void Exit(T entity);
    //3. 상태일 때 Update
    public abstract void Update(T entity);
    //4. 상태일 때 FixedUpdate
    public abstract void FixedUpdate(T entity);

    public abstract bool OnMessage(T entity, Telegram telegram);
}