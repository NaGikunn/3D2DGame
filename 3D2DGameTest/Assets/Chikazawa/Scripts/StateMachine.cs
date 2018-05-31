namespace StateMachine
{
    /// <summary>
    /// 
    /// 有限状態機械（ステートマシン）
    ///
    /// </summary>
    public class StateMachine<T>
    {
        //現在ステートの状態
        private State<T> currentState;
    
        //状態を初期化
        public StateMachine()
        {
            currentState = null;
        }
    
        //現在の状態を確認
        public State<T> CurrentState
        {
            get { return currentState; }
        }
    
        //ステートの状態を変える
        public void ChangeState(State<T> state)
        {
            //ステートから抜ける処理を行う
            if (currentState != null)
            {
                currentState.Exit();
            }
            //新しいステートに切り替えてEnterを呼ぶ
            currentState = state;
            currentState.Enter();
        }
    
        //毎フレーム行うステートの処理
        public void Update()
        {
            //状態が変化するまで処理を行う
            if (currentState != null)
            {
                currentState.Execute();
            }
        }
    }
}