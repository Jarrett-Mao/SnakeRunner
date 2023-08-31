using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour
{
    SnakeMovement SM;

    void Start(){
        SM = transform.GetComponentInParent<SnakeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Box" && transform == SM.BodyParts[0]){
            if(SM.BodyParts.Count > 1 && SM.BodyParts[1] != null){
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position + new Vector3(0, 0.5f, 0);
            }
            else if (SM.BodyParts.Count == 1){
                SM.PartsAmountTextMesh.transform.parent = null;
            }

            SM.SnakeParticle.Stop();
            SM.SnakeParticle.transform.position = collision.contacts[0].point;
            SM.SnakeParticle.Play();
            Destroy(this.gameObject);
            GameController.Score++;

            collision.transform.GetComponent<AutoDestroy>().life -= 1;
            collision.transform.GetComponent<AutoDestroy>().UpdateText();

            collision.transform.GetComponent<AutoDestroy>().SetBoxColor();
            if (SM.BodyParts.Count > 0) // Check if there are elements in the list
            {
                SM.BodyParts.Remove(SM.BodyParts[0]);
            }
            else
            {
                Debug.LogWarning("No body parts to remove.");
            }
            // SM.BodyParts.Remove(SM.BodyParts[0]);
        }
        else if (collision.transform.tag == "SimpleBox" && transform == SM.BodyParts[0]){
            SM.SnakeParticle.Stop();
            
            SM.SnakeParticle.transform.position = collision.contacts[0].point;
            
            SM.SnakeParticle.Play();

            if(SM.BodyParts.Count > 1 && SM.BodyParts[1] != null){
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position + new Vector3(0, 0.5f, 0);
            }
            else if(SM.BodyParts.Count == 1){
                SM.PartsAmountTextMesh.transform.parent = null;
            }

            Destroy(this.gameObject);

            GameController.Score++;

            collision.transform.GetComponent<AutoDestroy>().life -= 1;
            collision.transform.GetComponent<AutoDestroy>().UpdateText();

            collision.transform.GetComponent<AutoDestroy>().SetBoxColor();
            SM.BodyParts.Remove(SM.BodyParts[0]);
        }
        else if (collision.transform.tag == "SimpleBox" && transform != SM.BodyParts[0]){
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), collision.transform.GetComponent<Collider2D>());
            collision.transform.GetComponent<AutoDestroy>().dontMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (SM.BodyParts.Count > 0){
            if(collision.transform.tag == "Food" && transform == SM.BodyParts[0]){
                for(int i = 0; i < collision.transform.GetComponent<FoodBehavior>().foodAmount; i++){
                    SM.AddBodyPart();
                }
                Destroy(collision.transform.gameObject);
            }
        }
    }
}



