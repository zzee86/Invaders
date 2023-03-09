using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class KeyboardInput : MonoBehaviour
{
    EventSystem eventSystem;
    [SerializeField] private Selectable firstItem;
    [SerializeField] private Button button;

    void Start()
    {
        eventSystem = EventSystem.current;
        firstItem.Select();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable nextItem = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (nextItem != null)
            {
                nextItem.Select();
                Debug.Log(nextItem.name);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            button.onClick.Invoke();
            Debug.Log("Submitted");
        }
    }
}
