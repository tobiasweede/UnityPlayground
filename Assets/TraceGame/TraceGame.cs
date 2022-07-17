using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraceGame : MonoBehaviour
{
    public TraceSphere TracePrefab;
    public GameObject AnswerPrefab;
    public int InstanceCount;
    public float highlightTargetDuration;
    public float duration;
    private Vector3 areaSize;
    private int targetId;
    private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        CreateAnswerButtons();
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
        StartCoroutine(StopGame(duration));
    }

    void InstantiateGameSpheres(int id)
    {
        TraceSphere newUnit = Instantiate(TracePrefab, transform);
        newUnit.SetAreaSize(areaSize);
        newUnit.Id = id;
        newUnit.name = "Sphere " + id.ToString();

        Vector3 position = new Vector3(
        Random.Range(-areaSize.x, areaSize.x),
        Random.Range(-areaSize.y, areaSize.y),
        Random.Range(-areaSize.z, areaSize.z));
        newUnit.transform.localPosition = position;
        if (newUnit.Id == targetId)
        {
            StartCoroutine(HighlightTarget(newUnit, highlightTargetDuration));
        }
    }

    IEnumerator HighlightTarget(TraceSphere newUnit, float duration)
    {
        Color origColor = newUnit.GetComponent<MeshRenderer>().material.color;
        newUnit.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(duration);
        newUnit.GetComponent<MeshRenderer>().material.color = origColor;
    }
    IEnumerator StopGame(float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach (TraceSphere unit in GetComponentsInChildren<TraceSphere>())
        {
            unit.ToggleMove = false;
            var idText = unit.GetComponentInChildren<TextMeshPro>();
            idText.text = unit.Id.ToString();
            idText.enabled = true;
        }
        startButton.interactable = true;
    }

    void CreateAnswerButtons()
    {
        var canvas = GameObject.Find("Canvas");
        for (int i = 0; i < InstanceCount; ++i)
        {
            GameObject newAnswer = Instantiate(AnswerPrefab, canvas.transform);
            newAnswer.name = $"Answer {i}";
            var answerText = newAnswer.GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = i.ToString();
        }

    }

    void Update()
    {

    }
}
