using UnityEngine;

public class getState : MonoBehaviour, IInteractable
{
    public string Interact()
    {
        GameObject leftHand = GameObject.FindGameObjectWithTag("invL");
        GameObject rightHand = GameObject.FindGameObjectWithTag("invR");

        if (!leftHand.activeSelf)
        {
            leftHand.SetActive(true);
            leftHand.GetComponent<foodStates>().SetState(GetComponent<foodStates>().currentState);
            leftHand.GetComponent<Dishes>().setDish(GetComponent<Dishes>().currentDish);
        }
        else if (leftHand.activeSelf)
        {
            leftHand.SetActive(false);
        }


        if (!rightHand.activeSelf)
        {
            rightHand.SetActive(true);
            rightHand.GetComponent<foodStates>().SetState(GetComponent<foodStates>().currentState);
            rightHand.GetComponent<Dishes>().setDish(GetComponent<Dishes>().currentDish);
        }
        else if (rightHand.activeSelf)
        {
            rightHand.SetActive(false);
        }



        return null;
    }
}
