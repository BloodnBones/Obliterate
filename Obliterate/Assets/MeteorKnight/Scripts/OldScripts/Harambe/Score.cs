using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{

    GameObject ScoreText;
    public int NumberScore;
    public int HP = 3;
    static public int Score_Scalar;
    static public bool Dead = false;
    // Use this for initialization
    void Start()
    {
        Dead = false;
        ScoreText = GameObject.Find("Score");
        NumberScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.GetComponent<TextMesh>().text = "Score : " + NumberScore.ToString();
        if (HP == 0)
        {
            Dead = true;

        }
    }

    void OnTriggerEnter(Collider other)
    {
        NumberScore += 1000 * Score_Scalar;
        Destroy(other.gameObject);
        HP -= 1;
        Debug.Log(NumberScore.ToString());
    }
}
