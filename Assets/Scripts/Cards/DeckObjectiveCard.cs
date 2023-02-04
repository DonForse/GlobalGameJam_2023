using System;

namespace Cards
{
    [Serializable]
    public class DeckObjectiveCard : Card
    {
        public int GrandParents;
        public int Parents;
        public int Childs;
    }
}