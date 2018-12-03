using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tile : MonoBehaviour {

	void OnMouseDown(){
		//Pawn.instance.transform.LookAt(transform.position);
		Pawn.instance.transform.DOMove(transform.position,0.5f).SetEase(Ease.InOutQuad);
		
	}
}
