
using UnityEngine;

public class SetFoodMaterials : MonoBehaviour
{
    public Material Trash;
    public Material raw_chicken;
    public Material preping_chicken;
    public Material done_chicken;
    public Material raw_Beef;
    public Material preping_Beef;
    public Material done_Beef;
    public Material raw_lamb;
    public Material preping_lamb;
    public Material done_lamb;
    public Material raw_potato;
    public Material preping_potato;
    public Material done_potato;
    public Material raw_salad;
    public Material preping_salad;
    public Material done_salad;




    public void SetFoodMaterial(GameObject obj, foodStates.State state, Dishes.Dish dish)
    {
        string msg = state.ToString() + "_" + dish.ToString();
        //Debug.Log(msg);

        switch (msg)
        {
            case string z when z.ToLower().Contains("unwashed"):
                switch (msg)
                {
                    case string b when b.ToLower().Contains("potato"):
                        obj.GetComponent<MeshRenderer>().material = raw_potato;
                        break;
                    case string c when c.ToLower().Contains("lettuce"):
                        obj.GetComponent<MeshRenderer>().material = raw_salad;
                        break;
                }
                break;


            case string a when a.ToLower().Contains("raw"):
                switch (msg)
                {
                    case string b when b.ToLower().Contains("chicken"):
                        obj.GetComponent<MeshRenderer>().material = raw_chicken;
                        break;
                    case string c when c.ToLower().Contains("beef"):
                        obj.GetComponent<MeshRenderer>().material = raw_Beef;
                        break;
                    case string d when d.ToLower().Contains("lamb"):
                        obj.GetComponent<MeshRenderer>().material = raw_lamb;
                        break;
                    case string e when e.ToLower().Contains("potato"):
                        obj.GetComponent<MeshRenderer>().material = raw_potato;
                        break;
                    case string f when f.ToLower().Contains("salad"):
                        obj.GetComponent<MeshRenderer>().material = raw_salad;
                        break;
                    default:
                        break;
                }
                break;

            case string a when a.ToLower().Contains("preping"):

                switch (msg)
                {
                    case string b when b.ToLower().Contains("chicken"):
                        obj.GetComponent<MeshRenderer>().material = preping_chicken;
                        break;
                    case string c when c.ToLower().Contains("beef"):
                        obj.GetComponent<MeshRenderer>().material = preping_Beef;
                        break;
                    case string d when d.ToLower().Contains("lamb"):
                        obj.GetComponent<MeshRenderer>().material = preping_lamb;
                        break;
                    case string e when e.ToLower().Contains("potato"):
                        obj.GetComponent<MeshRenderer>().material = preping_potato;
                        break;
                    case string f when f.ToLower().Contains("salad"):
                        obj.GetComponent<MeshRenderer>().material = preping_salad;
                        break;

                    default:
                        break;
                }
                break;
            case string a when a.ToLower().Contains("done"):

                switch (msg)
                {
                    case string b when b.ToLower().Contains("chicken"):
                        obj.GetComponent<MeshRenderer>().material = done_chicken;
                        break;
                    case string c when c.ToLower().Contains("beef"):
                        obj.GetComponent<MeshRenderer>().material = done_Beef;
                        break;
                    case string d when d.ToLower().Contains("lamb"):
                        obj.GetComponent<MeshRenderer>().material = done_lamb;
                        break;
                    case string e when e.ToLower().Contains("potato"):
                        obj.GetComponent<MeshRenderer>().material = done_potato;
                        break;
                    case string f when f.ToLower().Contains("salad"):
                        obj.GetComponent<MeshRenderer>().material = done_salad;
                        break;
                }
                break;

            case string d when d.Contains("Trash"):

                obj.GetComponent<MeshRenderer>().material = Trash;
                break;

            default:
                break;
        }

    }

    public void SetCustomerPreview(GameObject obj, Dishes.Dish dish)
    {
        switch (dish)
        {
            case Dishes.Dish.chicken:
                obj.GetComponent<MeshRenderer>().material = done_chicken;
                break;
            case Dishes.Dish.beef:
                obj.GetComponent<MeshRenderer>().material = done_Beef;
                break;
            case Dishes.Dish.lamb:
                obj.GetComponent<MeshRenderer>().material = done_lamb;
                break;
            case Dishes.Dish.potato:
                obj.GetComponent<MeshRenderer>().material = done_potato;
                break;
            case Dishes.Dish.salad:
                obj.GetComponent<MeshRenderer>().material = done_salad;
                break;
            default:
                break;
        }

    }
}
