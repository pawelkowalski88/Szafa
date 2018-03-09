using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SzafaEntities;

namespace SzafaInterfaces
{
    public interface IClothesEditViewModel
    {
        ICommand BrowseForFile { get; }
        ICommand CancelCommand { get; }
        PieceOfClothing CurrentItem { get; set; }
        ICommand EditOKCommand { get; }
        string Title { get; set; }
        ObservableCollection<ClothingType> TypesList { get; set; }
    }
}
