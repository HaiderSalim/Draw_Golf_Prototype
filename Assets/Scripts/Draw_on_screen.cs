using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Draw_on_screen : MonoBehaviour
{
    [SerializeField]
    GameObject brush;
    [SerializeField]
    private int Maxpoints = 100;
    [SerializeField]
    private Image ink_bar;

    LineRenderer CurrentlineRenderer;
    private Vector2 lastpos;
    private List<Vector3> newposition = new List<Vector3>();

    void Start()
    {
        
    }
    void Update()
    {
        Draw();
    }
    void Draw()
    {
        if (Input.touchCount != 0)
        {
            var mouspos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CreateBrush();
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                ink_bar.fillAmount -= 0.005f;
                if(lastpos != mouspos)
                {
                    Addpoints(mouspos);
                    lastpos = mouspos;
                }
            }
        }
    }

    void CreateBrush()
    {
        var brushinstance = Instantiate(brush, lastpos, Quaternion.identity);
        CurrentlineRenderer = brushinstance.GetComponent<LineRenderer>();
        newposition.Clear();
    }

    void Addpoints(Vector2 pointpos)
    {
        newposition.Add(pointpos);

        if (CurrentlineRenderer.positionCount > Maxpoints)
        {
            newposition.RemoveAt(0);
        }

        Update_points();
    }

    void Update_points()
    {
        CurrentlineRenderer.positionCount = newposition.Count;
        for (int i = 0; i < newposition.Count; i++)
        {
            CurrentlineRenderer.SetPosition(i, newposition[i]);
        }
    }
}
