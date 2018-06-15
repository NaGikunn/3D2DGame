using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]

public class GameSystem : MonoBehaviour
{

    public void Game()
    {
        // 「GameScene」シーンに遷移する
        SceneManager.LoadScene("StageSelect");
    }
}
