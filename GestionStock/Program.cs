using System;
using Products;

namespace Gestion
{     class Program
    {     
        private const int taillePermiseNomProduit = 44;
        private const int taillePermiseStock = 10;
        private const int taillePermiseEtat = 24;
        static void Main(String[] args)
        {
            int n = 1;
            List<Product> Stocks = loadProductsIntoList("./Stocks.txt");
            do{
                affichage(Stocks);
                
                var la = Console.ReadKey();
                
                int productIdToUpdateStock;
                bool temoin = int.TryParse(la.KeyChar.ToString(), out productIdToUpdateStock);
                if (temoin == true) {
                    foreach(var p in Stocks)
                    {
                        if(p.Id == productIdToUpdateStock)
                        {
                            p.Stock--;
                            if (p.Stock <= 0) p.Stock = 0;
                            p.Etat = etatExact(p.Stock);
                        }
                    }
                    if (productIdToUpdateStock == 0)
                    {
                        Environment.Exit(0);
                    }
                } 
                Console.Clear();
            } while(n != 0);
        } 

        static List<int> registerAvailableProductIds(List<Product> products)
        {
            var ids = new List<int>();

            foreach (var item in products)
            {
                ids.Add(item.Id);
            }
            return ids;
        }
      
        static List<Product> loadProductsIntoList(string filePath)
        {
            var lineProductsArray = File.ReadAllLines(filePath).ToList();
            var products = new List<Product>();

            int id;
            int stock;
            string name;
            Etat etat;
            string[] productDataArray;

            foreach( var line in lineProductsArray)
            {
                productDataArray = line.Split(":");

                int.TryParse(productDataArray[0], out id);
                name = productDataArray[1];

                int.TryParse(productDataArray[2], out stock);
                etat = etatExact(stock);

                Product p = new Product(id, name, stock, etat);
                products.Add(p);
            }
            return products;
        }

        static string matchEtatToString(Etat etat)
        {
            string state;

            switch (etat)
            {
                case Etat.DISPONIBLE:
                    state = "RAS";
                break;
                case Etat.EN_RUPTURE:
                    state = "Rupture";
                break;
                case Etat.EN_REAPPROVISIONNEMENT:
                    state = "Réappro";
                break;
                default: state = "Rupture";
                break;
            }
            return state;
        }

        static Etat etatExact(int quantite)
        {
            Etat state = Etat.EN_RUPTURE;
            
            if (quantite > 5) state = Etat.DISPONIBLE;
            if (quantite <= 5) state = Etat.EN_REAPPROVISIONNEMENT;
            if (quantite <= 0) state = Etat.EN_RUPTURE;
            
            return state;
        }

        static void affichage(List<Product> Stocks)
        {
            Console.WriteLine("\n");
            Console.WriteLine("+---+--------------------------------------------+------------+-------------------------+");
            Console.WriteLine("| # | Produit                                    |  Stocks    |           Etat          |");
            Console.WriteLine("+---+--------------------------------------------+------------+-------------------------+");

            foreach (var item in Stocks)
            {
                string id = "";
                if(item.Id<10) {
                    id ="| "+item.Id+" | ";
                }else {
                    id ="|"+item.Id+" | ";
                }
                Console.Write(id);  
                
                Console.Write(item.Name);
                afficherEspaceNFois(taillePermiseNomProduit - item.Name.Length - 1);
                Console.Write("| ");
                
                string qteStr = item.Stock.ToString();
                Console.Write(qteStr+" ");
                afficherEspaceNFois(taillePermiseStock - qteStr.Length);
                Console.Write("| ");
                string etatStock = matchEtatToString(item.Etat);
                Console.Write(etatStock);
                afficherEspaceNFois(taillePermiseEtat - etatStock.Length);
                Console.Write("|\n");
            }
            Console.WriteLine("+---+--------------------------------------------+------------+-------------------------+");
        }

        static void afficherEspaceNFois(int n)
        {
            for (int i = 0; i<n; i++) Console.Write(" ");
        }
	}
    public enum Etat
    {
        DISPONIBLE,
        EN_RUPTURE,
        EN_REAPPROVISIONNEMENT
    }
}