using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antclass : MonoBehaviour
{
    public class Ant
    {
        public int id;
        public int x;
        public int y;
        public int age=0;
        public int hp = 100;
    }
    public Ant[] antlist = new Ant[1000];
}
