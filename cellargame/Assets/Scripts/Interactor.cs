using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

interface IInteractible
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    Transform interactorSource;
    [SerializeField] private float interactRange;

    void Start()
    {
        interactorSource = transform;
    }

    void Update()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractible interactObj))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    interactObj.Interact();
                    Debug.Log("Interact");
                }

                Debug.Log("Interactible Found");
            }
        }
    }
}