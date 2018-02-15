using Prism.Events;
using SzafaEntities;

namespace CustomEvents
{
    public class PieceOfClothingChangedEvent : PubSubEvent<PieceOfClothing>
    {
        public PieceOfClothingChangedEvent()
        {

        }
    }
}
