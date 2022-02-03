using UnityEngine;
using UnityEngine.UI;


namespace ZombieQuest
{
    
    public class UIManager : MonoBehaviour
    {
        
        public enum Alignment
        {
            Left,
            Right,
            TopLeft,
            TopRight,
            Character
        }

        
        public static UIManager Instance;

        [SerializeField]
        private Text leftText = null;
        [SerializeField]
        private Text rightText = null;
        [SerializeField]
        private string textToTrim = null;

        [SerializeField] private Text _topLeftText;
        [SerializeField] private Text _topRightText;
        [SerializeField] private TextMesh _characterHeadText;

        
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        
        public void Display(State enteredState, Alignment alignment)
        {
            var name = enteredState.ToString();
            name = name.Remove(name.IndexOf(textToTrim), textToTrim.Length);
            name = name.Remove(name.IndexOf("State"), 5);

            if (alignment == Alignment.Left)
            {
                leftText.text = name;
            }
            else
            {
                rightText.text = name;
            }
        }
        
        
        public void Display(string text, Alignment alignment)
        {
 
            if (alignment == Alignment.Left)
            {
                leftText.text = text;
            }
            else
            {
                rightText.text = text;
            }
        }
        
        
        public void Display(string text, Text textContainer)
        {
            textContainer.text = text;
        }


        public void Clear(Text textContainer)
        {
            textContainer.text = "";
            textContainer.gameObject.SetActive(false);
        }

        
        public void DisplayCharacterText(string text)
        {
            _characterHeadText.text = text;
        }
        
        
        
    }
}
