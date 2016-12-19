using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour {
	//速度变量
	public float speed = 0.4f;
	//初始化2D矢量坐标
	Vector2 dest=new Vector2(14,14);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//这个也会每次都调用，但是时间周期性固定，unity一般以固定时间周期来计算物理计算，所以物理计算一般都用这个FixedUpdate来调用。
	void FixedUpdate(){
		// 使对象朝着既定目标坐标移动。每次都会计算移动的距离，所以并不是一次性给予目的地，用movetowards方法再加工一下，就可以每次给一点。
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D>().MovePosition(p);


		//在目的地没有到的情况下，不给于重新的目的地。
		if ((Vector2)transform.position == dest) {
			if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
				dest = (Vector2)transform.position + Vector2.up;
			if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
				dest = (Vector2)transform.position + Vector2.right;
			if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
				dest = (Vector2)transform.position - Vector2.up;
			if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
				dest = (Vector2)transform.position - Vector2.right;
		}

		// 通过计算来判断上下左右的动画，目的地减去物体位子坐标可以判断
		Vector2 dir = dest - (Vector2)transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);


	}

	/*
	 *用来判断是否撞墙 遇到墙为false 没有墙为true 
	 */
	bool valid(Vector2 dir){
		Vector2 pos = transform.position;
		//从前面发射射线获得RaycastHit2d对象 对面是circle collider2d就是我自己，如果对面是box collider2d的话，那就是墙
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		//hit.collider 这里比较了下，是否是CircleCollider2D 是的话 证明前面没有墙，为真，
		return (hit.collider is CircleCollider2D);
	}
}
