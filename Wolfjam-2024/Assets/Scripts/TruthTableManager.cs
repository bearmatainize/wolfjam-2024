using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TruthTableManager : MonoBehaviour
{
    public TMP_Text yours;
    public TMP_Text goal;

    private string[] goals = new string[] {
        "0\n0\n0\n0\n"
    };

    private void Start()
    {
        goal.text = goals[0];
        yours.text = "1\n1\n1\n1\n";

    }

    public void Check()
    {
        if (yours.text == goal.text)
        {
            Debug.Log("Correct!");
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

    public void ChangeYours(int[] values)
    {
        yours.text = "";
        for (int i = 0; i < values.Length; i++)
        {
            yours.text += values[i] + "\n";
        }
    }

}
