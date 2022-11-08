using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] Transform leftSpawnTransform = null;
    [SerializeField] Transform rightSpawnTransform = null;

    GameObject leftCharacter = null;
    GameObject rightCharacter = null;
    [SerializeField] List<GameObject> charPrefab = new List<GameObject>();

    private void Awake()
    {

        leftCharacter = charPrefab[GameManager.Instance.LeftCharIndex];
        rightCharacter = charPrefab[GameManager.Instance.RightCharIndex];
    }

    private void Start()
    {
        if (GameManager.Instance.RightPlayer == "Player1")
        {
            GameObject instObj = Instantiate(rightCharacter, rightSpawnTransform.position, Quaternion.identity);
            instObj.GetComponent<Player>().selectPlayer = 1;
            instObj.tag = "1Player";
        }
        else if (GameManager.Instance.RightPlayer == "Player2")
        {
            GameObject instObj = Instantiate(rightCharacter, rightSpawnTransform.position, Quaternion.identity);
            instObj.GetComponent<Player>().selectPlayer = 2;
            instObj.tag = "2Player";
        }

        if (GameManager.Instance.LeftPlayer == "Player1")
        {
            GameObject instObj = Instantiate(leftCharacter, leftSpawnTransform.position, Quaternion.identity);
            instObj.GetComponent<Player>().selectPlayer = 1;
            instObj.tag = "1Player";
        }
        else if (GameManager.Instance.LeftPlayer == "Player2")
        {
            GameObject instObj = Instantiate(leftCharacter, leftSpawnTransform.position, Quaternion.identity);
            instObj.GetComponent<Player>().selectPlayer = 2;
            instObj.tag = "2Player";
        }
    }
}
