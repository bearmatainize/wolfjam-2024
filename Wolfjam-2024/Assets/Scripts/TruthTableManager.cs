using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruthTableManager : MonoBehaviour
{
    public TMP_Text yours;
    public TMP_Text goal;
    public Button test;
    public Button next;
    public RectTransform testRectTransform;
    public RectTransform nextRectTransform;

    [SerializeField] private int goalsIndex;

    private string[] goals = new string[] {
        "0\n1\n1\n1\n", //Or
        "0\n0\n0\n1\n", //And
        "1\n1\n0\n0\n", //Not
        "1\n1\n1\n0\n", //Nand from Not and And
        "1\n1\n1\n0\n", //Pure Nand
        "1\n0\n0\n0\n",//Nor
        "1\n0\n0\n0\n"//Nor from Nand
    };

    void Awake()
    {
        Debug.Log("Running!");
        goal.text = goals[goalsIndex];
        yours.text = "0\n0\n0\n0\n";
        test.interactable = true;
        next.interactable = false;
        // Get the RectTransform of the buttons
        nextRectTransform = next.GetComponent<RectTransform>();
        testRectTransform = test.GetComponent<RectTransform>();

        // Set the Z position of the buttons' RectTransform
        Vector3 nextNewPosition = nextRectTransform.localPosition;
        nextNewPosition.y = nextNewPosition.y + 10000f;
        nextRectTransform.localPosition = nextNewPosition;
        Vector3 testNewPosition = testRectTransform.localPosition;
        testNewPosition.y = testNewPosition.y - 10000f;
        testRectTransform.localPosition = testNewPosition;
        Debug.Log("Ran!");
    }

    public void Check()
    {
        if (yours.text == goal.text)
        {
            Debug.Log("Correct!");
            test.interactable = false;
            next.interactable = true;

            // Set the Z position of the buttons' RectTransform
            Vector3 testNewPosition = testRectTransform.localPosition;
            testNewPosition.y = testNewPosition.y + 10000f;
            testRectTransform.localPosition = testNewPosition;
            Vector3 nextNewPosition = nextRectTransform.localPosition;
            nextNewPosition.y = nextNewPosition.y - 10000f;
            nextRectTransform.localPosition = nextNewPosition;

        }
        else
        {
            Debug.Log("Incorrect!");
        }
    }

    public void ChangeGoal(int index)
    {
        goal.text = goals[index];
    }

    public void ChangeYours(List<int> values)
    {
        yours.text = "";
        for (int i = 0; i < values.Count; i++)
        {
            yours.text += values[i] + "\n";
        }
    }

}
