using UnityEngine;

namespace MainMenuControllers.Store
{
    public abstract class CharacterData : ScriptableObject
    {
        [SerializeField] private int _index;
        [SerializeField] private int _ratingToOpen;
        [SerializeField] private TypeWolf _typeWolf;
        [SerializeField] private Sprite _body;
        [SerializeField] private Sprite _leftHand;
        [SerializeField] private Sprite _rightHand;
        [SerializeField] private Sprite _bow;
        [SerializeField] private Sprite _characterIcon;

        public int Index => _index;
        public int RatingToOpen => _ratingToOpen;
        public TypeWolf TypeWolf => _typeWolf;
        public Sprite Body => _body;
        public Sprite LeftHand => _leftHand;
        public Sprite RightHand => _rightHand;
        public Sprite Bow => _bow;
        public Sprite CharacterIcon => _characterIcon;
        public abstract TypeStateStoreItem TypeStateStoreItem { get; }
        public abstract string IndexEntryKey { get; }
    }
}
