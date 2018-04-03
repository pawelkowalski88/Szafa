using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesListModule.Filtering
{
    public class TypeFilteringConditionsService
    {
        public List<FilteringConditions> Conditions { get; set; }
        public event EventHandler FilteringConditionsUpdated;
        private ITypesService typesService;

        public TypeFilteringConditionsService(ITypesService ts)
        {
            typesService = ts;
            Conditions = new List<FilteringConditions>();
            Conditions.Add(new FilteringConditions()
            {
                Name = "Wszystkie",
                Conditions = new Predicate<PieceOfClothing>(x => { return true; })
            });

            typesService.TypesListUpdated += TypesService_TypesListUpdated;
            //typesService.UpdateTypesList();
        }

        private void TypesService_TypesListUpdated(object sender, EventArgs e)
        {
            List<ClothingType> listTypes = typesService.TypesList;

            Conditions = new List<FilteringConditions>();
            Conditions.Add(new FilteringConditions()
            {
                Name = "Wszystkie",
                Conditions = new Predicate<PieceOfClothing>(x => { return true; })
            });
            foreach (var t in listTypes)
            {
                //string searchName = t.Name.ToString();
                long typeID = t.Id;
                Conditions.Add(new FilteringConditions()
                {
                    Name = t.Name,
                    //Conditions = new Predicate<PieceOfClothing>(x => Check(x, searchName))
                    Conditions = new Predicate<PieceOfClothing>(x => Check(x, typeID))
                });
             
            }

            FilteringConditionsUpdated(Conditions, new EventArgs());
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
