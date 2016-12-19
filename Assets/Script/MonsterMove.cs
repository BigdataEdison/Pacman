using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour {
	//坐标地址，开始轮询此坐标数组
	public Transform[] waypoints;
	//数组角标
	int cur = 0;

	public float speed = 0.3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		//坐标不等于目的坐标的时候继续与西宁。
		if (transform.position != waypoints[cur].position) {
			Vector2 p = Vector2.MoveTowards(transform.position,waypoints[cur].position ,speed);
			GetComponent<Rigidbody2D>().MovePosition(p);
		}
		// 坐标等于时，开始走下一个坐标点。走完开始循环
		else cur = (cur + 1) % waypoints.Length;

		// Animation
		Vector2 dir = waypoints[cur].position - transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}

	void OnTriggerEnter2D(Collider2D co){
		if (co.name == "pacman") {
			Destroy (GameObject.Find("pacman"));
		}
	}
}
