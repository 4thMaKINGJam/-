using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public int talkID;
    public TextMeshProUGUI Dialogue_text;
    int talkIndex;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
        Talk(talkID, talkIndex); // 최초 대사 실행
        talkIndex++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    public void NextDialogue()
    {
        Talk(talkID, talkIndex);
        talkIndex++;
        if (talkIndex == talkData[talkID].Length)
        {
            SceneManager.LoadScene("MainScene");
            talkIndex = 0; // 초기화
        }
    }

    void GenerateData()
    {
        talkData.Add(0, new string[] {"안녕하신가, 신입 차사!\n당신은 아쉽게도 운을 타고나지 못했어.",
            "이전이라면 즉시 환생할 수 있었겠지만,\n지금은 저승 인구 과잉으로 스스로\n환생 기회를 잡아야 해.",
            "당신은 그 중에서도 서대문 던전에 배정되었어.",
            "악의 구슬이 쏘는 레이저와 던전의 장애물을 피해\n악령들의 공격을 뚫고, 그들의 집을 파괴하면서\n던전의 끝에 도달한다면 성공이야!",
            "구슬을 파괴하면 차사의 편이 되어 악령들을\n처치해주기도 하니 던전을 탈출할 때\n이 점을 활용해보는 것도 좋을 거야.",
            "그럼 행운을 빈다네!", "" });
    }

    void Talk(int talk_ID, int talk_Index)
    {
        string dialogue_data = talkData[talkID][talkIndex];
        Dialogue_text.text = dialogue_data;
    }
}
