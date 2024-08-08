using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class ChariotManager : MonoBehaviour
{
    public List<ChariotManivelle> Manivelles;
    public List<GameObject> Lights;
    public GameObject BlockingEnd;
    private StringBuilder codeBuilder = new StringBuilder("XXXX");

    private void Update()
    {
        for (int i = 0; i < Manivelles.Count; i++)
        {
            codeBuilder[i] = Manivelles[i].GetChariotPos().ToString().First();
        }

        if(codeBuilder.ToString() == "3211")
        {
            foreach(GameObject light in Lights)
            {
                light.SetActive(true);
            }
            Destroy(BlockingEnd);
        }
    }
}
