using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    private Tilemap tilemap;
    float starttime,nowtime;
    int antleft=0;
    int antright=0;
    Antclass antclass;
    Locate locate;
    GameObject antuilist;
    GameObject oneant;
    readonly int[] dx = { 1, 0, 1, -1, 0, -1 };
    readonly int[] dy = { 0, 1, 1, 0, -1, -1 };
    float[,] pheromone = new float[50,50];
    readonly int LIFESPAN = 64;
    // Ants in [antleft,antright) are likely to survive
    void Born()
    {
        antclass.antlist[antright] = new Antclass.Ant();
        antclass.antlist[antright].x = locate.BORN_X;
        antclass.antlist[antright].y = locate.BORN_Y;
        antright++;
    }
    void Dead()
    {
        while (antclass.antlist[antleft].age >= LIFESPAN)
        {
            antclass.antlist[antleft].hp = 0;
            antleft++;
            pheromone[antclass.antlist[antleft].x + 20, antclass.antlist[antleft].y + 20] /= 0.2f;
        }
    }
    void Move()
    {
        print(antright);
        for (int i = antleft; i < antright; i++)
        {
            if (antclass.antlist[i].x == locate.TARGET_X && antclass.antlist[i].y == locate.TARGET_Y)
            {
                antclass.antlist[i].hp = 0;
            }
            //if (antclass.antlist[i].hp <= 0) continue;
            float[] p = new float[6];
            for (int j = 0; j < 6; j++) p[j] = 0f;
            float sum=0f;
            for (int j = 0; j < dx.Length; j++)
            {
                print(i);
                if(!locate.InPolygon(antclass.antlist[i].x+dx[j], antclass.antlist[i].y+dy[j]))
                {
                    p[j] = 0f;
                }
                else
                {
                    p[j] = locate.Dis(antclass.antlist[i].x+dx[j], antclass.antlist[i].y+dy[j]) * pheromone[antclass.antlist[i].x+dx[j] + 20, antclass.antlist[i].y+dy[j] + 20];
                }
                sum += p[j];
            }
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
            float target = UnityEngine.Random.Range(0, 1);
            int direction = 0;
            while (target > 0)
            {
                target -= p[direction] / sum;
                if (target <= 0) break;
                direction++;
            }
            antclass.antlist[i].x += dx[direction];
            antclass.antlist[i].y += dy[direction];
            pheromone[antclass.antlist[i].x + 20, antclass.antlist[i].y + 20] += 1.0f/ antclass.antlist[i].age;
        }
    }
    void UIofAnt()
    {
        for (int i = 0; i < antuilist.transform.childCount; i++)
        {
            Destroy(antuilist.transform.GetChild(i).gameObject);
        }
        for (int i = antleft; i < antright; i++)
        {
            //if (antclass.antlist[i].hp <= 0) continue;
            Vector3Int cell = new Vector3Int(antclass.antlist[i].x, antclass.antlist[i].y, 0);
            Vector3 grid;
            grid=tilemap.CellToWorld(cell);
            GameObject newant=Instantiate(oneant);
            newant.transform.position = grid;
            if(i==0)print(cell);
        }
    }
    void Start()
    {
        starttime = Time.time;
        antclass = GetComponent<Antclass>();
        locate = GetComponent<Locate>();
        antuilist = GameObject.Find("antlist");
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        oneant = GameObject.Find("oneant");
        for (int i = 0; i < 50; i++)
        {
            for(int j = 0; j < 50; j++)
            {
                pheromone[i,j] = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        nowtime = Time.time;
        if (nowtime - starttime >= 0.95f && nowtime - starttime <= 1.05f)
        {
            Born();
            Dead();
            Move();
            UIofAnt();
            starttime +=1;
        }
    }
}
