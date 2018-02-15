using Prism.Events;
using SzafaEntities;

namespace CustomEvents
{
    public class ClothesEditButtonClickedEvent : PubSubEvent<PieceOfClothing>
    {
        public ClothesEditButtonClickedEvent()
        {

        }
    }
}
