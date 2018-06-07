using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StateMachine
{
    /// <summary>
    /// 
    /// ステートを持つオブジェクトの基底
    /// 
    /// </summary>
    //unityのベースクラスを継承している。  T=ステートのクラス  TEnum=ステートの状態を保存するEnum
    public abstract class StatefulObjectBase<T, TEnum> : MonoBehaviour
        where T : class where TEnum : System.IConvertible
    {
        //ステートのリスト
        protected List<State<T>> stateList = new List<State<T>>();
        //ステートマシン
        protected StateMachine<T> stateMachine;
        
        //ステートの状態を切り替える
        public virtual void ChangeState(TEnum state)
        {
            if (stateMachine == null)
            {
                return;
            }

            stateMachine.ChangeState(stateList[state.ToInt32(null)]);
        }

        //ステートの現在の状態を確認
        public virtual bool IsCurrentState(TEnum state)
        {
            if (stateMachine == null)
            {
                return false;
            }
            //変化があった場合、状態を更新
            return stateMachine.CurrentState == stateList[state.ToInt32(null)];
        }
        //ステートマシンの状態を監視する
        protected virtual void Update()
        {
            if (stateMachine != null)
            {
                stateMachine.Update();
            }
        }
    }
}
