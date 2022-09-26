using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antclass : MonoBehaviour
{
    public struct Ant
    {
        public int id;
        public int x;
        public int y;
        public int age;
        public int hp;
    }
    public Ant[] antlist = new Ant[1000];
    void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            antlist[i].id = i;
            antlist[i].x = 0;
            antlist[i].y = 0;
            antlist[i].age = 0;
            antlist[i].hp = 100;
        }
    }
}
