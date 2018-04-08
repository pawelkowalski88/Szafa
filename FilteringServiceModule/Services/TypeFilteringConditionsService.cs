using FilteringEntities;
using System;
using System.Collections.Generic;
using SzafaEntities;
using SzafaInterfaces;

namespace FilteringServiceModule.Services
{
    public class TypeFilteringConditionsService : ITypeFilteringConditionsService
    {
        public List<FilteringConditions> Conditions { get; set; }
        public event EventHandler FilteringConditionsUpdated;
        private ITypesService typesService;

        public TypeFilteringConditionsService(ITypesService ts)
        {
            typesService = ts;
            Conditions = new List<FilteringConditions>();
            Conditions.Add(GetAllItemsConditions());

            typesService.TypesListUpdated += TypesService_TypesListUpdated;
        }

        private void TypesService_TypesListUpdated(object sender, EventArgs e)
        {
            List<ClothingType> listTypes = typesService.TypesList;

            Conditions = new List<FilteringConditions>();
            Conditions.Add(GetAllItemsConditions());
            foreach (var t in listTypes)
            {
                long typeID = t.Id;
                Conditions.Add(new FilteringSortingConditions()
                {
                    Name = t.Name,
                    Conditions = new Predicate<PieceOfClothing>(x => Check(x, typeID))
                });
             
            }

            FilteringConditionsUpdated(Conditions, new EventArgs());
        }

        private FilteringSortingConditions GetAllItemsConditions()
        {
            return new FilteringSortingConditions()
            {
                Name = "Wszystkie",
                Conditions = new Predicate<PieceOfClothing>(x => { return true; })
            };
        }

        private bool Check(PieceOfClothing x, string name)
        {
            if (x.Type != null)
            {
                return x.Type.Name == name;
            }
            return false;
        }

        private bool Check(PieceOfClothing x, long typeId)
        {
            if(x.Type != null)
            {
                return x.Type.Id == typeId;
            }
            return false;
        }
    }
}
