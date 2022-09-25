using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Locate : MonoBehaviour
{
    private Tilemap tilemap;
    GameObject locate1,locate2,locate3,locate4,locate5,locate6;
    int[] edge_x = new int[7];
    int[] edge_y = new int[7];
    public int TARGET_X, TARGET_Y;
    public float Dis(int point_x, int point_y)
    {
        return Mathf.Sqrt((TARGET_X - point_x) * (TARGET_X - point_x) + (TARGET_Y - point_y) * (TARGET_Y - point_y));
    }
    public bool InPolygon(int point_x,int point_y)
    {
        bool _in = false;
        for (int i = 0, j = edge_x.Length - 1; i < edge_x.Length; j = i++)
        {
            if (((edge_y[i] > point_y) != (edge_y[j] > point_y)) &&
                (point_x < (edge_x[j] - edge_x[i]) * (point_y - edge_y[i]) / (edge_y[j] - edge_y[i]) + edge_x[i]))
                _in = !_in;
        }
        return _in;
    }
    // Start is called before the first frame update
    void Start()
    {
        locate1 = GameObject.Find("locate1");
        locate2 = GameObject.Find("locate2");
        locate3 = GameObject.Find("locate3");
        locate4 = GameObject.Find("locate4");
        locate5 = GameObject.Find("locate5");
        locate6 = GameObject.Find("locate6");
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        edge_x[0]=tilemap.WorldToCell(locate1.transform.position).x;
        edge_y[0] = tilemap.WorldToCell(locate1.transform.position).y;
        edge_x[1] = tilemap.WorldToCell(locate2.transform.position).x;
        edge_y[1] = tilemap.WorldToCell(locate2.transform.position).y;
        edge_x[2] = tilemap.WorldToCell(locate3.transform.position).x;
        edge_y[2] = tilemap.WorldToCell(locate3.transform.position).y;
        edge_x[3] = tilemap.WorldToCell(locate4.transform.position).x;
        edge_y[3] = tilemap.WorldToCell(locate4.transform.position).y;
        edge_x[4] = tilemap.WorldToCell(locate5.transform.position).x;
        edge_y[4] = tilemap.WorldToCell(locate5.transform.position).y;
        edge_x[5] = tilemap.WorldToCell(locate6.transform.position).x;
        edge_y[5] = tilemap.WorldToCell(locate6.transform.position).y;
        edge_x[6] = tilemap.WorldToCell(locate1.transform.position).x;
        edge_y[6] = tilemap.WorldToCell(locate1.transform.position).y;
        TARGET_X= tilemap.WorldToCell(locate4.transform.position).x;
        TARGET_Y= tilemap.WorldToCell(locate4.transform.position).y;
    }
}
