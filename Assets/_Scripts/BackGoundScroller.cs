using UnityEngine;
using System.Collections;

public class BackGoundScroller : MonoBehaviour {

	public float scrollSpeed;
	private Renderer ren;
	private Vector2 savedOffset;
	
	void Start () {
		ren = GetComponent<Renderer>();
		savedOffset = ren.sharedMaterial.GetTextureOffset ("_MainTex");
	}
	
	void Update () {
		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (savedOffset.x, y);
		ren.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
	
	void OnDisable () {
		ren.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}