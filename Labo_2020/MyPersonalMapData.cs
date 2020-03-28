using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyCartographyObjects
{
	[Serializable]
	public class MyPersonalMapData
	{
		#region VARIABLES MEMBRES
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string Mail { get; set; }
		public ObservableCollection<ICartoObj> OcICartoObj { get; set; }
		#endregion

		#region CONSTRUCTEURS
		public MyPersonalMapData()
		{
			Nom = "Nom";
			Prenom = "Prenom";
			Mail = "nom.prenom@hepl.be";
			OcICartoObj = new ObservableCollection<ICartoObj>();
		}

		public MyPersonalMapData(string nom, string prenom, string mail, ObservableCollection<ICartoObj> ocICartoObj) : this()
		{
			Nom = nom;
			Prenom = prenom;
			Mail = mail;
			OcICartoObj = ocICartoObj;
		}
		#endregion

		#region METHODES
		public void Reset()
		{
			/*
			Console.WriteLine("Avant reset: ");
			foreach (ICartoObj co in _ocICartoObj)
				Console.WriteLine(co.ToString());
			*/
			OcICartoObj.Clear();
			/*
			Console.WriteLine("Après reset: ");
			foreach (ICartoObj co in _ocICartoObj)
				Console.WriteLine(co.ToString());
			*/
		}

		public void Load(string filename)
		{
			/* le filedialog s'ouvre dans l'application wpf, ici on effectue le chargement du fichier à partir
			 * du chemin donné en paramètre */

			filename = @"C:\Users\Yannick\OneDrive\Prog\yannickjooris" + filename;
			try
			{
				if(File.Exists(filename))
				{
					BinaryFormatter binFormat = new BinaryFormatter();
					using (Stream fstream = File.OpenRead(filename))
					{
						OcICartoObj = (ObservableCollection<ICartoObj>)binFormat.Deserialize(fstream);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			// Object is disposed of indirectly using the "using" language construct
		}

		public void Save(string filename)
		{
			/* le filedialog s'ouvre dans l'application wpf, ici on effectue la sauvegarde du fichier à partir
			 * du chemin donné en paramètre */
			filename = @"C:\Users\Yannick\OneDrive\Prog\yannickjooris" + filename;

			try
			{
				BinaryFormatter binFormat = new BinaryFormatter();
				using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					binFormat.Serialize(fStream, OcICartoObj);
				}			
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			// Object is disposed of indirectly using the "using" language construct
		}

		public override string ToString()
		{
			if (OcICartoObj != null)
				return Nom + "\n" + Prenom + "\n" + Mail + "\n" + string.Join("\n\t", OcICartoObj);
			else
				return Nom + "\n" + Prenom + "\n" + Mail;
		}


		#endregion
	}
}
