using UnityEngine;

public class MiniRune : MonoBehaviour
{
    public string wantedChar;
    private string givenChar;
    private bool goodPos;

    private void Update()
    {
        if (wantedChar == givenChar)
            goodPos = true;
        else
            goodPos = false;
    }

    public bool GetGood()
    {
        return goodPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        givenChar = other.gameObject.name;
    }
}
