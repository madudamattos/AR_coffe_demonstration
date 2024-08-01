using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWaterLevel : MonoBehaviour
{
    
    protected bool isEmpty = true;
    public bool isFull = false;
    private Vector3 initialPosition;

    [SerializeField] private GameObject water;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = water.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FillMug()
    {
        if (isEmpty)
        {
            water.GetComponent<MeshRenderer>().enabled = true;
            isEmpty = false;
        }

        while(!isFull)
        {
            yield return new WaitForSeconds(0.3f);
            water.transform.localPosition += new Vector3(0, 1f, 0);

            if (water.transform.localPosition.y >= 27)
            {
                isFull = true;
            }
        }
    }

    public IEnumerator EmptyMug()
    {
        if (isEmpty)
        {
            water.GetComponent<MeshRenderer>().enabled = false;
            isEmpty = true;
        }

        while (!isEmpty)
        {
            yield return new WaitForSeconds(0.3f);
            water.transform.localPosition -= new Vector3(0, 1f, 0);

            if (water.transform.localPosition.y <= initialPosition.y)
            {
                isEmpty = true;
            }
        }
    }

    public void Fill()
    {
        StartCoroutine(FillMug());
        if (isFull)
        {
            StopCoroutine(FillMug());
        }

    }
    
}
