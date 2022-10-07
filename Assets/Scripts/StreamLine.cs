using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamLine : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private Vector3 targetPosition = Vector3.zero;

    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;

    public AddScore addScore;

    private bool [] oliveOilType = new bool[2];

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
        addScore = FindObjectOfType<AddScore>();
        addScore.activateScore = false; 
    }

    private void Start()
    {
        if (transform.parent.name == "ChousasNostrasBottle") //Octopus2
        {
            oliveOilType[0] = true;
            oliveOilType[1] = false;  
        }
        else if (transform.parent.name == "RoundOliveOilBottle_SegredosDoCoa") //Pizza2
        {
            oliveOilType[0] = false;
            oliveOilType[1] = true;        
        }
        else if (transform.parent.name == "RoundOliveOilBottle_SegredosDoCoa" && transform.parent.name == "ChousasNostrasBottle") 
        {
            //Debug.Log("Bug na Matrix");       
        }
        else
        {
            oliveOilType[0] = false;
            oliveOilType[0] = false;
        }
        
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {        
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);
            MoveToPosition(1, targetPosition);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
        StoppedPouring();
    }

    private IEnumerator EndPour()
    {
        while (!HasReachedPosition(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }

        Destroy(gameObject);
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

        if (hit.transform.CompareTag("FoodGame"))
        {
            if (hit.transform.name == "Octopus2(Clone)")
            {
                if(oliveOilType[0])
                {
                    PourOilInFood();
                    addScore.RightOliveOil();
                }
                else if(!oliveOilType[0])
                {
                    addScore.WrongOliveOil();
                }
            }

            else if (hit.transform.name == "Pizza2(Clone)")
            {
                if(oliveOilType[1])
                {
                    PourOilInFood();
                    addScore.RightOliveOil();
                }
                else if(!oliveOilType[1])
                {
                    addScore.WrongOliveOil();
                }            
            }
            else
            {
                StoppedPouring();
            }            
        }
        else
        {
            StoppedPouring();
        }      

        return endPoint;
    }

    private void PourOilInFood()
    {
        addScore.activateScore = true;
    }

    private void StoppedPouring()
    {
        addScore.activateScore = false;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;

            bool isHiting = HasReachedPosition(1, targetPosition);

            splashParticle.gameObject.SetActive(isHiting);

            yield return null;

        }
    }

}
