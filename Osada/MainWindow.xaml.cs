using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Osada.Annotations;
using Osada.map;
using Osada.player;
using Osada.building;

namespace Osada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int currentX, currentY, lastX, lastY;
        private int currentFieldId, lastFieldId;
        private Player PlayerN1, PlayerN2;
        private int i;
        private int Turn;

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainWindow()
        {
            Turn = 1;
            i = 0;
            currentX = currentY = lastX = lastY = currentFieldId = lastFieldId = 0;
            InitializeComponent();

            this.DataContext = this;
            
            Board = new ObservableCollection<Field>{};
            Map Drawer = new Map(); 
            Drawer.MapGenerator(Board);
            PlayerN1 = new Player(1);
            PlayerN2 = new Player(2);
            playersResourceRefresher();

            Buildings = new ObservableCollection<Building> {};


        }

        private void Build(object sender, RoutedEventArgs e)
        {
            var elementAt = Board.ElementAt(currentFieldId);
            string type = Convert.ToString((sender as Button).Tag);
            Building buildingToBuild;
            if (type == "Dom")
            {
                buildingToBuild = new House(ref PlayerN1, ref elementAt);
            }
            else if (type == "Pole - zboże")
            {
                buildingToBuild = new Ground(ref PlayerN1, ref elementAt, type);
            }
            else if (type == "Armia")
            {
                buildingToBuild = new Army(ref PlayerN1, ref elementAt);
            }
            else
            {
                buildingToBuild = null;
            }
            //MessageBox.Show(Convert.ToString(PlayerN1.Resource[0]));
            Buildings.Add(buildingToBuild);
            Board.Insert(currentFieldId + 1, Board.ElementAt(currentFieldId));
            Board.RemoveAt(currentFieldId);
            playersResourceRefresher();
            RefreshInfo();


        }        
        private ObservableCollection<Field> _board;
        public ObservableCollection<Field> Board
        {
            get
            {
                return _board;
                OnPropertyChanged();
            }
            set
            {
                if (_board != value)
                {
                    _board = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Building> _buildings;
        public ObservableCollection<Building> Buildings
        {
            get
            {
                return _buildings;
                OnPropertyChanged();
            }
            set
            {
                if (_buildings != value)
                {
                    _buildings = value;
                    OnPropertyChanged();
                }
            }
        }
        private void Field_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(1,1,1,1);
        }
        private void Field_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).BorderThickness = new Thickness(0,0,0,0);
        }
        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int elementId = Convert.ToInt32((sender as Border).Tag);
            currentFieldId = Board.ElementAt(elementId).FieldId;
            lastX = currentX;
            lastY = currentY;
            lastFieldId = currentFieldId;
            RefreshInfo();
        }
        private void RefreshInfo()
        {
            {//info o polu
                currentX = Board.ElementAt(currentFieldId).X;
                currentY = Board.ElementAt(currentFieldId).Y;
                currentFieldId = Board.ElementAt(currentFieldId).FieldId;
                InfoGrid.Background = Board.ElementAt(currentFieldId).Color;
                TbX.Text = Convert.ToString(currentX);
                TbY.Text = Convert.ToString(currentY);
                TbXLast.Text = Convert.ToString(lastX);
                TbYLast.Text = Convert.ToString(lastY);
                TbIdLast.Text = Convert.ToString(lastFieldId);
                TbId.Text = Convert.ToString(Board.ElementAt(currentFieldId).FieldId);
                TbHeight.Text = Convert.ToString(Board.ElementAt(currentFieldId).Height);
                TbType.Text = Convert.ToString(Board.ElementAt(currentFieldId).Type);
            }
            {//info o budynku
                TbBuildingType.Text = Convert.ToString(Board.ElementAt(currentFieldId).Building);
                if (Board.ElementAt(currentFieldId).Building != "Pusto")
                {
                    TbCrewLabel.Text = "Załoga:";
                    TbCrewSlash.Text = "/";
                    TbOwnerLabel.Text = "Właściciel:";
                    TbOwner.Text = Convert.ToString(Board.ElementAt(currentFieldId).Owner);
                    var crew = from Building in Buildings
                               where Building.FieldId == currentFieldId
                               select new { Crew = Building.Crew, CrewMax = Building.CrewMax };
                    foreach (var change in crew)
                    {
                        TbCrew.Text = Convert.ToString(change.Crew);
                        TbCrewMax.Text = Convert.ToString(change.CrewMax);
                    }
                }
                else
                {
                    TbOwnerLabel.Text = "";
                    TbOwner.Text = "";
                    TbCrew.Text = "";
                    TbCrewMax.Text = "";
                    TbCrewLabel.Text = "";
                    TbCrewSlash.Text = "";
                }
            }
        }
        private void BtBuildingMenu_Click(object sender, RoutedEventArgs e)
        {
            GridArmy.Visibility = Visibility.Hidden;
            GridDiscovering.Visibility = Visibility.Hidden;
            GridBuild.Visibility = Visibility.Visible;
        }
        private void BtDiscoveringMenu_Click(object sender, RoutedEventArgs e)
        {
            GridArmy.Visibility = Visibility.Hidden;
            GridDiscovering.Visibility = Visibility.Visible;
            GridBuild.Visibility = Visibility.Hidden;
        }
        private void BtArmyMenu_Click(object sender, RoutedEventArgs e)
        {
            GridArmy.Visibility = Visibility.Visible;
            GridDiscovering.Visibility = Visibility.Hidden;
            GridBuild.Visibility = Visibility.Hidden;
        }
        private void playersResourceRefresher()
        {
            TbFood.Text = Convert.ToString(PlayerN1.Resource[0]);
            TbGold.Text = Convert.ToString(PlayerN1.Resource[1]);
            TbStone.Text = Convert.ToString(PlayerN1.Resource[2]);
            TbWood.Text = Convert.ToString(PlayerN1.Resource[3]);
            TbSettlers.Text = Convert.ToString(PlayerN1.Settlers);
            TbSettlersMax.Text = Convert.ToString(PlayerN1.SettlersMax);
            TbSettlersAll.Text = Convert.ToString(PlayerN1.SettlersAll);
            TbSciencists.Text = Convert.ToString(PlayerN1.Sciencists);
            TbSciencistsAll.Text = Convert.ToString(PlayerN1.SciencistsAll);
            TbSoldiers.Text = Convert.ToString(PlayerN1.Soldiers);
            TbSoldiersAll.Text = Convert.ToString(PlayerN1.SoldiersAll);
            TbWoodTech.Text = Convert.ToString(PlayerN1.Technology[3]);
            TbStoneTech.Text = Convert.ToString(PlayerN1.Technology[2]);
            TbGoldTech.Text = Convert.ToString(PlayerN1.Technology[1]);
            TbFoodTech.Text = Convert.ToString(PlayerN1.Technology[0]);
        }
        private void NextTurn(object sender, RoutedEventArgs e)
        {
            Turn++;
            BtNextTurn.Content = Convert.ToString(Turn) + " >>";
            
            var play1 = from Building in Buildings
                       where Building.Owner == 1
                       select Building;
            foreach (var building in play1)
            {
                building.Work(ref PlayerN1);
            }
            play1 = from Building in Buildings
                        where Building.Owner == 2
                        select Building;
            foreach (var building in play1)
            {
                building.Work(ref PlayerN2);
            }
            playersResourceRefresher();
        }
        private void Employ(object sender, RoutedEventArgs e)
        {
            if (Board.ElementAt(currentFieldId).Building != "Pusto")
            {
                var play1 = from Building in Buildings
                            where Building.FieldId == currentFieldId
                            select Building;
                foreach (var building in play1)
                {
                    building.EmployCrew(ref PlayerN1);
                }
            }
            RefreshInfo();
            playersResourceRefresher();
        }
        private void Release(object sender, RoutedEventArgs e)
        {
            if (Board.ElementAt(currentFieldId).Building != "Pusto")
            {
                var play1 = from Building in Buildings
                            where Building.FieldId == currentFieldId
                            select Building;
                foreach (var building in play1)
                {
                    building.ReleaseCrew(ref PlayerN1);
                }
            }
            RefreshInfo();
            playersResourceRefresher();
        }
        private void DestroyBuilding(object sender, RoutedEventArgs e)
        {
            if (Board.ElementAt(currentFieldId).Building != "Pusto")
            {
                var play1 = from Building in Buildings
                            where Building.FieldId == currentFieldId
                            select Building;
                var field = Board.ElementAt(currentFieldId);
                foreach (var building in play1)
                {
                    building.Destroy(ref PlayerN1, ref field);
                    field.ImageGenerator();
                    Buildings.Remove(building);
                }
                
                Board.Insert(currentFieldId + 1, Board.ElementAt(currentFieldId));
                Board.RemoveAt(currentFieldId);
            }
            
            RefreshInfo();
            playersResourceRefresher();
        }
    }

    
}
