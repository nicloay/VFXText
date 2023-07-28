using System;
using UnityEngine;

namespace DefaultNamespace.Controllers
{
    public class TriggerComponentOnButtonPress : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour target;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                target.enabled = !target.enabled;
            }
        }
    }
}