using Gestion;

namespace Products
{
    class Product
    {
        public Product(int id, string name, int stock, Etat etat)
        {
            Id = id;
            Name = name;
            Stock = stock;
            Etat = etat;
        }
        public int Id
        { set; get; }

        public string Name
        { get; set; }
        public int Stock
        { set; get; }
        public Etat Etat
        { set; get; }
        public Product()
        {
            Id = 0;
            Name = "";
            Stock = 0;
            Etat = Etat.EN_RUPTURE;
        }

        override public string ToString()
        {
            return "Id: " + Id + ", Name: " + Name + ", Stock: " + Stock + ", Etat: " +Etat;
        }
    }
}