using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace Feladat
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<String> utoNevek = new ObservableCollection<string>();
		public ObservableCollection<String> csaladNevek= new ObservableCollection<string>();
		public ObservableCollection<String> generaltNevek = new ObservableCollection<string>();
		public ObservableCollection<String> torlendoCsaladNevek = new ObservableCollection<string>();
		public ObservableCollection<String> torlendoUtoNevek = new ObservableCollection<string>();
		public ObservableCollection<String> torlendoUtoNevek2 = new ObservableCollection<string>();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnUtonev_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog()==true)
			{
				foreach (var nev in File.ReadAllLines(ofd.FileName).ToList())
				{
					utoNevek.Add(nev);
				}
				lbUtonevek.ItemsSource = utoNevek;
			}
			lblUtonevSzam.Content = utoNevek.Count;
			
		}

		private void btnCsaladnev_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == true)
			{
				foreach (var nev in File.ReadAllLines(ofd.FileName).ToList())
				{
					csaladNevek.Add(nev);
				}
				lbCsaladnevek.ItemsSource = csaladNevek;
			}


			lblCsaladnevSzam.Content=csaladNevek.Count;
			lblMaxSzam.Content = csaladNevek.Count;
			sldCsuszka.Maximum = csaladNevek.Count;
		}

		private void btnGeneral_Click(object sender, RoutedEventArgs e)
		{
			Random rnd = new Random();
			double csuzskaErtek = Convert.ToInt32(sldCsuszka.Value);
			//lbGeneraltNevek.Items.SortDescriptions.Clear();
			if (rbEgy.IsChecked == true)
			{

				for (int i = 0; i < Math.Floor(csuzskaErtek); i++)
				{
					int csaladnevRandom = rnd.Next(0, csaladNevek.Count);
					int utonevRandom = rnd.Next(0, utoNevek.Count);


					generaltNevek.Add(csaladNevek[csaladnevRandom] + " " + utoNevek[utonevRandom]);
					torlendoCsaladNevek.Add(csaladNevek[csaladnevRandom]);
					torlendoUtoNevek.Add(utoNevek[utonevRandom]);
					csaladNevek.RemoveAt(csaladnevRandom);
					utoNevek.RemoveAt(utonevRandom);

				}

				foreach (var item in generaltNevek)
				{
					lbGeneraltNevek.Items.Add(item);
				}
				generaltNevek.Clear();
				//lbGeneraltNevek.ItemsSource = generaltNevek;


			}
			
			else if (rbKetto.IsChecked == true)
			{

				for (int i = 0; i < Math.Floor(csuzskaErtek); i++)
				{
					string utonev, csaladnev, utonev2;
					int csaladnevRandom = rnd.Next(0, csaladNevek.Count);
					int utonevRandom = rnd.Next(0, utoNevek.Count);
					int utonevRandom2 = rnd.Next(0, utoNevek.Count-1);
					csaladnev = csaladNevek[csaladnevRandom];
					csaladNevek.RemoveAt(csaladnevRandom);
					utonev = utoNevek[utonevRandom];
					utoNevek.RemoveAt(utonevRandom);
					utonev2 = utoNevek[utonevRandom2];
					utoNevek.RemoveAt(utonevRandom2);
					generaltNevek.Add(csaladnev + " " + utonev + " " + utonev2);
					torlendoCsaladNevek.Add(csaladnev);
					torlendoUtoNevek.Add(utonev);
					torlendoUtoNevek2.Add(utonev2);
					
				}
				//lbGeneraltNevek.ItemsSource = generaltNevek;

				foreach (var item in generaltNevek)
				{
					lbGeneraltNevek.Items.Add(item);
				}
				generaltNevek.Clear();
			}

			sldCsuszka.Maximum = csaladNevek.Count;
			lblUtonevSzam.Content = utoNevek.Count;
			lblCsaladnevSzam.Content = csaladNevek.Count;
			lblMaxSzam.Content = csaladNevek.Count;
			stbRendezes.Content = " ";
			NevListaVegereUgras();

		}

		private void sldCsuszka_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			txtSzamlalo.Text = Convert.ToString((int)sldCsuszka.Value);
		}

		private void btnTorol_Click(object sender, RoutedEventArgs e)
		{
			generaltNevek.Clear();
			lbGeneraltNevek.Items.Clear();
			
			
			foreach (Object item in torlendoCsaladNevek)
			{
				csaladNevek.Add((string)item);
			}

			foreach (Object item in torlendoUtoNevek)
			{
				utoNevek.Add((string)item);
			}

			foreach (Object item in torlendoUtoNevek2)
			{
				utoNevek.Add((string)item);
			}
			
			torlendoUtoNevek.Clear();
			torlendoUtoNevek2.Clear();
			torlendoCsaladNevek.Clear();
			lbCsaladnevek.ItemsSource = csaladNevek;
			lbUtonevek.ItemsSource = utoNevek;

			sldCsuszka.Maximum = csaladNevek.Count;
			lblUtonevSzam.Content = utoNevek.Count;
			lblCsaladnevSzam.Content = csaladNevek.Count;
			lblMaxSzam.Content = csaladNevek.Count;
			NevListaVegereUgras();

		}

		private void txtSzmalalo_TextChanged(object sender, TextChangedEventArgs e)
		{

			try
			{
				if (txtSzamlalo.Focus())
				{
					sldCsuszka.Value = Convert.ToDouble(txtSzamlalo.Text);
				}
			}
			catch (Exception)
			{

				if (txtSzamlalo.Text == "")
				{
					sldCsuszka.Value = 0;
				}
			}
			
        }

		private void NevListaVegereUgras()
		{

			lbCsaladnevek.Items.MoveCurrentToLast();
			lbCsaladnevek.ScrollIntoView(lbCsaladnevek.Items.CurrentItem);
			lbUtonevek.Items.MoveCurrentToLast();
			lbUtonevek.ScrollIntoView(lbUtonevek.Items.CurrentItem);
			lbGeneraltNevek.Items.MoveCurrentToLast();
			lbGeneraltNevek.ScrollIntoView(lbGeneraltNevek.Items.CurrentItem);

		}

		private void btnRendez_Click(object sender, RoutedEventArgs e)
		{

			lbGeneraltNevek.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
			stbRendezes.Content = "Rendezett névsor!";
		}

		private void btnMentes_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.AddExtension = true;
			sfd.DefaultExt = "txt";
			sfd.Filter = "Szöveges fájl (*.txt) | *.txt | CSV fájl (*.csv) | *.csv | Összes fájl (*.*) | *.*";
			sfd.Title = "Adja meg a névsor nevét!";
			
			if (sfd.ShowDialog() == true)
			{
				StreamWriter saveFile = new StreamWriter(sfd.FileName);
				try
				{
					

					if (sfd.FilterIndex == 2)
					{
						string nevek = "";
						foreach (var item in lbGeneraltNevek.Items)
						{
							string[] tomb = item.ToString().Split(" ");
							if (tomb.Length == 2)
							{
								nevek = tomb[0] + ";" + tomb[1];
							}
							else if (tomb.Length == 3)
							{
								nevek = tomb[0] + ";" + tomb[1] + ";" + tomb[2];
							}
							saveFile.WriteLine(nevek);
						}
						saveFile.Close();
					}

					else
					{
						for (int i = 0; i < lbGeneraltNevek.Items.Count; i++)
						{
							saveFile.WriteLine((string)lbGeneraltNevek.Items[i]);
						}
						saveFile.Close();
					}
				}
				catch (Exception)
				{

					throw;
					
				}

				
				MessageBox.Show("Sikeres a mentés!");
				saveFile.Dispose();
			}
			
		}

		private void DoubleClick(object sender, MouseButtonEventArgs e)
		{
			string? kivalasztottNev = Convert.ToString(lbGeneraltNevek.SelectedItem);
			string[] tomb = kivalasztottNev.Split(" ");
			if (lbGeneraltNevek.SelectedItem != null)
			{
				generaltNevek.Remove(kivalasztottNev);

				if (tomb.Length == 2)
				{
					
					torlendoCsaladNevek.Remove(tomb[0]);
					torlendoUtoNevek.Remove(tomb[1]);
				}

				else if (tomb.Length == 3)
				{
					torlendoCsaladNevek.Remove(tomb[0]);
					torlendoUtoNevek.Remove(tomb[1]);
					torlendoUtoNevek2.Remove(tomb[2]);
				}

			}

			lbGeneraltNevek.ItemsSource = generaltNevek;

			if (tomb.Length == 3)
			{
				csaladNevek.Add(tomb[0]);
				utoNevek.Add(tomb[1]);
				utoNevek.Add(tomb[2]);
			}
			else
			{
				csaladNevek.Add(tomb[0]);
				utoNevek.Add(tomb[1]);
			}
		

			sldCsuszka.Maximum = csaladNevek.Count;
			lblUtonevSzam.Content = utoNevek.Count;
			lblCsaladnevSzam.Content = csaladNevek.Count;
			lblMaxSzam.Content = csaladNevek.Count;

			NevListaVegereUgras();

		}
		
	}
}
