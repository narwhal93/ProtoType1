using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hoon
{
public class GameManager : SingletonMonoBehaviour<GameManager> {


    void Awake()
    {
        Screen.SetResolution(1600, 900, true); // Fix Screen Resolution. When you change this value, change the value in the canvas
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
}