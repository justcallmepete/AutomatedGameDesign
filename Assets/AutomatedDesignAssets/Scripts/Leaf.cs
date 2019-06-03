using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class Leaf {

	public int minSize = 2;
	public int positionX, positionY;
	public int width, height;
	public Leaf left, right;
	public Rect rect;
	public int debugId;
	public bool isLeaf;
	//public Hallway hallWay;

	private static int debugCounter = 0;

	public Leaf(int posX, int posY, int width, int height){
		positionX = posX;
		positionY = posY;
		this.width = width;
		this.height = height;
		debugId = debugCounter;
		debugCounter++;
	}

	public bool IAmLeaf()
	{
		return left == null && right == null;
	}

	public bool Split(int minSize, int maxSize){
		bool splitH = false;
		if(!IAmLeaf()){
			// this means we already split the bsp
			return false;
		}

		if(width <= minSize || height <= minSize){
			Debug.Log("Not enough width/height");
			return false;
		}
		if (width > height && width / height >= 1.25){
			splitH = false;
		} else 
		if(height > width && height / width >= 1.25){
			splitH = true;
		}

		if(splitH){
			int split = Random.Range(minSize, maxSize);
			left = new Leaf(positionX, positionY, width, split);
			right = new Leaf(positionX, positionY + split, width, height - split);


		//	left = new Leaf(positionX, positionY, width, height / 2);
		//	right = new Leaf(positionX, positionY + minSize, width, height / 2);
		} else {
			int split = Random.Range(minSize, maxSize);
			left = new Leaf(positionX, positionY, split, height);
			right = new Leaf(positionX + split, positionY, width - split, height);

		//		left = new Leaf(positionX, positionY, width / 2, height);
		//	right = new Leaf(positionX + minSize, positionY, width / 2, height);
		}
		return true;
	}

}
