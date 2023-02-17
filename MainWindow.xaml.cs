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
					lbUtonevek.Items.Add(nev);
				}
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
					lbCsaladnevek.Items.Add(nev);
				}
			}


			lblCsaladnevSzam.Content= lbCsaladnevek.Items.Count;
			lblMaxSzam.Content = lbCsaladnevek.Items.Count;
			sldCsuszka.Maximum = lbCsaladnevek.Items.Count;
		}

		private void btnGeneral_Click(object sender, RoutedEventArgs e)
		{
			Random rnd = new Random();
			double csuzskaErtek = Convert.ToInt32(sldCsuszka.Value);
			if (rbEgy.IsChecked == true)
			{

				for (int i = 0; i < Math.Floor(csuzskaErtek); i++)
				{
					int csaladnevRandom = rnd.Next(0, lbCsaladnevek.Items.Count);
					int utonevRandom = rnd.Next(0, lbUtonevek.Items.Count);


					generaltNevek.Add(lbCsaladnevek.Items[csaladnevRandom] + " " + lbUtonevek.Items[utonevRandom]);
					torlendoCsaladNevek.Add((string)lbCsaladnevek.Items[csaladnevRandom]);
					torlendoUtoNevek.Add((string)lbUtonevek.Items[utonevRandom]);
					lbCsaladnevek.Items.RemoveAt(csaladnevRandom);
					lbUtonevek.Items.RemoveAt(utonevRandom);
					csaladNevek.RemoveAt(csaladnevRandom);
					utoNevek.RemoveAt(utonevRandom);
					

				}

				foreach (var item in generaltNevek)
				{
					lbGeneraltNevek.Items.Add(item);
				}
				generaltNevek.Clear();


			}
			
			else if (rbKetto.IsChecked == true)
			{

				for (int i = 0; i < Math.Floor(csuzskaErtek); i++)
				{
					string utonev, csaladnev, utonev2;
					int csaladnevRandom = rnd.Next(0, lbCsaladnevek.Items.Count);
					int utonevRandom = rnd.Next(0, lbUtonevek.Items.Count);
					int utonevRandom2 = rnd.Next(0, lbUtonevek.Items.Count - 1);
					csaladnev = (string)lbCsaladnevek.Items[csaladnevRandom];
					lbCsaladnevek.Items.RemoveAt(csaladnevRandom);
					csaladNevek.RemoveAt(csaladnevRandom);
					utonev = (string)lbUtonevek.Items[utonevRandom];
					lbUtonevek.Items.RemoveAt(utonevRandom);
					utoNevek.RemoveAt(utonevRandom);
					utonev2 = (string)lbUtonevek.Items[utonevRandom2];
					lbUtonevek.Items.RemoveAt(utonevRandom2);
					utoNevek.RemoveAt(utonevRandom2);
					generaltNevek.Add(csaladnev + " " + utonev + " " + utonev2);
					torlendoCsaladNevek.Add(csaladnev);
					torlendoUtoNevek.Add(utonev);
					torlendoUtoNevek2.Add(utonev2);
					
				}

				foreach (var item in generaltNevek)
				{
					lbGeneraltNevek.Items.Add(item);
				}
				generaltNevek.Clear();
			}

			sldCsuszka.Maximum = lbCsaladnevek.Items.Count;
			lblUtonevSzam.Content = lbUtonevek.Items.Count;
			lblCsaladnevSzam.Content = lbCsaladnevek.Items.Count;
			lblMaxSzam.Content = lbCsaladnevek.Items.Count;
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
				lbCsaladnevek.Items.Add(item);
			}

			foreach (Object item in torlendoUtoNevek)
			{
				utoNevek.Add((string)item);
				lbUtonevek.Items.Add(item);

			}

			foreach (Object item in torlendoUtoNevek2)
			{
				utoNevek.Add((string)item);
				lbUtonevek.Items.Add(item);
				

			}

			torlendoUtoNevek.Clear();
			torlendoUtoNevek2.Clear();
			torlendoCsaladNevek.Clear();
			sldCsuszka.Maximum = lbCsaladnevek.Items.Count;
			lblUtonevSzam.Content = lbUtonevek.Items.Count;
			lblCsaladnevSzam.Content = lbCsaladnevek.Items.Count;
			lblMaxSzam.Content = lbCsaladnevek.Items.Count;
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
						foreach (Object item in lbGeneraltNevek.Items)
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
					lbGeneraltNevek.Items.Remove(kivalasztottNev);
				}

				else if (tomb.Length == 3)
				{
					torlendoCsaladNevek.Remove(tomb[0]);
					torlendoUtoNevek.Remove(tomb[1]);
					torlendoUtoNevek2.Remove(tomb[2]);
					lbGeneraltNevek.Items.Remove(kivalasztottNev);
				}

			}


			if (tomb.Length == 3)
			{
				csaladNevek.Add(tomb[0]);
				utoNevek.Add(tomb[1]);
				utoNevek.Add(tomb[2]);
				lbCsaladnevek.Items.Add(tomb[0]);
				lbUtonevek.Items.Add(tomb[1]);
				lbUtonevek.Items.Add(tomb[2]);
			}
			else
			{
				csaladNevek.Add(tomb[0]);
				utoNevek.Add(tomb[1]);
				lbCsaladnevek.Items.Add(tomb[0]);
				lbUtonevek.Items.Add(tomb[1]);
			}
		

			sldCsuszka.Maximum = lbCsaladnevek.Items.Count;
			lblUtonevSzam.Content = lbUtonevek.Items.Count;
			lblCsaladnevSzam.Content = lbCsaladnevek.Items.Count;
			lblMaxSzam.Content = lbCsaladnevek.Items.Count;

			NevListaVegereUgras();

		}

		private void btnAthelyezes_Click(object sender, RoutedEventArgs e)
		{
			lbCsaladnevek.Items.Clear();
			lbUtonevek.Items.Clear();


			foreach (Object item in csaladNevek)
			{
				lbUtonevek.Items.Add(item);
			}

			foreach (Object item in utoNevek)
			{
				lbUtonevek.Items.Add(item);
			}
			utoNevek.Clear();

			foreach (var item in csaladNevek)
			{
				utoNevek.Add(item);
			}
			foreach (Object item in lbUtonevek.Items)
			{
				utoNevek.Add((string)item);
			}

			csaladNevek.Clear();
			sldCsuszka.Maximum = lbCsaladnevek.Items.Count;
			lblUtonevSzam.Content = lbUtonevek.Items.Count;
			lblCsaladnevSzam.Content = lbCsaladnevek.Items.Count;
			lblMaxSzam.Content = lbCsaladnevek.Items.Count;
		}
    }
}
