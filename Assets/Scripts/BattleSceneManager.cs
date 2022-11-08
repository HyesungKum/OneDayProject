using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] Vector3 leftSpawnPostion = Vector3.zero;
    [SerializeField] Vector3 rightSpawnPostion = Vector3.zero;

    GameObject leftCharacter = null;
    GameObject rightCharacter = null;
    List<GameObject> charPrefab = new List<GameObject>();

    private void Awake()
    {
        leftCharacter = charPrefab[GameManager.Instance.leftCharIndex];
        rightCharacter = charPrefab[GameManager.Instance.rightCharIndex];
    }

    private void Start()
    {
        Instantiate(leftCharacter, leftSpawnPostion, Quaternion.identity);

        //controller apply
        leftCharacter.GetComponent<Player>().selectPlayer = (int)GameManager.Instance.LeftPlayer[6];

        Instantiate(rightCharacter, rightSpawnPostion, Quaternion.identity);

        rightCharacter.GetComponent<Player>().selectPlayer = (int)GameManager.Instance.RightPlayer[6];
    }
}
