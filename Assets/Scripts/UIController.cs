using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text life;
    [SerializeField] TMP_Text msg;
    [SerializeField] Image image;
    public GameObject enemy;
    public static UIController instance;

    private void Start()
    {
        instance = this;
        image.gameObject.SetActive(false);
        msg.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        life.text = enemy.GetComponent<EnemyAI>().health.ToString() ;
    }
    public void PlayerWin()
    {
        Color color = Color.green;
        color.a = 0.5f;
        image.gameObject.SetActive(true);
        image.color = color;
        msg.text = "El jugador gana";


    }
    public void AIWin()
    {
        Color color = Color.red;
        color.a = 0.5f;
        image.gameObject.SetActive(true);
        image.color = color;
        msg.text = "La IA gana";

    }
}
