﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletfire : MonoBehaviour
{
    public AnimationCurve yCurve;
    public AnimationCurve xCurve;
    public WordHolder words;
    private string level;
    [SerializeField]
    private int bulletsamount = 10;
    private float zAngle;
    private float zStep;

    [SerializeField]
    private float startangle = 359f, endangle = 180f;

    private Vector2 bulletmovedirection;
    private bool xMove = false;
    private bool yMove = false;




    // Start is called before the first frame update
    void Start()
    {
        zAngle = 60;
        zStep = 10;
        level = "one";
        //InvokeRepeating("FireMove", 0f, 0.4f);
        //InvokeRepeating("FireSpread", 0f, 0.2f);
        //InvokeRepeating("FireSpread", 0f, 0.4f);
        //InvokeRepeating("FireCurve", 0f, 2f);
        //InvokeRepeating("FireRandom", 0f, 0.4f);
    }

    private void FireRandom()
    {
        float angleStep = (endangle - startangle) / bulletsamount;
        float angle = startangle;
        zAngle = Random.Range(60, 120);

        //for (int i = 0; i < bulletsamount + 1; i++)
        //{
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        GameObject bul = Bulletpoolscript.bulletpoolinstance.GetBullet(); //Retrieve a bullet, make it active and fly in the right direction. 
        if (Random.Range(0, 5)<4){
            bul.GetComponent<SpriteRenderer>().sprite = words.retrieveDamageWord(level);
            bul.tag = "badend";
        }
        else{
            bul.GetComponent<SpriteRenderer>().sprite = words.retrieveCorrectWord(level);
            bul.tag = "goodend";
        }
        
        bul.transform.position = transform.position;
        bul.transform.rotation = Quaternion.identity;
        bul.transform.Rotate(0, 0, Random.Range(60, 120), Space.Self);
        bul.SetActive(true);
        bul.GetComponent<Bulletscript>().setMoveDirection(bulDir);
        Destroy(bul.GetComponent<BoxCollider2D>());
        bul.AddComponent<BoxCollider2D>();
        angle += angleStep;
      //  }
    }

    private void FireCurve()
    {
        float angleStep = (endangle - startangle) / bulletsamount;
        float angle = startangle;
        zAngle = 60;

        for (int i = 0; i < bulletsamount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = Bulletpoolscript.bulletpoolinstance.GetBullet(); //Retrieve a bullet, make it active and fly in the right direction. 
            if (Random.Range(0, 5)<4){
                bul.GetComponent<SpriteRenderer>().sprite = words.retrieveDamageWord(level);
                bul.tag = "badend";
            }
            else{
                bul.GetComponent<SpriteRenderer>().sprite = words.retrieveCorrectWord(level);
                bul.tag = "goodend";
            }
            
            bul.transform.position = transform.position;
            bul.transform.rotation = Quaternion.identity;
            bul.transform.Rotate(0, 0, zAngle, Space.Self);
            bul.SetActive(true);
            bul.GetComponent<Bulletscript>().setMoveDirection(bulDir);
            Destroy(bul.GetComponent<BoxCollider2D>());
            bul.AddComponent<BoxCollider2D>();
            angle += angleStep;
            zAngle+=10;
        }
    }

    private void FireSpread()
    {
        float angleStep = (endangle - startangle) / bulletsamount;
        float angle = startangle;

        
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        GameObject bul = Bulletpoolscript.bulletpoolinstance.GetBullet(); //Retrieve a bullet, make it active and fly in the right direction. 
        if (Random.Range(0, 5)<4){
            bul.GetComponent<SpriteRenderer>().sprite = words.retrieveDamageWord(level);
            bul.tag = "badend";
        }
        else{
            bul.GetComponent<SpriteRenderer>().sprite = words.retrieveCorrectWord(level);
            bul.tag = "goodend";
        }
        
        bul.transform.position = transform.position;
        bul.transform.rotation = Quaternion.identity;
        bul.transform.Rotate(0, 0, zAngle, Space.Self);
        bul.SetActive(true);
        bul.GetComponent<Bulletscript>().setMoveDirection(bulDir);
        Destroy(bul.GetComponent<BoxCollider2D>());
        bul.AddComponent<BoxCollider2D>();
        angle += angleStep;
        zAngle+=zStep;
        if (zAngle==120){
            zStep = -10;
        }
        else if (zAngle==60){
            zStep = 10;
        }
        
    }

    private void FireMove()
    {
        float angleStep = (endangle - startangle) / bulletsamount;
        float angle = startangle;

        
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        GameObject bul = Bulletpoolscript.bulletpoolinstance.GetBullet(); //Retrieve a bullet, make it active and fly in the right direction. 
        if (Random.Range(0, 5)<4){
            bul.GetComponent<SpriteRenderer>().sprite = words.retrieveDamageWord(level);
            bul.tag = "badend";
        }
        else{
            bul.GetComponent<SpriteRenderer>().sprite = words.retrieveCorrectWord(level);
            bul.tag = "goodend";
        }
        
        bul.transform.position = transform.position;
        bul.transform.rotation = Quaternion.identity;
        bul.transform.Rotate(0, 0, 90, Space.Self);
        bul.SetActive(true);
        bul.GetComponent<Bulletscript>().setMoveDirection(bulDir);
        Destroy(bul.GetComponent<BoxCollider2D>());
        bul.AddComponent<BoxCollider2D>();
        angle += angleStep;
        
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("FireMove", 0f, 0.4f);
        //InvokeRepeating("FireSpread", 0f, 0.2f);
        //InvokeRepeating("FireSpread", 0f, 0.4f);
        //InvokeRepeating("FireCurve", 0f, 2f);
        //InvokeRepeating("FireRandom", 0f, 0.4f);
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            xMove = false;
            yMove = false;
            CancelInvoke();
            InvokeRepeating("FireMove", 0f, 2f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            xMove = false;
            yMove = false;
            CancelInvoke();
            InvokeRepeating("FireCurve", 0f, 2f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            zAngle = 60;
            xMove = false;
            yMove = false;
            CancelInvoke();
            InvokeRepeating("FireSpread", 0f, 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){
            xMove = false;
            yMove = true;
            CancelInvoke();
            InvokeRepeating("FireMove", 0f, 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)){
            xMove = true;
            yMove = false;
            CancelInvoke();
            InvokeRepeating("FireRandom", 0f, 0.4f);
        }
        if (yMove){
            transform.position = new Vector3(transform.position.x, yCurve.Evaluate((Time.time)), transform.position.z);
        }
        if (xMove){
            transform.position = new Vector3(xCurve.Evaluate((Time.time)), transform.position.y, transform.position.z);
        }
        //transform.position = new Vector3(transform.position.x, yCurve.Evaluate((Time.time)), transform.position.z);
        //transform.position = new Vector3(xCurve.Evaluate((Time.time)), transform.position.y, transform.position.z);
        //transform.RotateAround(new Vector3(-1, -2, 0), new Vector3(0, 0, 1), 20 * Time.deltaTime);
    }
}
