using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private XYPlot plot;
    private ButtonMouseOver[] buttons;
    void Start()
    {
        plot = GameObject.FindWithTag("Graph").GetComponent<XYPlot>();
        plot.Initialize();

        buttons = GameObject.FindObjectsOfType<ButtonMouseOver>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            plot.CreateNewPoint(Input.mousePosition);
            plot.ClearUser();
        }
    }

    private bool IsMouseOverUI()
    {
        foreach (var button in buttons)
        {
            if (button.isMouseOver) return true;
        }

        return false;
    }
}
