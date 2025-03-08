using UnityEngine;
using UnityEngine.Events; 

public class SceneTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter; 
    public UnityEvent onTriggerExit; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player entered the trigger!");
            onTriggerEnter.Invoke(); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger!");
            onTriggerExit.Invoke(); 
        }
    }
}
