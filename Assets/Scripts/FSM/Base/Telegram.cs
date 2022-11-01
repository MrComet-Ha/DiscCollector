public struct Telegram{
    public float dispatchTime;
    public string sender;
    public string receiver;
    public Message message;

    public void SetTelegram(float time, string sender, string receiver, Message message){
        this.dispatchTime = time;
        this.sender = sender;
        this.receiver = receiver;
        this.message = message;
    }
}