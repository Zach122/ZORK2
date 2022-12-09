namespace LaboFinal_A22
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            Controleur ctrl = new Controleur(new string[2] { "orc", "koboldMage" });
            ctrl.jouer();
        }
    }
}