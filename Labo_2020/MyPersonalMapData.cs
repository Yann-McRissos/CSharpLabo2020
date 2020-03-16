using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyCartographyObjects
{
	public class MyPersonalMapData
	{
		#region VARIABLES MEMBRES
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string Mail { get; set; }
		private ObservableCollection<ICartoObj> _ocICartoObj;
		#endregion

		#region PROPRIETES
		public ObservableCollection<ICartoObj> OcICartoObj
		{
			get { return _ocICartoObj; }
		}
		#endregion

		#region CONSTRUCTEURS
		public MyPersonalMapData()
		{
			Nom = "Nom";
			Prenom = "Prenom";
			Mail = "nom.prenom@hepl.be";
			new ObservableCollection<ICartoObj>();
		}

		public MyPersonalMapData(string nom, string prenom, string mail, ObservableCollection<ICartoObj> ocICartoObj) : this()
		{
			Nom = nom;
			Prenom = prenom;
			Mail = mail;
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
			_ocICartoObj.Clear();
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
			try
			{
				if(File.Exists(filename))
				{
					BinaryFormatter binFormat = new BinaryFormatter();
					using (Stream fstream = File.OpenRead(filename)
					{
						_ocICartoObj = (ObservableCollection<ICartoObj>)binFormat.Deserialize(fstream);
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

			// faut serialiser
			filename = "yannickjooris" + filename;

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
		#endregion
	}
}
