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
		public ObservableCollection<string> utoNevek = new ObservableCollection<string>();
		public ObservableCollection<string> csaladNevek= new ObservableCollection<string>();
		public ObservableCollection<string> generaltNevek = new ObservableCollection<string>();
		public ObservableCollection<string> torlendoCsaladNevek = new ObservableCollection<string>();
		public ObservableCollection<string> torlendoUtoNevek = new ObservableCollection<string>();
		public ObservableCollection<string> torlendoUtoNevek2 = new ObservableCollection<string>();

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
			int csuzskaErtek = Convert.ToInt32(sldCsuszka.Value);
			lbGeneraltNevek.Items.SortDescriptions.Clear();
			if (rbEgy.IsChecked == true)
			{

				for (int i = 0; i < csuzskaErtek; i++)
				{
					int csaladnevRandom = rnd.Next(0, csaladNevek.Count);
					int utonevRandom = rnd.Next(0, utoNevek.Count);


					generaltNevek.Add(csaladNevek[csaladnevRandom] + " " + utoNevek[utonevRandom]);
					torlendoCsaladNevek.Add(csaladNevek[csaladnevRandom]);
					torlendoUtoNevek.Add(utoNevek[utonevRandom]);
					csaladNevek.RemoveAt(csaladnevRandom);
					utoNevek.RemoveAt(utonevRandom);

				}
				lbGeneraltNevek.ItemsSource = generaltNevek;


			}
			
			else if (rbKetto.IsChecked == true)
			{


				for (int i = 0; i < csuzskaErtek; i++)
				{
					int csaladnevRandom = rnd.Next(0, csaladNevek.Count);
					int utonevRandom = rnd.Next(0, utoNevek.Count);
					int utonevRandom2 = rnd.Next(0, utoNevek.Count);
					generaltNevek.Add(csaladNevek[csaladnevRandom] + " " + utoNevek[utonevRandom] + " " + utoNevek[utonevRandom2]);
					torlendoCsaladNevek.Add(csaladNevek[csaladnevRandom]);
					torlendoUtoNevek.Add(utoNevek[utonevRandom]);
					torlendoUtoNevek2.Add(utoNevek[utonevRandom2]);
					csaladNevek.RemoveAt(csaladnevRandom);
					if (utonevRandom2 >= utoNevek.Count || utoNevek[utonevRandom2] == utoNevek[utonevRandom])
					{
						continue;
					}

					utoNevek.RemoveAt(utonevRandom);
					utoNevek.RemoveAt(utonevRandom2);
				}
				lbGeneraltNevek.ItemsSource = generaltNevek;
				

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
			txtSzmalalo.Text = Convert.ToString((int)sldCsuszka.Value);
		}

		private void btnTorol_Click(object sender, RoutedEventArgs e)
		{
			lbGeneraltNevek.ItemsSource="";
			generaltNevek.Clear();


			
			
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
				if (txtSzmalalo.Focus())
				{
					sldCsuszka.Value = Convert.ToDouble(txtSzmalalo.Text);
				}
			}
			catch (Exception)
			{

				if (txtSzmalalo.Text == "")
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
					for (int i = 0; i < lbGeneraltNevek.Items.Count; i++)
					{
						saveFile.WriteLine((string)lbGeneraltNevek.Items[i]);
					}
					saveFile.Close();
				}
				catch (Exception)
				{

					throw;
					
				}
				saveFile.Dispose();
			}
			
		}

		private void DoubleClick(object sender, MouseButtonEventArgs e)
		{
			string kivalasztottNev = Convert.ToString(lbGeneraltNevek.SelectedItem);
			
			if (lbGeneraltNevek.SelectedItem != null)
			{
				generaltNevek.Remove(kivalasztottNev);

				if (kivalasztottNev.Length == 2)
				{
					string[] tomb = kivalasztottNev.Split(" ");
					torlendoCsaladNevek.Remove(tomb[0]);
					torlendoUtoNevek.Remove(tomb[1]);
				}

				else if (kivalasztottNev.Length == 3)
				{
					string[] tomb = kivalasztottNev.Split(" ");
					torlendoCsaladNevek.Remove(tomb[0]);
					torlendoUtoNevek.Remove(tomb[1]);
					torlendoUtoNevek2.Remove(tomb[1]);
				}

			}

			string[] visszadad = kivalasztottNev.Split(" ");

			lbGeneraltNevek.ItemsSource = generaltNevek;

			if (visszadad.Length == 3)
			{
				csaladNevek.Add(visszadad[0]);
				utoNevek.Add(visszadad[1]);
				utoNevek.Add(visszadad[2]);
			}
			else
			{
				csaladNevek.Add(visszadad[0]);
				utoNevek.Add(visszadad[1]);
			}
		

			sldCsuszka.Maximum = csaladNevek.Count;
			lblUtonevSzam.Content = utoNevek.Count;
			lblCsaladnevSzam.Content = csaladNevek.Count;
			lblMaxSzam.Content = csaladNevek.Count;

			NevListaVegereUgras();

		}
		
	}
}
