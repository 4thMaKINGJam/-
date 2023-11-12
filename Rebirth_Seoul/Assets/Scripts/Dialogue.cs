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
        Talk(talkID, talkIndex); // ���� ��� ����
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
            talkIndex = 0; // �ʱ�ȭ
        }
    }

    void GenerateData()
    {
        talkData.Add(0, new string[] {"�ȳ��ϽŰ�, ���� ����!\n����� �ƽ��Ե� ���� Ÿ���� ���߾�.",
            "�����̶�� ��� ȯ���� �� �־�������,\n������ ���� �α� �������� ������\nȯ�� ��ȸ�� ��ƾ� ��.",
            "����� �� �߿����� ���빮 ������ �����Ǿ���.",
            "���� ������ ��� �������� ������ ��ֹ��� ����\n�Ƿɵ��� ������ �հ�, �׵��� ���� �ı��ϸ鼭\n������ ���� �����Ѵٸ� �����̾�!",
            "������ �ı��ϸ� ������ ���� �Ǿ� �Ƿɵ���\nóġ���ֱ⵵ �ϴ� ������ Ż���� ��\n�� ���� Ȱ���غ��� �͵� ���� �ž�.",
            "�׷� ����� ��ٳ�!", "" });
    }

    void Talk(int talk_ID, int talk_Index)
    {
        string dialogue_data = talkData[talkID][talkIndex];
        Dialogue_text.text = dialogue_data;
    }
}
