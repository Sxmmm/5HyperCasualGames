using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

    private int n1, n2;
    private int correct, wrong;
    public Text cText, wText;
    public Text equation;
    public Text userinput;

	void Start () {
        n1 = Mathf.Abs(Random.Range(0, 999));
        n2 = Mathf.Abs(Random.Range(0, 999));
	}

	void Update () {
        equation.text = n1 + " + " + n2 + " = ?";
        cText.text = correct.ToString();
        wText.text = wrong.ToString();
    }

    public void Submit() {
        if (userinput.text == (n1 + n2).ToString()) {
            n1 = Mathf.Abs(Random.Range(0, 999));
            n2 = Mathf.Abs(Random.Range(0, 999));
            correct++;
        } else {
            wrong++;
        }
    }
}
