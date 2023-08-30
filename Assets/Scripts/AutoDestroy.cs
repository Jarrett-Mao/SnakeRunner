using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    SnakeMovement SM;

    public int life;
    public float lifeForColor;
    TextMesh thisTextMesh;

    GameObject[] ToDestroy;
    GameObject[] ToUnparent;

    int maxLifeForRed = 50;

    Vector3 initialPos;
    public bool dontMove;

    void SetBoxSize(){

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(){

    }

    IEnumerator EnableSomeBars(){
        int i = 0;

        while (i < transform.childCount){
            if (transform.GetChild(i).tag == "Bar"){
                int r = Random.Range(0, 6);
                if(r == 1){
                    ToUnparent[i] = transform.GetChild(i).gameObject;
                }
                else {
                    ToDestroy[i] = transform.GetChild(i).gameObject;
                }
                i++;
                yield return new WaitForSeconds(0.01f);
            }
            else {
                i++;
            }
        }

        for (int k = 0; k < ToUnparent.Length; k++){
            if (ToUnparent[k] != null){
                ToUnparent[k].transform.parent = null;
            }
            if (ToDestroy[k] != null){
                Destroy(ToDestroy[k]);
            }
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision){

    }

    private void OnTriggerStay2D(Collider2D collision){

    }

    public void SetBoxColor(){

    }
}
