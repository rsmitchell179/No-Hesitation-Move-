﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class doughboyDialogue : MonoBehaviour
{

	public Image bubble;
	public GameObject character;
	public TMP_Text ui_text;
	// public Text ui_text;
	public Camera cam;
	private float delay = 0.03f;
	public string actual_text;
	private string current_text = "";

	doughboyClass db_class;

    // Start is called before the first frame update
    void Start()
    {
    	cam = Camera.main;
        bubble.enabled = false;
        ui_text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        db_class = this.gameObject.GetComponent<doughboyClass>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.gameObject.tag == "Player")
    	{
    		// Debug.Log("collision detected");
    		// bubble.enabled = true;
    		current_text = "";
    		// ui_text.text = actual_text;
    		// ui_text.enabled = true;
    		// set_pos(bubble);
    		StartCoroutine(start_text());
    	}
    }

    IEnumerator start_text()
    {
    	for(int i = 0; i <= actual_text.Length; i++)
    	{	
    		ui_text.enabled = true;
    		current_text = actual_text.Substring(0, i);
    		ui_text.text = current_text;
    		yield return new WaitForSecondsRealtime(delay);
    	}
    }

    void OnTriggerStay2D(Collider2D other)
    {
    	if(other.tag == "Player"){
    		bubble.enabled = true;
    		set_pos(bubble);
    	}
    }

    void OnTriggerExit2D(Collider2D other)
    {	
    	if(other.tag == "Player"){
    		StopAllCoroutines();
    		current_text = "";
    		StartCoroutine(delay_setfalse());
    	// bubble.enabled = false;
    	}
    }

    IEnumerator delay_setfalse()
    {
    	yield return new WaitForSeconds (0.0f);
    	ui_text.enabled = false;
    	bubble.enabled = false;
    }

    void set_pos(Image bub)
    {
    	float y_offset = character.GetComponent<SpriteRenderer>().bounds.max.y + db_class.offset;
    	Vector3 bub_position = new Vector3(character.transform.position.x, y_offset, character.transform.position.z);
    	bubble.transform.position = cam.WorldToScreenPoint(bub_position);
    }
}
