using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController2048 : MonoBehaviour
{
    public static GameController2048 instance;

    [SerializeField] GameObject fillPrefab;
    [SerializeField] Transform[] allCells;
    [SerializeField] Canvas Canvas;
    [SerializeField] GameObject failPanel;
    
    public static float ScaleFactor;

    public ColorSetter colorSetter;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        ScaleFactor = Canvas.scaleFactor; 
    }
    private void Start()
    {
        AddNewLineBelow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddNewLineBelow();      
        }
    }
    public void AddNewLineBelow()
    {
        StartCoroutine(ShiftAllUp());
    }
    private IEnumerator  ShiftAllUp()
    {
        yield return null;
        for (int i=0; i<allCells.Length; i++)
        {
            if (i - 4 < 0) { continue; }                                 //check is top row
            if (allCells[i].transform.childCount == 0) { continue; }    //check is cell filled?
            if (allCells[i-4].childCount >0) { continue; }             //check is top cell filled?

            GameObject tempFill = Instantiate(fillPrefab, allCells[i-4]);

            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            int cachedVal = allCells[i].GetChild(0).GetComponent<Fill2048>().value;

            tempFillComp.FillValueUpdate(cachedVal, colorSetter.SetColor(cachedVal));
            Destroy(allCells[i].GetChild(0).gameObject);
            yield return null;
        }

        GenerateNewLine();
        CheckFailCase();
        BringAllDown(); 

    }
    private void GenerateNewLine()
    {
        for (int i = 12; i <= 15; i++)
        {
            if (allCells[i].childCount != 0) { continue; } //check if filled? 

            float chance = Random.Range(0f, 1f);
            if (chance <= 0.4)
            {
                GameObject tempFill = Instantiate(fillPrefab, allCells[i]);
                Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
                tempFillComp.FillValueUpdate(1, colorSetter.SetColor(1));

            }
            else if (chance < 0.8 && chance > 0.4)
            {
                GameObject tempFill = Instantiate(fillPrefab, allCells[i]);
                Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
                tempFillComp.FillValueUpdate(2, colorSetter.SetColor(2));
            }
            else
            {
                GameObject tempFill = Instantiate(fillPrefab, allCells[i]);
                Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
                tempFillComp.FillValueUpdate(4, colorSetter.SetColor(4));
            }
        }
    }
    public void BringAllDown()
    {
        StartCoroutine(ShiftAllDown());
    }
    private IEnumerator ShiftAllDown()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i <= 11; i++)
        {
            if (allCells[i].childCount == 0) { continue; } //check if empty  
            if (allCells[i + 4].childCount == 0)
            {
                allCells[i ].transform.GetChild(0).SetParent(allCells[i+4]);
                yield return null;
                if (i > 3) { i = i - 5; }
            }
        }
    }


    private void CheckFailCase()
    {
        StartCoroutine(CheckGameOver());
    }
    private IEnumerator CheckGameOver()
    {
        yield return new WaitForEndOfFrame();
        int maxFilled = allCells.Length;
        int temp = 0;

        for (int i = 0; i <= 15; i++)
        {
            if (allCells[i].childCount == 0) { continue; }
            else { temp++; }
        }
        if (temp >= maxFilled) { failPanel.SetActive(true); }
    }


}
