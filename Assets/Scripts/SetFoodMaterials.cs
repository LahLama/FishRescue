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


    public void SetFoodMaterial(GameObject obj, foodStates.State state, Dishes.Dish dish)
    {
        string msg = state.ToString() + "_" + dish.ToString();
        Debug.Log(msg);

        switch (msg)
        {
            case string a when a.ToLower().Contains("raw"):
                switch (msg)
                {
                    case string b when b.ToLower().Contains("chicken"):
                        obj.GetComponent<MeshRenderer>().material = raw_chicken;
                        break;
                    case string c when c.ToLower().Contains("beef"):
                        obj.GetComponent<MeshRenderer>().material = raw_Beef;
                        break;
                    default:
                        break;
                }
                break;

            case string c when c.ToLower().Contains("preping"):
                switch (msg)
                {
                    case string d when d.ToLower().Contains("chicken"):
                        obj.GetComponent<MeshRenderer>().material = preping_chicken;
                        break;
                    case string e when e.ToLower().Contains("beef"):
                        obj.GetComponent<MeshRenderer>().material = preping_Beef;
                        break;
                    default:
                        break;
                }
                break;
            case string d when d.ToLower().Contains("done"):
                switch (msg)
                {
                    case string e when e.ToLower().Contains("chicken"):
                        obj.GetComponent<MeshRenderer>().material = done_chicken;
                        break;
                    case string f when f.ToLower().Contains("beef"):
                        obj.GetComponent<MeshRenderer>().material = done_Beef;
                        break;
                    default:
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
}
