using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    private Text _interaction_text;

    public static SelectionManager _instance;
    public GameObject _interaction_Info_UI;
    public GameObject _selectedObject;

    #region MONO

    private void Awake()
    {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    { 
        _interaction_text = _interaction_Info_UI.GetComponent<Text>();
    }

    #endregion

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if  (hit.collider.TryGetComponent(out InteractableObject interactable) && interactable.playerRange)
            {
                _interaction_text.text = interactable.GetName();
                _interaction_Info_UI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    interactable.onClick();
                }
            }
            else //if there is a hit without an intaractable script
            {
                _interaction_Info_UI.SetActive(false);
            }

        } 
        else //if there is no hit at all
        {
            _interaction_Info_UI.SetActive(false);
        }
    }
}
