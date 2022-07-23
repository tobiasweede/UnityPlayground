using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraceGame : MonoBehaviour
{
    public TraceSphere TracePrefab;
    private List<GameObject> sphereList = new();
    public GameObject AnswerPrefab;
    private List<GameObject> answerList = new();
    public int InstanceCount;
    public float highlightTargetDuration;
    public float playDuration = 10.0f;
    public float showResultDuration = 3.0f;
    private Vector3 areaSize;
    private int targetId;
    private Button startButton;
    private TextMeshProUGUI resultText;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        resultText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();
        resultText.gameObject.SetActive(false);
    }

    public void StartTraceGame()
    {
        startButton.interactable = false;
        foreach (TraceSphere unit in GetComponentsInChildren<TraceSphere>())
        {
            Destroy(unit.gameObject);
        }
        targetId = Random.Range(0, InstanceCount);
        areaSize = transform.GetComponent<MeshFilter>().mesh.bounds.size / 2;
        for (int i = 0; i < InstanceCount; ++i)
        {
            InstantiateGameSpheres(i);
        }
        StartCoroutine(EvaluateGame(playDuration));
    }

    void InstantiateGameSpheres(int id)
    {
        TraceSphere sphere = Instantiate(TracePrefab, transform);
        sphere.SetAreaSize(areaSize);
        sphere.Id = id;
        sphere.name = "Sphere " + id.ToString();
        sphereList.Add(sphere.gameObject);

        Vector3 position = new Vector3(
        Random.Range(-areaSize.x, areaSize.x),
        Random.Range(-areaSize.y, areaSize.y),
        Random.Range(-areaSize.z, areaSize.z));
        sphere.transform.localPosition = position;
        if (sphere.Id == targetId)
        {
            StartCoroutine(HighlightTarget(sphere, highlightTargetDuration));
        }
    }

    IEnumerator HighlightTarget(TraceSphere newUnit, float playDuration)
    {
        Color origColor = newUnit.GetComponent<MeshRenderer>().material.color;
        newUnit.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(playDuration);
        newUnit.GetComponent<MeshRenderer>().material.color = origColor;
    }
    IEnumerator EvaluateGame(float playDuration)
    {
        yield return new WaitForSeconds(playDuration);
        foreach (TraceSphere unit in GetComponentsInChildren<TraceSphere>())
        {
            unit.ToggleMove = false;
            var idText = unit.GetComponentInChildren<TextMeshPro>();
            idText.text = unit.Id.ToString();
            idText.enabled = true;
        }
        CreateAnswerButtons();
    }

    void CreateAnswerButtons()
    {
        var canvas = GameObject.Find("Canvas");
        for (int i = 0; i < InstanceCount; ++i)
        {
            GameObject answerButton = Instantiate(AnswerPrefab, canvas.transform);
            answerButton.transform.localPosition = new Vector3(-100 + i * 50f, -50f, 0);
            answerButton.name = $"Answer {i}";
            var answerText = answerButton.GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = i.ToString();
            var button = answerButton.GetComponent<Button>();
            int index = i; // cannot use i here: https://answers.unity.com/questions/1288510/buttononclickaddlistener-how-to-pass-parameter-or.html
            button.onClick.AddListener(delegate { onAnswerClick(index); });
            answerList.Add(answerButton);
        }
    }

    void onAnswerClick(int index)
    {
        foreach (GameObject answer in answerList)
            Destroy(answer);
        answerList = new();
        foreach (GameObject sphere in sphereList)
            Destroy(sphere);
        sphereList = new();

        if (index == targetId)
            resultText.text = "Correct answer!";
        else
            resultText.text = $"Wrong answer! Sphere {targetId} was correct!";

        resultText.gameObject.SetActive(true);
        StartCoroutine(AllowRestart(showResultDuration));
    }

    IEnumerator AllowRestart(float showResultsDuration)
    {
        yield return new WaitForSeconds(showResultsDuration);
        resultText.gameObject.SetActive(false);
        startButton.interactable = true;

    }
}
