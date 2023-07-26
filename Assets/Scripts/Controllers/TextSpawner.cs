using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Controllers
{
    [RequireComponent(typeof(TextParticleController))]
    public class TextSpawner : MonoBehaviour
    {
        [SerializeField] private TextAsset _textAsset;
        
        private TextParticleController _particleController;
        private string[] words;
        private int currentWord = 0;
        private void Awake()
        {
            _particleController = GetComponent<TextParticleController>();
            words = _textAsset.text.Split(' ');
            
        }

        private void Update()
        {
            if (currentWord >= words.Length)
            {
                currentWord = 0;
            }

            var positionX = Random.Range(0, Screen.width);
            var positionY = Random.Range(0, Screen.height);
            _particleController.SpawnWord(new Vector2(positionX, positionY), words[currentWord++]);
        }
    }
}