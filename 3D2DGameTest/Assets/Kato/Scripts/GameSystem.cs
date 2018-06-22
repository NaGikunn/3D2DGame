using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameSystem : MonoBehaviour
{
    
        public void OnMouseDown()
    {

        StartCoroutine("Sample");　//コルーチンを呼び出す

    }

    // コルーチン
    private IEnumerator Sample()
    {
        yield return new WaitForSeconds(5.0f);　 //5秒たってから
        Application.LoadLevel("Game");　　　　　//画面遷移
    }
}


