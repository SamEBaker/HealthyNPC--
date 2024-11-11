using UnityEngine;

public class NPCRespawn : MonoBehaviour
{
    [SerializeField] private GameObject NPC;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var AnotherNPC = Instantiate(NPC, transform.position, transform.rotation);
        }
    }


}
