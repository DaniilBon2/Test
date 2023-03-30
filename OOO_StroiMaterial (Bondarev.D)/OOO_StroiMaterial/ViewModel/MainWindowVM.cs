using OOO_StroiMaterial.DbEnti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media.Media3D;

namespace OOO_StroiMaterial.ViewModel
{
    public class MainWindowVM : BaseVM

    {

        private ObservableCollection<Materials> _materialInfo;
        private Materials _selectedMaterial;


        public ObservableCollection<Materials> Materials
        {
            get => _materialInfo;
            set
            {
                _materialInfo = value;
                OnPropertyChanged(nameof(Materials));

            }
        }

        public Materials SelectedMaterials
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged(nameof(SelectedMaterials));
            }
        }

        public MainWindowVM()
        {
            RebindData();
            SetTimer();

        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            RebindData();
        }




        public void RebindData()
        {

            Materials = new ObservableCollection<Materials>();
            LoadData();
        }




        private void SetTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        public void DeleteSelectItem()
        {
            if (!(SelectedMaterials is null))
            {
                using (var db = new Materials())
                {

                    var result = MessageBox.Show("Вы действительно хотите удалить выбранный товар?" +
                        "Это действие невозможно отменить.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var entityForDelete = db.Materials.Where(elem => elem.Articul == SelectedMaterials.Articul).FirstOrDefault();

                            db.Materials.Remove(entityForDelete);

                            db.SaveChanges();

                            LoadData();

                            MessageBox.Show("Рейс успешно удален", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }

                }
            }
        }




        public void LoadData()
        {

            var result = AppData.db.Materials.ToList();

            result.ForEach(elem => Materials?.Add(elem));
        }


    }
}
