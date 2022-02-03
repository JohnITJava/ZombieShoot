using System;
using TestScripts;
using UnityEngine;
using UnityEngine.UI;


namespace ZombieQuest
{
    
    internal sealed class DialogController : MonoBehaviour
    {
        
        private UIManager _UIManager;
        private SoundManager _soundManager;
        
        private MainInitializer _mainInitializer;
        
        private char _specDivider = '|';
        private float _textDelay = 1.0f;
        private float _startDelay = 0.0f;

        private int _currentPointer = 0;

        private bool _isNeedStartMessage;
        private bool _isNeedDeadManMessage;

        
        private string _characterIntroText = "Hm... | \n What's going here...";
        
        private string _sittingDeadManText = "Khh... | Khhh... \n" +
                                         "Listen!! | Khhh... | Khh \n" +
                                         "Listen! | I trying! ... Khh... | \n" +
                                         "I CAN NOT SAVE THEM !!! | Khh.. | \n" +
                                         "Save them... | Save them... plz";

        
        // public DialogManager() { }
        //
        //
        // public DialogManager(MainInitializer mainInitializer)
        // {
        //     _mainInitializer = mainInitializer;
        //     _mainInitializer.OnStartEvent += StartMessage;
        //     
        //     _UIManager = UIManager.Instance;
        //     _soundManager = SoundManager.Instance;
        // }


        private void Start()
        {
            _UIManager = UIManager.Instance;
            _soundManager = SoundManager.Instance;
        }


        private void Update()
        {
            if (_isNeedStartMessage)
            {
                StartMessage();
            }

            if (_isNeedDeadManMessage)
            {
                
            }
            
        }


        private void StartMessage()
        {
            var textStrings = _characterIntroText.Split(_specDivider);
            _startDelay += Time.deltaTime;

            if (_startDelay >= _textDelay)
            {
                _UIManager.DisplayCharacterText(textStrings[_currentPointer]);
                _currentPointer += 1;
                
                if (_currentPointer == textStrings.Length)
                {
                    _isNeedStartMessage = false;
                    _isNeedDeadManMessage = true;
                }
                _startDelay = 0.0f;
            }
        }


        private void DeadManShowText()
        {
            var textStrings = _sittingDeadManText.Split(_specDivider);
            _startDelay += Time.deltaTime;

            if (_startDelay >= _textDelay)
            {
                _UIManager.DisplayCharacterText(textStrings[_currentPointer]);
                _currentPointer += 1;
                
                if (_currentPointer == textStrings.Length)
                {
                    _isNeedDeadManMessage = false;
                }
                _startDelay = 0.0f;
            }
        }


    }
}