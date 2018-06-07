using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サンプル用のnamespaceを使用してみる
namespace StateMachine
{
    public enum status
    {
        Stay,
        Pursuit,
        Attack,
    }

    //StatefulObjectBase(ステートマシン用に作成した基底クラス)を継承
    public class Enemy : StatefulObjectBase<Enemy, status>//<>
    {
        //個体で違う数値・参照が必要な変数はここに書く
        Transform player;
        [SerializeField]
        bool IsFly;

        [SerializeField] //巡回地点
        Transform[] StayPoint;

        [SerializeField]
        float speed = 4f;                  //移動

        float moveVec;                     //移動方向

        [SerializeField]
        float WalkStopTime;               //巡回時の待機時間
        [SerializeField]
        float TargetLostTime;             //見失った時に立ち止まる時間

        bool P_Targetlostflg;               //プレーヤーを見失った時に発生

        [SerializeField]
        float attackInterval = 2f;        //攻撃頻度

        [SerializeField]
        Get_Pursuit pursuit;              //追跡に移行する範囲
        [SerializeField]
        Get_Attack attack;                //攻撃判定系統

        [SerializeField]
        float margin = 4f;               //オブジェクトの大きさ

        private float changeTargetDistance = 1f;//接触地点への判定距離

        void Start()
        {
            // 始めにプレイヤーの位置を取得できるようにする
            player = GameObject.FindWithTag("Player").transform;

            // ステートマシンの初期設定
            stateList.Add(new StateWalk(this));
            stateList.Add(new StatePursuit(this));
            stateList.Add(new StateAttack(this));

            stateMachine = new StateMachine<Enemy>();

            ChangeState(status.Stay);

        }
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
                attack.AttackStopflg = true;
        }


        /// <summary>
        /// 待機or徘徊
        /// </summary>
        class StateWalk : State<Enemy>
        {
            //引数はここで指定　
            //baseは何もしない　"owner"でEnemyで使用している変数を使用可能にする
            public StateWalk(Enemy owner) : base(owner) {}

            //巡回地点の変更フラグ
            int PointCount = 0 ;
            float StayTime = 0 ;
            //巡回地点＠Enemy owner.変数名 で呼び出し
            //public Transform[] StayPoint;
            //目標地点
            Vector3 targetPoint;
            Vector3 diff;

            public override void Enter()
            {
                owner.moveVec = 1;      //向きを初期化
                StayTime = 0;           //待機時間を初期化
                //初期呼び出し時、最初の巡回地点を設定する
                if (targetPoint == Vector3.zero)
                {
                    //最後の地点を入力して最初の地点を取得出来るようにする
                    PointCount = owner.StayPoint.Length;
                    //地点を取得して目標地点に設定
                    Change_Point();
                }
                else if (owner.P_Targetlostflg || !owner.IsFly)//プレーヤーを見失ったらその地点に行く*歩行型のみ
                {
                    targetPoint = owner.player.gameObject.transform.position;
                }
                else
                {
                    ////地点を取得して目標地点に設定
                    Change_Point();
                }
            }

            public override void Execute()//Update処理
            {
                //追跡範囲に入ったら、追跡ステートに遷移
                if (owner.pursuit.PursuitFlg)
                {
                    owner.ChangeState(status.Pursuit);
                }
                // 目標地点との距離が小さければ、
                float sqrDistanceToTarget = Vector3.SqrMagnitude(owner.transform.position - targetPoint);
                if (sqrDistanceToTarget < owner.changeTargetDistance)
                {
                    if (owner.P_Targetlostflg )
                    {
                        if (StayTime < owner.TargetLostTime)
                        {
                            StayTime += Time.deltaTime;
                        }
                        else
                        {
                            owner.P_Targetlostflg = false;
                        }
                    }
                    //その場で一定時間待機
                    else if (StayTime < owner.WalkStopTime)
                    {
                        StayTime += Time.deltaTime;
                    }
                    //目標地点を変更してカウントリセット
                    else
                    {
                        Change_Point();
                        StayTime = 0;
                    }
                }
                else
                {
                    diff = (targetPoint - owner.transform.position);

                    //移動方向に応じて向きの切り替え
                    if (diff.x > 0)
                    {
                        owner.moveVec = 1;
                        //目標の方向を向いて進む
                        owner.transform.rotation = Quaternion.FromToRotation(Vector3.right, diff);
                        owner.transform.Translate(Vector3.right * owner.speed * Time.deltaTime);

                    }
                    else if (diff.x < 0)
                    {
                        owner.moveVec = -1;
                        //
                        owner.transform.rotation = Quaternion.FromToRotation(Vector3.left, diff);
                        owner.transform.Translate(Vector3.left * owner.speed * Time.deltaTime);

                    }
                    owner.transform.localScale = new Vector3(owner.moveVec, 1f, 1f);

                }

                // 目標地点の方向を向く
                //Quaternion targetRotation = Quaternion.LookRotation(targetPoint - owner.transform.position);
                //owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotation, Time.deltaTime * owner.rotationSmooth);
                //// 前方に進む
                //owner.transform.Translate(Vector3.forward * owner.speed * Time.deltaTime);



            }

            public override void Exit() {}

            //次の目標地点を設定する
            void Change_Point()
            {
                //次の地点を取得
                PointCount++ ;
                //一巡したら最初の地点を取得
                if (owner.StayPoint.Length <= PointCount)
                {
                    PointCount = 0;
                }
                //目標地点を設定する
                targetPoint = owner.StayPoint[PointCount].position;
            }
        }

        /// <summary>
        /// 追跡
        /// </summary>
        class StatePursuit : State<Enemy>
        {
            public StatePursuit(Enemy owner) : base(owner) { }


            public override void Enter() { }

            public override void Execute()
            {
                //float sqrDistanceToPlayer = Vector3.SqrMagnitude(owner.transform.position - owner.player.position);
                // 攻撃範囲に入ったら、攻撃ステートに遷移
                if (owner.attack.AttackFlg)
                {
                    owner.ChangeState(status.Attack);
                }

                // 捕捉エリアから出ると、徘徊ステートに遷移
                if (!owner.pursuit.PursuitFlg)
                {
                    owner.ChangeState(status.Stay);
                }
                //追跡
                Pursuit();
                //owner.transform.Translate(Vector3.right * owner.speed * Time.deltaTime);
            }


            public override void Exit()
            {
                if (!owner.IsFly)
                    owner.P_Targetlostflg = true;
            }

            //追跡する
            void Pursuit()
            {
                //距離と位置を取得
                Vector3 diff = (owner.player.gameObject.transform.position - owner.transform.position);
                
                //プレーヤーの位置に合わせて方向を調整する
                //右向き
                if (diff.x > 0)
                {
                    //オブジェクトの向きを合わせる
                    owner.moveVec = 1;
                    owner.transform.localScale = new Vector3(owner.moveVec, 1f, 1f);
                    //プレーヤーの方向を向いて進む
                    owner.transform.localRotation = Quaternion.FromToRotation(Vector3.right, diff);
                    owner.transform.Translate(Vector3.right * owner.speed * Time.deltaTime);
                }
                //左向き
                else if (diff.x < 0)
                {
                    owner.moveVec = -1;
                    owner.transform.localScale = new Vector3(owner.moveVec, 1f, 1f);

                    owner.transform.localRotation = Quaternion.FromToRotation(Vector3.left, diff);
                    owner.transform.Translate(Vector3.left * owner.speed * Time.deltaTime);
                }

            }
        }

        /// <summary>
        /// 攻撃
        /// </summary>
        class StateAttack : State<Enemy>
        {
            public StateAttack(Enemy owner) : base(owner) { }

            float LastAttackTime; //最後に攻撃した時間
            float Leave_Dis;      //距離を取る範囲

            StateWalk walk;

            Vector2 DistanceToPlayer;//プレーヤーの距離            
            public override void Enter()
            {
                Leave_Dis = owner.margin;
            }

            public override void Execute()
            {
                //プレーヤーとの距離・方向を取得
                DistanceToPlayer = owner.player.gameObject.transform.position - owner.transform.position;
                
                //数値がマイナスにならないようにすることで、判定を楽に出来るようにする。
                if (DistanceToPlayer.x < 0)
                    DistanceToPlayer.x = -DistanceToPlayer.x;
                if (DistanceToPlayer.y < 0)
                    DistanceToPlayer.y = -DistanceToPlayer.y;

                //攻撃範囲から離れたら追跡ステートに遷移
                if (!owner.attack.AttackFlg)
                {
                    owner.ChangeState(status.Pursuit);
                }
                //連続で攻撃をしないように待機時間をかける
                //一定間隔で攻撃判定を出す
                if (Time.time > owner.attackInterval + LastAttackTime && !owner.attack.AttackStopflg)
                {
                    //攻撃判定を出す時間を更新
                    LastAttackTime = Time.time;
                    //攻撃判定を出す＠歩行型
                    if (!owner.IsFly)
                    {
                        owner.attack.GetAttack = true;
                        owner.attack.AttackStopflg = true;
                    }
                }
                //攻撃待機中
                //歩行型
                else if (!owner.IsFly)
                {
                    //離れていたら一定の距離まで接近する
                    if (DistanceToPlayer.x > owner.margin * 3 /* || DistanceToPlayer.y > owner.margin * 3*/)
                    {
                        owner.attack.GetAttack = false;
                        Pursuit();
                    }
                    //歩行型のみ　近づきすぎるとゆっくり距離とる
                    else if (DistanceToPlayer.x < Leave_Dis /* || DistanceToPlayer.y < Leave_Dis*/)
                    {
                        owner.attack.GetAttack = false;
                        Leave();
                    }
                }
                //飛行型
                else if (owner.IsFly)
                {
                    if (owner.attack.AttackStopflg)
                    {
                        Leave();
                    }
                    else
                    {
                        Pursuit();
                    }
                }
            }

            public override void Exit()
            {
                if (!owner.IsFly)
                    owner.P_Targetlostflg = true;
            }

            //接近処理
            void Pursuit()
            {
                //距離と位置を取得
                DistanceToPlayer = (owner.player.gameObject.transform.position - owner.transform.position);

                //プレーヤーの位置に合わせて方向を調整する
                if (DistanceToPlayer.x > 0)
                {
                    owner.moveVec = 1;
                    owner.transform.localScale = new Vector3(owner.moveVec, 1f, 1f);

                    owner.transform.localRotation = Quaternion.FromToRotation(Vector3.right,DistanceToPlayer);
                    owner.transform.Translate(Vector3.right * owner.speed * Time.deltaTime);
                }
                else if (DistanceToPlayer.x < 0)
                {
                    owner.moveVec = -1;
                    owner.transform.localScale = new Vector3(owner.moveVec, 1f, 1f);

                    owner.transform.localRotation = Quaternion.FromToRotation(Vector3.left, DistanceToPlayer);
                    owner.transform.Translate(Vector3.left * owner.speed * Time.deltaTime);
                }

            }
            //後退処理
            void Leave()
            {
                //距離と位置を取得
                if(!owner.IsFly)
                DistanceToPlayer = (owner.player.gameObject.transform.position - owner.transform.position);

                if (DistanceToPlayer.x > 0)
                {
                    owner.moveVec = 1;
                    owner.transform.localScale = new Vector3(owner.moveVec, 1f, 1f);

                    if (!owner.IsFly)
                    {
                        owner.transform.localRotation = Quaternion.FromToRotation(Vector3.right, DistanceToPlayer);
                        owner.transform.Translate(Vector3.left * owner.speed / 1.5f * Time.deltaTime);
                    }
                    else
                    {
                        owner.transform.localRotation = Quaternion.FromToRotation(Vector3.right, DistanceToPlayer);
                        owner.transform.Translate(Vector3.right * owner.speed * Time.deltaTime);
                    }
                }
                else if (DistanceToPlayer.x < 0)
                {
                    owner.moveVec = -1;
                    owner.transform.localScale = new Vector3(owner.moveVec, 1f, 1f);

                    if (!owner.IsFly)
                    {
                        owner.transform.localRotation = Quaternion.FromToRotation(Vector3.left, DistanceToPlayer);
                        owner.transform.Translate(Vector3.right * owner.speed / 1.5f * Time.deltaTime);
                    }
                    else
                    {
                        owner.transform.localRotation = Quaternion.FromToRotation(Vector3.left, DistanceToPlayer);
                        owner.transform.Translate(Vector3.left * owner.speed * Time.deltaTime);
                    }
                }

            }
        }

    }
}

