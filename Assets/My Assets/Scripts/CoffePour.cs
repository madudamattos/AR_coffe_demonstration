using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffePour : MonoBehaviour
{

    private Transform interactable;
    public Transform waterSpawn = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    public bool pourCheck = false;

    private Stream currentStream = null;

    public ControlWaterLevel waterScript;
    private Coroutine fillingCoroutine = null;

    private bool inReach = false;
    private bool isFilling = false;
    private bool isFull = false;

    private bool stop = false;

    [SerializeField] ActiveCoffeMesh coffeScript;
    [SerializeField] KettleWaterPour kettleScript;

    public bool coffe;
    public bool kettle;


    public void Start()
    {
        interactable = GameObject.Find("Interactable").transform;
    }

    private void Update()
    {

        pourCheck = PourCheck();
        

        if (isPouring != pourCheck && !stop)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
        }

        if (isFull)
        {
            stop = true;
            EndPour();
        }

        //encher a caneca
        inReach = DetecMug();

        if (inReach && !isFilling && isPouring)
        {
            fillingCoroutine = StartCoroutine(waterScript.FillMug());
            isFilling = true;
        }
        else if (!inReach || !isPouring || isFull)
        {
            if (fillingCoroutine != null)
            {
                StopCoroutine(fillingCoroutine);
                fillingCoroutine = null;
            }
            isFilling = false;
        }

    }

    private void StartPour()
    {
        print("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        print("End");
        currentStream.End();
        currentStream = null;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, waterSpawn.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

    private bool IsTapOpen()
    {
        if (interactable.rotation.x < -0.3f)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool DetecMug()
    {
        RaycastHit hit;
        Ray ray = new Ray(waterSpawn.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {

            if (hit.collider.gameObject.tag == "Water")
            {
                isFull = waterScript.isFull;
                return true;
            }
        }

        return false;
    }

    public bool PourCheck()
    {
        kettle = kettleScript.inFilter;
        coffe = coffeScript.isCoffeFilterActive;

        if (kettle && coffe)
        {
            return true;
        }

        return false;
    }
}
