using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KettleWaterPour : MonoBehaviour
{
    // GameObjects de operacao 
    public Transform waterSpawn = null;
    public GameObject streamPrefab = null;
    private Stream currentStream = null;
    [SerializeField] Transform interactable;

    // Encher o recipiente de agua
    public ControlWaterLevel waterScript;
    private Coroutine fillingCoroutine = null;


    // Variaveis de controle de estado 
    protected bool isPouring = false;
    public bool pourCheck = false;

    protected bool inReach = false;
    protected bool isFilling = false;
    protected bool isFull = false;
    public bool inFilter = false;
    public bool filterCheck = false;

    [SerializeField] CoffePour coffePour;
    [SerializeField] ActiveCoffeMesh coffeScript;

    private Coroutine coffeCoroutine = null;

    public bool waterHitFilter = false;


    public void Start()
    {

    }

    private void Update()
    {
        pourCheck = ActiveWater();

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }

        // Nesse caso derramar agua vai engatilhar uma acao, e nesse caso essa ação é encher a caneca de água 

        if (filterCheck == false)
        {
            inFilter = DetectFilter();
        }
        
        }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, waterSpawn.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }


    // Sobrescreve a classe original do PourDetector, de forma que a ativação da agua da chaleira é acontece quando ela esta inclinada o suficiente
    public bool ActiveWater()
    {
        if (transform.rotation.x < -0.2f || transform.rotation.x > 0.2f)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool DetectFilter()
    {
        RaycastHit hit;
        Ray ray = new Ray(waterSpawn.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {

            if (hit.collider.gameObject.tag == "CoffeFilter")
            {
                filterCheck = true;
                return true;
            }
        }

        return false;
    }

    //  Sobrescreve a função original do PourDetector, de forma que a ação desencadeada é a coagem do café.
    public void TriggerAction()
    {
        inFilter = DetectFilter();
    } 
}
